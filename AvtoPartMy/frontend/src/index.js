import 'bootstrap/dist/css/bootstrap.css';
import 'font-awesome/css/font-awesome.min.css';
import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'connected-react-router';
import store, {history} from './store';
import {authUser} from './actions/RegisterUser';

const token = localStorage.authToken;

if(token) {
  store.dispatch(authUser(token));
}


ReactDOM.render(
    <Provider store={store}>
     <ConnectedRouter history={history}>
        <App />
      </ConnectedRouter>
    </Provider>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
