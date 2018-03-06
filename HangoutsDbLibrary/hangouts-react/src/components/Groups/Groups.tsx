import * as React from 'react';

import {GroupService} from './../../services/GroupService'
import { IGroup } from '../../models/IGroup';

import * as Cookies from 'js-cookie';

import * as createClass from 'create-react-class';
import Select from 'react-select';

import './Groups.css'
import './../StartPage/SideBar.css'
import { SideBar } from '../StartPage/SideBar';
import {OneGroup} from './OneGroup'
import { NoChoosen } from './../Activity/NoChoosen';

export interface GroupProps {
    idUser:number;
}

interface GroupState {  
    message: string;
    groupsFirstPart: IGroup[];
    groupsSecondPart: IGroup[];
    allGroups : IGroup[];
    many: Number;


}

const FLAVOURS = [
    { label: 'admin', value: 'admin' }
  ];

var showDiscoverGroup = 'no';

var MultiSelectField = createClass({
    displayName: 'MultiSelectField',
    getInitialState () {
        return {
            removeSelected: true,
            disabled: false,
            crazy: false,
            stayOpen: false,
            value: [],
            rtl: false,
        };
    },
    handleSelectChange (value: any) {
        console.log('You\'ve selected:', value);

        if(value!==null){
            Cookies.set('idGroup', value);
            showDiscoverGroup = 'yes';

        }else{
            showDiscoverGroup='no';
        }
      this.setState({ value });
    },    

    handleShowDiscovery(){
        return ((showDiscoverGroup == 'yes') ?
                <OneGroup /> :
                <NoChoosen />)
    },
  
    render () {
        const { disabled, stayOpen, value } = this.state;
        const options = FLAVOURS;
        return (
            <div>
            <div className='multipleSelection'>
                <Select
                    closeOnSelect={!stayOpen}
                    disabled={disabled}
                    onChange={this.handleSelectChange}
                    options={options}
                    placeholder="Discover new Groups:"
                    simpleValue
                    value={value}
                />
            </div>
            <div >
            {this.handleShowDiscovery()}
            </div>
            </div>
        );
    }
  });

export class Groups extends React.Component<GroupProps, GroupState>{

    constructor(props: GroupProps){
        super(props);
        
        this.state = {
            groupsFirstPart: [], 
            groupsSecondPart: [], 
            allGroups: [],
            message: 'Nothing done',
            many: 0
        };

    }

    componentDidMount(){

        GroupService.GetGroupsFromAUser().then((responseGroupsFromUser : IGroup[])=>{


            this.setState({
                groupsSecondPart: responseGroupsFromUser,
                groupsFirstPart : responseGroupsFromUser.splice(0, responseGroupsFromUser.length/2), 
                many : responseGroupsFromUser.length,
            });



            GroupService.GetAllGroups().then((responseAllGroups : IGroup[])=>{


                responseAllGroups.map(function(this: Groups, groupFromAllGroups: IGroup){
                    var check=1;
                    this.state.groupsFirstPart.map(function(group: IGroup){
                        if(group.idGroup == groupFromAllGroups.idGroup){
                            check=0;
                        }
                    });
    
                    this.state.groupsSecondPart.map(function(group: IGroup){
                        if(group.idGroup == groupFromAllGroups.idGroup){
                            check=0;
                        }
                    });
    
                    if(check==1){
                        FLAVOURS.push({label: groupFromAllGroups.name, value: groupFromAllGroups.idGroup!.toString()});
                    }
    
               }.bind(this));
               FLAVOURS.splice(0,1);
            },
            
            (error) =>{
                console.log('Error');
            });

    
        },
        
        (error) =>{
            console.log('Error');
        });
        
    }


    handleSettingsCliked(index: any){
        return document.getElementById(index)!.classList.toggle("show");
    }

    handleDeleteGroup(index: number){
        Cookies.set('idGroup', index.toString());

        GroupService.DeleteGroup().then((responseDeleteGroup : any)=>{

            console.log(responseDeleteGroup);
            window.location.reload();
            
            },
        
            (error) =>{
                this.setState({
                    message: "Error when trying to delete a Group!"
                });
            });
    }

    handleLeaveGroup(index : number){
        Cookies.set('idGroup', index.toString());
        
        GroupService.RemoveUserFromGroup().then((responseDeleteGroup : any)=>{

            console.log(responseDeleteGroup);
            window.location.reload();
            
            },
        
            (error) =>{
                this.setState({
                    message: "Error when trying to remove a User from a Group!"
                });
            });

    }
    

    render(){

        let createGrid = (group: IGroup, index: any) => {
            index = group.idGroup;
            return(
                <li key={index}>
                    <span>
                         
                         <b>Group name: </b>  {group.name} 
                         <button onClick={(event) => this.handleSettingsCliked(index)} className='dropbtn'> Settings</button>
                         <div className='dropdown-content' id={index}>
                         <a onClick={(event) => this.handleLeaveGroup(index)} >Leave Group</a>
                         <a onClick={(event) => this.handleDeleteGroup(index)}>Delete Group</a>
                        </div>
                
                    </span>
                </li>
            );
        }

        let createGridSecond = (group: IGroup, index: any) => {
            index = group.idGroup;
            if(group.name != null){
                return(
                    <li key={index}>
                        <span>
                             
                             <b>Group name: </b>  {group.name} 
                             <button onClick={(event) => this.handleSettingsCliked(index)} className='dropbtn'>Settings</button>
                             <div className='dropdown-content' id={index}>
                                <a onClick={(event) => this.handleLeaveGroup(index)} >Leave Group</a>
                                <a onClick={(event) => this.handleDeleteGroup(index)}>Delete Group</a>
                            </div>
                    
                        </span>
                    </li>
                );
            }
            return (<div></div>);
            
        }

        return(
            <div>
                <SideBar idUser={this.props.idUser}/>
                <div className='myContainer'>
                <MultiSelectField/>
                </div>
            <div>
                    <div className = 'tabel'>
                        <div className='leftSide'>
                            <ul>
                                {this.state.groupsFirstPart.map(createGrid)}
                            </ul>    
                        </div>
                        <div className='rightSide'>
                            <ul>
                            {this.state.groupsSecondPart.map(createGridSecond)}
                            </ul>  
                        </div>
                    </div>
            </div>
            </div>
            );
    }
}


