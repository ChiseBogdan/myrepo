import { IUserConnected } from './../models/IUserConnected';
import axios from 'axios';

import { IUser } from './../models/IUser';

export class UserService {
    private static root: string = 'http://localhost:52104/api/User/';

    public static getUserById(id: number): Promise<IUser> {
        return new Promise((resolve, reject) => {
            axios.get(this.root + id).then((response: any) => {
                let character = this.toUser(response.data);
                resolve(character);
            },
                (error: any) => {
                    reject(error);
                })
        });
    }

    public static Login(user: IUserConnected): Promise<IUserConnected>{
        let Url = this.root + 'Login';
        return new Promise((resolve, reject) => {
            axios.put(Url, user).then((response: any) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                })
        });
    }

    public static getUsers(): Promise<IUser[]> {
        return new Promise((resolve, reject) => {
            axios.get(this.root).then((response: any) => {
                let users = response.data.values.map(this.toUser);
                resolve(users);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static createUser(user: IUser): Promise<IUser> {
            return new Promise((resolve, reject) =>{
            
                axios.post(this.root, user).then((response : any) => {
                    resolve(response.data);
                },
                (error: any) => {
                    reject(error);
                });
            });
    }

    private static toUser(responseCharacter: any): IUser {
        return {
            fullname: responseCharacter.value.fullname,
            username: responseCharacter.value.userName,
            password: responseCharacter.value.password,
            idUser: responseCharacter.value.idUser
        };
    }

    // private static toConnectedUser(responseCharacter: any): IUserConnected {
    //     return {
    //         username: responseCharacter.value.userName,
    //         password: responseCharacter.value.password
    //     };
    // }
}