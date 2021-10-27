
import jwt from 'jsonwebtoken';
import { REGISTER_AUTH, REGISTER_BEGIN, REGISTER_FAILED } from '../constants/actionTypes';
import authServie from '../services/auth.servie';
import authTokenRequest from '../services/auth_request';
import {push} from 'connected-react-router';



export const RegisterUser=(model)=>async(dispatch)=>{

    try {

        dispatch({type: REGISTER_BEGIN});
        const result = await authServie.register(model);
        //const token = result.data.token;
        console.log("register reuslt", result);
        dispatch({type: REGISTER_AUTH,payload:model.email});
        // localStorage.authToken = token;
        // dispatch(authUser(token));
        return Promise.resolve(result);
    }
    catch(err) {
        const{data}=err.response;
        //console.log("Register error", err.response.data);
        dispatch({type:REGISTER_FAILED,payload:data.errors});
        //console.log("Problem register");

        return Promise.reject(data);
    }

}