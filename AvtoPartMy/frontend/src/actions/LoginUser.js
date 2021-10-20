
import jwt from 'jsonwebtoken';
import { LOGIN_AUTH, REGISTER_BEGIN } from '../constants/actionTypes';
import authServie from '../services/auth.servie';
import authTokenRequest from '../services/auth_request';
import {push} from 'connected-react-router';



export const LoginUser=(model)=>async(dispatch)=>{

    try {

        // dispatch({type: REGISTER_BEGIN});
        // const result = await authServie.register(model);
        // dispatch({type: REGISTER_AUTH, payload: model.email});
        // dispatch(push("/"));

         //return Promise.resolve(result);

        
        const result = await authServie.login(model);  
        //console.log("Result:",result.data);
        //console.log("Result data token:",result.data.token);
        var jwt_token=result.data.token;
        var verified = jwt.decode(jwt_token); 
        dispatch({type: LOGIN_AUTH, payload: verified});
        localStorage.setItem('Current user',jwt_token);
        authTokenRequest(jwt_token);   
        dispatch({type:REGISTER_BEGIN});
        dispatch(push("/"));
                
    }
    catch(error) {
        console.log("Problem login",error);

        //return Promise.reject(err);
    }

}