
import jwt from 'jsonwebtoken';
import { REGISTER_AUTH, REGISTER_BEGIN, REGISTER_FAILED,LOG_OUT,LOGIN_AUTH } from '../constants/actionTypes';
import authServie from '../services/auth.servie';
import setAuthorizationToken from '../Utils/setAuthorizationToken';
//import authTokenRequest from '../services/auth_request';
//import {push} from 'connected-react-router';
//import setAuthorizationToken from '../utils/setAuthorizationToken';



export const RegisterUser=(model)=>async(dispatch)=>{

    try {

        dispatch({type: REGISTER_BEGIN});
        const result = await authServie.register(model);
        const token = result.data.token;
        console.log("register reuslt", result);
        dispatch({type: REGISTER_AUTH});
        localStorage.authToken = token;
        dispatch(authUser(token));
        return Promise.resolve(result);
    }
    catch(err) {
        const{data}=err.response;
        //console.log("Register error", err.response.data);
        dispatch({type:REGISTER_FAILED});
        //console.log("Problem register");

        return Promise.reject(data);
    }

    
}
export const authUser = (token) => (dispatch) => {
        var user = jwt.decode(token);
        setAuthorizationToken(token);
        dispatch({type: LOGIN_AUTH, payload: user});
    }
    
    
    export const logoutUser = () => (dispatch) => {
        localStorage.removeItem('authToken');
        dispatch(
            {
                type: LOG_OUT
            }
        );}