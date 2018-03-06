import * as Cookies from 'js-cookie';

import * as React from 'react';

import { GroupService} from './../../services/GroupService'
import {IUserGroup} from './../../models/IUserGroup'

import './OneGroup.css'
import { IGroup } from '../../models/IGroup';

export interface OneGroupProps {
}

interface OneGroupState {  
    message: string;
    group: IGroup;

}

export class OneGroup extends React.Component<OneGroupProps, OneGroupState>{

    constructor(props: OneGroupProps){
        super(props);
        
        this.state = {
            message: 'Nothing done',
            group: {
                name: ''
            }
        };

    }

    componentDidMount(){
        var idGroup = Cookies.get('idGroup');
        var intIdGroup = parseInt(idGroup!.toString());
        GroupService.GetGroupById(intIdGroup).then((responseGroup : IGroup)=>{
            this.setState({group: responseGroup});
        },
        
        (error) =>{
            console.log('Error');
        });
    }

    handleJoinGroup(){

        let idGroup = Cookies.get('idGroup');
        let idUser = Cookies.get('idUser');


        let userGroup :IUserGroup = {
            groupId : parseInt(idGroup!.toString()),
            userId: parseInt(idUser!.toString())
        }
        GroupService.AddUserToGroup(userGroup).then((responseAddUserGroup : any)=>{
            
            console.log(responseAddUserGroup);
            window.location.reload();
        
        },
    
        (error) =>{
            this.setState({
                message: "Error when trying to add a User to a Group!"
            });
        });


    }

    
    render(){      

        return(
            <div className='myActivityContainer'>
                <span>
                    <b>Name: </b>  {this.state.group.name} 
                    <br></br>
                    <button onClick={(event) => this.handleJoinGroup()}>Join Group</button>
                </span>
            </div>
        );
    }


}
