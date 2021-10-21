
import './App.css';
import React from 'react';
import {
 
  Switch,
  Route
} from "react-router-dom";
import  Header  from './cocmponents/header';
import RegisterPage from './cocmponents/auth/Register';
import LoginPage from './cocmponents/auth/Login';
import UsersPage from './cocmponents/userlist';
 import HomePage from './cocmponents/home';



 function App() {
  return (
    <>
        <Header />
        <div className="container">
          <Switch>
          <Route exact path="/">
              <HomePage />
            </Route>

            <Route exact path="/register">
              <RegisterPage />
            </Route>

            <Route exact path="/login">
              <LoginPage />
            </Route>

            <Route exact path="/users">
              <UsersPage />
              </Route> 

          </Switch>
        </div>
      </>
  
  );
  
}


export default App;
