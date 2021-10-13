//import logo from './logo.svg';
import './App.css';
import React from 'react';
import {
  BrowserRouter as Router,
  Switch,
  Route
} from "react-router-dom";
import  Header  from './cocmponents/header';
import RegisterPage from './cocmponents/auth/Register';
import LoginPage from './cocmponents/auth/Login';
 import HomePage from './cocmponents/home';



 function App() {
  return (
     <Router>
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

          </Switch>
        </div>
      </Router>
  
  );
  
}


export default App;
