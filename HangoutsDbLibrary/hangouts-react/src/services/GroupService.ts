import axios from 'axios';

import { IGroup } from './../models/IGroup';
import { IUserGroup } from '../models/IUserGroup';

import * as Cookies from 'js-cookie';

export class GroupService {
    private static root: string = 'http://localhost:52104/api/Group/';


    public static GetAllGroups(): Promise<IGroup[]> {
        return new Promise((resolve, reject) => {
            axios.get(this.root).then((response: any) => {
                
                let groups= response.data.values.map(this.toGroup);
                resolve(groups);
            },
                (error: any) => {
                    reject(error);
                })
        });
    }

    public static GetGroupById(id: number) :Promise<IGroup> {
        let URL = this.root + id;
        return new Promise((resolve, reject) => {
            axios.get(URL).then((response: any) => {
                
                let group= this.toGroup(response.data);
                resolve(group);
            },
                (error: any) => {
                    reject(error);
                })
        });
    }

    public static GetGroupsFromAUser() : Promise<IGroup[]>{
        
        let idUser = parseInt(Cookies.get('idUser')!.toString());
        let URL = this.root + 'User/' + idUser;
        var Output : IGroup[] = [];

        return new Promise((resolve, reject) => {
            axios.get(URL).then((response: any) => {
                
                response.data.values.map(function(singularValue: any){
                    let group : IGroup = {
                        name: singularValue.value.name,
                        idGroup: singularValue.value.idGroup
                    }

                    Output.push(group);
                });

                resolve(Output);
            },
                (error: any) => {
                    reject(error);
                })
        });
    }

     public static AddUserToGroup(userGroup : IUserGroup): Promise<any> {

        let URL = 'http://localhost:52104/api/UserGroup/';
        return new Promise((resolve, reject) => {
            axios.post(URL, userGroup).then((response: any) => {
                
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                })
        });
    }

    public static DeleteGroup() : Promise<any>{
        let idGroup = Cookies.get('idGroup');
        let URL = this.root + idGroup;

        return new Promise((resolve, reject) => {
            axios.delete(URL).then((response: any) => {
                
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                })
        });
    }

    private static getTen(numar: number) : number{
        let x=1;
        while(numar>=1){
            x*=10;
            numar/=10;
        }
        return x*10;
    }

    public static RemoveUserFromGroup(): Promise<any>{
        
        
        let idGroup = Cookies.get('idGroup');
        let idUser = Cookies.get('idUser');

        let intUserId = parseInt(idUser!.toString());
        let intGroupId = parseInt(idGroup!.toString());

        let finalId = intUserId*this.getTen(intGroupId) + intGroupId;

        let URL = 'http://localhost:52104/api/User/Group/' + finalId;


        return new Promise((resolve, reject) => {
            axios.delete(URL).then((response: any) => {
                
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                })
        });
    }

    public static CreateGroup(group: IGroup) : Promise<IGroup> {
        return new Promise((resolve, reject) => {
            axios.post(this.root, group).then((response: any) => {
                
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                })
        });
    }

    private static toGroup(responseCharacter: any): IGroup {
        return {
            name: responseCharacter.value.name,
            idGroup: responseCharacter.value.idGroup
        };
    }
}