import { IGroupActivity } from './../models/IGroupActivity';
import { IActivity } from './../models/IActivity';
import axios from 'axios';

import * as Cookies from 'js-cookie';

export class ActivityService {
    private static root: string = 'http://localhost:52104/api/Activity/';


    public static GetAllActivities(): Promise<IActivity[]> {
        return new Promise((resolve, reject) => {
            axios.get(this.root).then((response: any) => {
                
                let activities= response.data.values.map(this.toActivity);
                resolve(activities);
            },
                (error: any) => {
                    reject(error);
                })
        });
    }

    public static GetActivityById(id: number): Promise<IActivity> {
        
        return new Promise((resolve, reject) => {
            axios.get(this.root+id).then((response: any) => {
                
                let activity= this.toActivity(response.data);
                resolve(activity);
            },
                (error: any) => {
                    reject(error);
                })
        });
    }

    public static DeleteActivity() :Promise<any>{
        
        let idActivity = Cookies.get('idActivity');
        let URL = this.root + idActivity;

        return new Promise((resolve, reject) => {
            axios.delete(URL).then((response: any) => {
                
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                })
        });
    }

    public static AddActivityToAGroup(groupActivity: IGroupActivity ): Promise<any> {

        let URL = 'http://localhost:52104/api/GroupActivity/';

        return new Promise((resolve, reject) => {
            axios.post(URL, groupActivity).then((response: any) => {
                
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                })
        });
    }

    public static GetAllActivitiesFromAUser() : Promise <IActivity[]>{
        
        let idUser = parseInt(Cookies.get('idUser')!.toString());
        let URL = 'http://localhost:52104/api/Activity/User/' + idUser;
        
        return new Promise((resolve, reject) => {
            axios.get(URL).then((response: any) => {
                
                let activities = response.data.values.map(this.toActivity)
                resolve(activities);
            },
                (error: any) => {
                    reject(error);
                })
        });
    }

    public static CreateActivity(activity: IActivity) : Promise<IActivity> {
        return new Promise((resolve, reject) => {
            axios.post(this.root, activity).then((response: any) => {
                
                let activity= this.toActivity(response.data);
                resolve(activity);
            },
                (error: any) => {
                    reject(error);
                })
        });
    }

    private static toActivity(responseCharacter: any): IActivity {
        return {
            description: responseCharacter.value.description,
            beginningTime : responseCharacter.value.beginningTime,
            endingTime : responseCharacter.value.endingTime,
            idActivity: responseCharacter.value.idActivity
        };
    }
}