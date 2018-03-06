
import * as React from 'react';
import Select from 'react-select';
import * as createClass from 'create-react-class';

import {GroupService} from './../../services/GroupService'
import {ActivityService} from './../../services/ActivitiesService'
import {IGroup} from './../../models/IGroup'
import {IGroupActivity} from './../../models/IGroupActivity'

import './SideBar.css';
import './GroupCreate.css';
import './MultipleSelection.css'

import * as Cookies from 'js-cookie';
import { IActivity } from '../../models/IActivity';

export interface ActivityCreateProps {
}

interface ActivityCreateState {  
    message: string;
    idGroup: string;
    activityDescription: string;
    beginningTime: string;
    endingTime: string;
}

const FLAVOURS = [
    { label: 'admin', value: 'admin' },
  ];

const CHOOSEN = [-1];



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
    CHOOSEN.push(value);
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
                onChange={this.handleSelectChange}
                options={options}
                placeholder="Select the Group:"
                simpleValue
                value={value}
            />
        </div>
    );
}
});

export class ActivityCreate extends React.Component<ActivityCreateProps, ActivityCreateState>{

    constructor(props: ActivityCreateProps){
        super(props);

        this.state = {
            beginningTime: '',
            endingTime: '',
            activityDescription: '',
            idGroup: '',
            message: 'Nothing done'
        }

    }

    handleModal(){
        var modal = Cookies.get("EventCreate");

        if(modal== "1"){
          document.getElementById('modal')!.style.display='none';
          Cookies.set('EventCreate', '0');
          window.location.reload();
        }
        else{
          document.getElementById('modal')!.style.display='inline';
        }        
    }

    componentDidMount(){
        GroupService.GetGroupsFromAUser().then((responseGroups : IGroup[])=>{
    
            responseGroups.map(function(group : IGroup) {
                FLAVOURS.push({label: group.name, value: group.idGroup!.toString()});
             });
             FLAVOURS.splice(0, 1);
        },
    
        (error) =>{
            this.setState({
                message: "Error when trying to get all Groups!"
            });
        });
     
    }

    handleActivityDescriptionChanged(event: any){
        this.setState({activityDescription: event.target.value});
    }

    handleBeginningTimeChanged(event: any){
        this.setState({beginningTime: event.target.value});
    }

    handleEndingTimeChanged(event: any){
        this.setState({endingTime: event.target.value});
    }

    getFromChoosen(){
        CHOOSEN.splice(0, CHOOSEN.length-1);
        return parseInt(CHOOSEN[0].toString());
    }

    handleCreateActivity(){
        let activity: IActivity = {
            description: this.state.activityDescription, 
            beginningTime: this.state.beginningTime,
            endingTime: this.state.endingTime
        };

        ActivityService.CreateActivity(activity).then((responseActivity : IActivity)=>{
    
            let groupActivity : IGroupActivity = {
                groupId: this.getFromChoosen(), 
                activityId: responseActivity.idActivity
            };

            ActivityService.AddActivityToAGroup(groupActivity).then((responseActivity : IActivity)=>{
                    console.log(responseActivity);
            },

            (error) =>{
                this.setState({
                    message: "Error when trying create Activity!"
                });
            });

        },
    
        (error) =>{
            this.setState({
                message: "Error when trying create Activity!"
            });
        });
    }

    render(){

        return(
                <form id='modal' className='modul-content animate'>

                <h1 className='title' > Create Activity </h1>


                <span className="close" onClick={(event) => {this.handleModal()}} title="Close Modal">&times;</span>
        
                <div className="container">
                <label><b>Activity Description</b></label>
                <br/>
                <input value={this.state.activityDescription} onChange={(event) => {this.handleActivityDescriptionChanged(event)}} type="text" required/>
                <label><b>Beginning Time</b></label>
                <br/>
                <input value={this.state.beginningTime} onChange={(event) => {this.handleBeginningTimeChanged(event)}} type="datetime-local" required/>
                <label><b>Ending Time</b></label>
                <br/>
                <input  value={this.state.endingTime} onChange={(event) => {this.handleEndingTimeChanged(event)}} type="datetime-local" required/>
                </div>

                <div>
                <MultiSelectField/>
                </div>

                <div className="container" >
                <button type="button" onClick={(event) => {this.handleModal()}} className="cancelbtn">Cancel</button>
                <button className='createButton' onClick={(event) => {this.handleCreateActivity()}} type="button">Create</button>
                </div>
            </form>
        );

}
}