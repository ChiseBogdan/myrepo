//import * as Cookies from 'js-cookie';

import * as React from 'react';

import { IActivity } from '../../models/IActivity';
import { ActivityService} from './../../services/ActivitiesService'

import './OneActivity.css'

export interface OneActivityProps {
    idActivity: string
}

interface OneActivityState {  
    message: string;
    activity: IActivity;

}

export class OneActivity extends React.Component<OneActivityProps, OneActivityState>{

    constructor(props: OneActivityProps){
        super(props);
        
        this.state = {
            message: 'Nothing done',
            activity: {
                description: '', 
                beginningTime: '',
                endingTime: ''
            }
        };

        this.getActivity();

    }

    componentDidMount(){
        // var idActivity = Cookies.get('idActivity');
        // var intIdActivity = parseInt(idActivity!.toString());
        // ActivityService.GetActivityById(intIdActivity).then((responseActivity : IActivity)=>{
        //     this.setState({activity: responseActivity});
        // },
        
        // (error) =>{
        //     console.log('Error');
        // });
    }

    componentWillReceiveProps(newprops: OneActivityProps) {
        if(newprops!== this.props){
            this.props = newprops;
        }

        this.getActivity();
    }

    getActivity() {
        var idActivity = this.props.idActivity;
        var intIdActivity = parseInt(idActivity!.toString());
        ActivityService.GetActivityById(intIdActivity).then((responseActivity : IActivity)=>{
            this.setState({activity: responseActivity});
        },
        
        (error) =>{
            console.log('Error');
        });
    }

    
    render(){      

        return(
            <div className='myActivityContainer'>
                <span>
                    <b>Activity description: </b>  {this.state.activity.description} 
                         <br></br>
                         <b>Ending time: </b>  {this.state.activity.beginningTime} 
                         <br></br>
                         <b>Beginnig time: </b>  {this.state.activity.endingTime} 
                         <br></br>
                </span>
            </div>
        );
    }


}
