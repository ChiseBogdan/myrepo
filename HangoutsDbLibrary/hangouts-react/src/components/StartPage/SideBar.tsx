import * as React from 'react';

import './SideBar.css';

import {GroupCreate} from './GroupCreate'
import {ActivityCreate} from './ActivityCreate'

import * as Cookies from 'js-cookie';


export interface SideBarProps {
    idUser:number;
}

interface SideBarState {    
    showComponent: string,
    value: never[]
}

export class SideBar extends React.Component<SideBarProps, SideBarState>{

    constructor(props: SideBarProps){
        super(props);

        this.state = {
            showComponent : 'Nothing',
            value: []
        }

    }

    componentWillReceiveProps(newprops: SideBarProps){
        if(this.props!=newprops){
            this.props = newprops;
        }
    }

    handleCreateComponent(component: string) {
        this.setState({showComponent: component});
        Cookies.set(component, '1');
    }


    render(){


        return(
            
        <div>
            <div className='sidenav'>
                <a href='/Activities'>Activities</a>
                <br></br>
                <a href='/Groups'>Groups</a>
                <p> Create </p>
                <div className='menuBox'>
                    <a onClick={(event) => this.handleCreateComponent('GroupCreate')} id='groupCreate'>Group</a>
                    <a onClick={(event) => this.handleCreateComponent('EventCreate')} id='eventCreate'>Activities</a>
                </div>
            </div>
            <div>
            { ((this.state.showComponent == 'GroupCreate') ?
            <GroupCreate idUser={this.props.idUser}/> :
            null)
            }
            </div>

            <div>
            { ((this.state.showComponent == 'EventCreate') ?
            <ActivityCreate /> :
            null)
            }
            </div>
        
        </div>
        );
    }

    


}