import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import './custom.css';
import SignIn from './components/SingIn';
import SignUp from './components/SingUp';
import Home from './components/Home';
import Start from './components/Start';

const isAuthorized = localStorage.getItem('token') ? true : false;

export default class App extends Component {
  static displayName = App.name;
  
  render() {
    return (
        <Routes>
          <Route path='/' element={<Home />} />
          <Route path='/signin' element={<SignIn />} />
          <Route path='/signup' element={<SignUp />} />
        </Routes>
    );
  }
}
