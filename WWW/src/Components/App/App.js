import React, { Component } from 'react';
import { Router, Route, browserHistory } from 'react-router';
import './App.css';
import Layout from './layout.js';
import dataProvider from '../Core/dataProvider.js';
import Search from '../Search/index.js';

class App extends Component {

  constructor(props) {
      console.log('Init App component');
      super(props);
      this.settings = props.settings;
      this.provider = new dataProvider(props);
      console.log('end');
  }

  render() {
      return (
          <Router history={ browserHistory }>
              <Route component={ Layout }>
                  <Route provider={ this.provider } settings={ this.settings } path="/" component={ Search }/>
              </Route>
              
          </Router>
    );
  }
}

export default App;
