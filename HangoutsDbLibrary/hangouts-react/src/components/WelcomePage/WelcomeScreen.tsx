import * as React from 'react';

import './WelcomeScreen.css';
import {  Redirect } from 'react-router';
import { Link } from 'react-router-dom';
import { IUser } from './../../models/IUser';
import {UserService} from './../../services/UserService'
import { IUserConnected } from '../../models/IUserConnected';


export interface SignUpProps {
}

interface SignUpState {
    message: string;
    username: string;
    fullname: string;
    password: string;
    selected: string;
    idUser: Number;
}

export class WelcomeScreen extends React.Component<SignUpProps, SignUpState>{

    constructor(props: SignUpProps){
        super(props);

        this.state = {
            message: 'Not signed up!',
            username: '',
            fullname: '',
            password: '',
            selected: 'SignUp',
            idUser: -1
        };

    }


    private login(event : any){
        event.preventDefault();

        let userToBeConnected : IUserConnected = {
            username : this.state.username,
            password : this.state.password
            
        };

        UserService.Login(userToBeConnected).then((responseToConnectUser : any)=>{
            this.setState({
                message : "A new User was connected!",
                username: "",
                fullname: "",
                password: "",
                idUser : responseToConnectUser

            });
            console.log(this.state.idUser);

        },

        (error) =>{
            this.setState({
                message: "Error when trying to connect a User!"
            });
        });

    }


    private signUp(event :any){
        event.preventDefault();

        let userToBeCreated : IUser = {
            username : this.state.username,
            fullname : this.state.fullname,
            password : this.state.password
            
        };

        UserService.createUser(userToBeCreated).then((responseToCreateUser : any)=>{
            this.setState({
                message : "A new User was created!",
                username: "",
                fullname: "",
                password: "",
                idUser : responseToCreateUser

            });

        },

        (error) =>{
            this.setState({
                message: "Error when trying to create a User!"
            });
        });

    }

    setActiveButtons(buttonName: string){
        this.setState({selected: buttonName});
    }

    isActive(value: string){
        return ('button' + ((this.state.selected===value) ? 'Active':'Nonactive' ));
    }

    handleUsernameChanged(event: any){
        this.setState({username: event.target.value});
    }

    handleFullnameChanged(event: any){
        this.setState({fullname: event.target.value});
    }

    handlePasswordChanged(event: any){
        this.setState({password: event.target.value});
    }

    renderLoginButton(): JSX.Element {
        return (
            <Link to={`/StartPage`}>
                <button className='loginButtons' type="button" onClick={(event) => this.login(event)} >Log in </button>
            </Link>
        );
    }

    renderSignUpButton(): JSX.Element {
        return (
            <button className='loginButtons' type="button" onClick={(event) => this.signUp(event)} >Sign Up </button>
        );
    }

    renderPanelUpButtons(): JSX.Element{

        return (
            <div className='myPanel'>
                <button type="button" className={this.isActive('SignUp')} onClick={(event) => this.setActiveButtons("SignUp")} >Sign Up</button>
                <button type="button" className={this.isActive('LogIn')} onClick={(event) => this.setActiveButtons("LogIn")} >Log In</button>
            </div>
        );

    }

    loginRender(): JSX.Element{
        return(
            <form>
            <input className='text-boxes' type="text" placeholder="Username" value={this.state.username} onChange={(event) => {this.handleUsernameChanged(event)}} required/>
            <br></br>
            <input className='text-boxes' type="password" placeholder="Password" value={this.state.password} onChange={(event) => { this.handlePasswordChanged(event)}} required/>
            {this.renderLoginButton()}
            </form>
        );
    }

    signUpRender(): JSX.Element{
        return(
            <form>
            <input className='text-boxes' type="text" placeholder="Fullname" value={this.state.fullname} onChange={(event) => {this.handleFullnameChanged(event)}} required/>
            <br></br>
            <input className='text-boxes' type="text" placeholder="Username" value={this.state.username} onChange={(event) => {this.handleUsernameChanged(event)}} required/>
            <br></br>
            <input className='text-boxes' type="password" placeholder="Password" value={this.state.password} onChange={(event) => { this.handlePasswordChanged(event)}} required/>
            {this.renderSignUpButton()}
            </form>
        );
    }

    renderHeader(): JSX.Element{
    return(<div className='head'></div>);
    }

    renderWelcomeMenu(): JSX.Element{

        if(this.state.selected==='SignUp' ){
            return(
                <div className='myPanelContent'> 
                    {this.signUpRender()}
                </div>  
            );
        }

        return (          
            <div className='myPanelContent'> 
                {this.loginRender()}
            </div>               
        );
    }
    
    render(){

        return (  
            this.state.idUser != -1 ?  <Redirect to={{
                pathname: '/Activities',
                state: { referrer: this.state.idUser }
                }}/> :   
            <div>
                {this.renderHeader()}
                <div className='screenUser'>
                    {this.renderPanelUpButtons()}
                    {this.renderWelcomeMenu()}
                </div>
            </div>
            
        );
    }
}