import * as React from 'react';

// import { BrowserRouter as Router } from 'react-router-dom';
import { Route } from 'react-router-dom';
import { WelcomeScreen } from './../WelcomePage/WelcomeScreen'
import { Groups } from '../Groups/Groups';
import { Activities } from '../Activity/Activities';
import { OneActivity } from './../Activity/OneActivity'

interface BodyProps {
}

interface BodyState {
}

export class Body extends React.Component<BodyProps, BodyState> {

    constructor(props: BodyProps) {
        super(props);

        this.state = {
        };
    }
    render() {
        return (
            <div className="hangouts-body">
                <div>
                    <Route path="/" exact={true} component={WelcomeScreen} />
                    <Route path="/Groups" exact={true} component={Groups} />
                    <Route path="/Activities" exact={true} component={Activities} />
                    <Route path="/OneActivity" exact={true} component={OneActivity} />
                </div>
            </div>
        );
    }
}
