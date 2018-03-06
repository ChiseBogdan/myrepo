import * as React from 'react';

import './App.css';

import { BrowserRouter as Router } from 'react-router-dom';

import { Body } from './components/Body/Body';


class App extends React.Component {

  render() {
    return (
      <Router>
            <Body/>
      </Router>
    );
  }

}

export default App;
