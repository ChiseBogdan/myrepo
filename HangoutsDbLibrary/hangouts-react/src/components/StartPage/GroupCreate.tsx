import * as React from 'react';

import * as createClass from 'create-react-class';
import Select from 'react-select';

import {UserService} from './../../services/UserService'

import './SideBar.css';
import './GroupCreate.css';
import './MultipleSelection.css'

import * as Cookies from 'js-cookie';

import { IUser } from '../../models/IUser';
import { IGroup } from '../../models/IGroup';
import { GroupService } from '../../services/GroupService';
import { IUserGroup } from '../../models/IUserGroup';

export interface GroupCreateProps {
    idUser: number;
}

interface GroupCreateState {  
    message: string;
    groupName: string;
}

var Choosen = [-1];


const FLAVOURS = [
      { label: 'admin', value: 'admin' }
    ];

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
  
  setChoosen(values: any){
        Choosen = values.split(',');
        var temp = [0];
        Choosen.map(function(number: any){
            if(number!=-1)
                temp.push(parseInt(number));
        });
        Choosen = temp;
        Choosen.splice(0, 1);
  }
  ,
  handleSelectChange (value: any) {
    this.setChoosen(value);
    this.setState({ value });
  },
  

  render () {
      const { disabled, stayOpen, value } = this.state;
      const options = FLAVOURS;
      return (
          <div className='multipleSelection'>
              <Select
                  closeOnSelect={!stayOpen}
                  disabled={disabled}
                  multi
                  onChange={this.handleSelectChange}
                  options={options}
                  placeholder="Add some friends in Group:"
                  simpleValue
                  value={value}
              />
          </div>
      );
  }
});

export class GroupCreate extends React.Component<GroupCreateProps, GroupCreateState>{

    constructor(props: GroupCreateProps){
        super(props);

        this.state={
            groupName: '',
            message: 'Nothing done'
        }

    }

    componentWillReceiveProps(newprops: GroupCreateProps){
        if(this.props!=newprops){
            this.props = newprops;
        }
    }

    componentDidMount(){

        UserService.getUsers().then((responseUsers : any)=>{
    
            responseUsers.map(function(this: GroupCreate, user : IUser) {
                if(user.idUser!= this.props.idUser)
                    FLAVOURS.push({label: user.fullname, value: user.idUser!.toString()});
             });
             FLAVOURS.splice(0, 1);
        },
    
        (error) =>{
            this.setState({
                message: "Error when trying to get all Users!"
            });
        });
     
    }

    handleCreateGroup(){

        let group : IGroup ={
            name : this.state.groupName
        }

        GroupService.CreateGroup(group).then((responseGroup : any)=>{

            let userGroup :IUserGroup = {
                groupId : parseInt(responseGroup.value.idGroup),
                userId: this.props.idUser
            }
            GroupService.AddUserToGroup(userGroup).then((responseAddUserGroup : any)=>{
                
                console.log(responseAddUserGroup);
            
            },
        
            (error) =>{
                this.setState({
                    message: "Error when trying to create a User!"
                });
            });


            Choosen.map(function(idUser: number){
                    if(idUser != -1){
                        let userGroup :IUserGroup = {
                            groupId : parseInt(responseGroup.value.idGroup),
                            userId: idUser
                        }

                        GroupService.AddUserToGroup(userGroup).then((responseAddUserGroup : any)=>{
                
                            console.log(responseAddUserGroup);
                        
                        },
                    
                        (error) =>{
                            console.log('Eroarea la adaugarea unui User');
                        });


                    }

            });

            },
        
            (error) =>{
                this.setState({
                    message: "Error when trying to create a User!"
                });
            });
        
    }

    handleModal(){
        var modal = Cookies.get("GroupCreate");

        if(modal== "1"){
          document.getElementById('modal')!.style.display='none';
          Cookies.set('GroupCreate', '0');
          window.location.reload();
        }
        else{
          document.getElementById('modal')!.style.display='inline';
        }        
    }

    handleGroupNameChanged(event: any){
        this.setState({groupName: event.target.value});
    }

    render(){

        return(
                <form id='modal' className='modul-content animate'>

                <h1 className='title' > Create Group </h1>


                <span className="close" onClick={(event) => {this.handleModal()}} title="Close Modal">&times;</span>
        
                <div className='container'>
                <label><b>Group Name</b></label>
                <br/>
                <input value={this.state.groupName} onChange={(event) => {this.handleGroupNameChanged(event)}} type="text" required/>
                </div>

                <div>
                <MultiSelectField/>
                </div>

                <div className='container' >
                <button type='button' onClick={(event) => {this.handleModal()}} className="cancelbtn">Cancel</button>
                <button type='button' onClick ={(event) => {this.handleCreateGroup()}} className='createButton'>Create</button>
                </div>
            </form>
        );
    }

    


}