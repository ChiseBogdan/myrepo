import * as React from 'react';

import './OneActivity.css'

export interface NoChoosenProps {
}

interface NoChoosenState {  
    message: string;

}


export class NoChoosen extends React.Component<NoChoosenProps, NoChoosenState>{

    constructor(props: NoChoosenProps){
        super(props);
        
        this.state = {
            message: 'Nothing done'
        };

    }

    
    render(){       

        return(
            <div className='myActivityContainer'>
                <span>
                    <b>Nothing choosen</b>
                </span>
            </div>
        );
    }


}
