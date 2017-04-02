import React, { Component } from 'react'
import Menu from '../Menu/Index.js'
import './layout.css'
import './elemental.css'

class Layout extends Component {
    constructor(props){
        console.log('Init Layout component');
        super(props);
        console.log('end');
    }
    render() {
        return (
            <div>
                <Menu/>
                <div id='container'>
                    { this.props.children }
                </div>
            </div>
        );
    }
}

export default Layout