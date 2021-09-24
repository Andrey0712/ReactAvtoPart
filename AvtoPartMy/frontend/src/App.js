import logo from './logo.svg';
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



class App extends React.Component{
  render() {
    return (
    <>
     <Router>
        <Header />
        <div className="container">
          <Switch>
            <Route exact path="/">
              <LoginPage />
            </Route>

            <Route exact path="/register">
              <RegisterPage />
            </Route>

          </Switch>
        </div>
      </Router>
    </>
  );
  }
}


export default App;
