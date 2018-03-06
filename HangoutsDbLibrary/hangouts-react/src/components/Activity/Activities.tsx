import * as React from 'react';

import {ActivityService} from './../../services/ActivitiesService'

import * as createClass from 'create-react-class';
import Select from 'react-select';

import * as Cookies from 'js-cookie';

import './Activities.css'
import { IActivity } from '../../models/IActivity';
import {SideBar} from './../StartPage/SideBar'
import {OneActivity} from './OneActivity'
import { NoChoosen } from './NoChoosen';

export interface ActivitiesProps {
    idUser: number;
}

interface ActivitiesState {  
    message: string;
    activitiesFirstPart: IActivity[];
    activitiesSecondPart: IActivity[];
}

var showDiscoverActivity = 'no';

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
    handleSelectChange (value: any) {
        console.log('You\'ve selected:', value);
        if(value!==null){
           // Cookies.set('idActivity', value);
            this.idActivity = value;
            showDiscoverActivity = 'yes';
        }else{
            showDiscoverActivity='no';
        }
            
        this.setState({ value });
    },
    
    handleShowDiscovery(){
        return ((showDiscoverActivity == 'yes') ?
                <OneActivity idActivity={this.idActivity}/> :
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
                    placeholder="Discover new Activities:"
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

export class Activities extends React.Component<ActivitiesProps, ActivitiesState>{

    constructor(props: ActivitiesProps){
        super(props);
        
        this.state = {
            activitiesFirstPart: [], 
            activitiesSecondPart: [], 
            message: 'Nothing done'
        };

    }

    componentDidMount(){
        ActivityService.GetAllActivitiesFromAUser().then((responseActivities : any)=>{

            this.setState({
                activitiesFirstPart: responseActivities,
                activitiesSecondPart : responseActivities.splice(0, responseActivities.length/2)
            });
            
            ActivityService.GetAllActivities().then((responseAllActivities : IActivity[])=>{


                responseAllActivities.map(function(this: Activities, activityFromAllActivities: IActivity){
                    var check=1;
                    this.state.activitiesFirstPart.map(function(activity: IActivity){
                        if(activity.idActivity == activityFromAllActivities.idActivity){
                            check=0;
                        }
                    });
    
                    this.state.activitiesSecondPart.map(function(activity: IActivity){
                        if(activity.idActivity == activityFromAllActivities.idActivity){
                            check=0;
                        }
                    });
    
                    if(check==1){
                        FLAVOURS.push({label: activityFromAllActivities.description, value: activityFromAllActivities.idActivity!.toString()});
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
    
    componentWillReceiveProps(newprops : ActivitiesProps){
        if(newprops!== this.props){
            this.props = newprops;
        }

    }

    handleDeleteActivity(index: number){

            Cookies.set('idActivity', index.toString())
            ActivityService.DeleteActivity().then((responseDeleteActivity : any)=>{

            console.log(responseDeleteActivity);
            window.location.reload();
            
            },
        
            (error) =>{
                this.setState({
                    message: "Error when trying to create a User!"
                });
            });
    }

    render(){

        let createGrid = (activity: IActivity, index: any) => {
            index = activity.idActivity;
            return(
                <li key={activity.idActivity}>
                    <span>
                         
                         <b>Activity description: </b>  {activity.description} 
                         <br></br>
                         <b>Ending time: </b>  {activity.beginningTime} 
                         <br></br>
                         <b>Beginnig time: </b>  {activity.endingTime} 
                         <br></br>
                         <button onClick={(event) => this.handleSettingsCliked(index)} className='dropbtn'>Settings</button>
                         <div className='dropdown-content' id={index}>
                         {/* <a href="#home">Update Activity</a> */}
                         <a onClick={(event) => this.handleDeleteActivity(index)}>Delete Activity</a>
                        </div>
                
                    </span>
                </li>
            );
        }

        let createGridSecond = (activity: IActivity, index: any) => {
            index = activity.idActivity;
            if(activity.description != null){
                return(
                    <li key={index}>
                        <span>
                             
                            <b>Activity description: </b>  {activity.description} 
                            <br></br>
                            <b>Ending time: </b>  {activity.beginningTime} 
                            <br></br>
                            <b>Beginnig time: </b>  {activity.endingTime} 
                            <br></br>
                             <button onClick={(event) => this.handleSettingsCliked(index)} className='dropbtn'>Settings</button>
                             <div className='dropdown-content' id={index}>
                                {/* <a href="#home">Update Activity</a> */}
                                <a onClick={(event) => this.handleDeleteActivity(index)}>Delete Activity</a>
                            </div>
                    
                        </span>
                    </li>
                );
            }
            return (<div></div>);
            
        }
        return(
                <div>
                    <SideBar idUser={this.props.location.state.email}/>
                    <div className='myContainer'>
                        <MultiSelectField/>
                    </div>
                    <div className = 'tabel'>
                        <div className='leftSide'>
                            <ul>
                                {this.state.activitiesFirstPart.map(createGrid)}
                            </ul>    
                        </div>
                        <div className='rightSide'>
                            <ul>
                                {this.state.activitiesSecondPart.map(createGridSecond)}
                            </ul>  
                        </div>
                    </div>
                </div>
            );
    }
}


