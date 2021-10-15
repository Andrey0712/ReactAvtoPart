
import jwt from 'jsonwebtoken';
import { REGISTER_AUTH, REGISTER_BEGIN } from '../constants/actionTypes';
import authServie from '../services/auth.servie';
import authTokenRequest from '../services/auth_request';
import {push} from 'connected-react-router';



export const RegisterUser=(model)=>async(dispatch)=>{

    try {

        // dispatch({type: REGISTER_BEGIN});
        // const result = await authServie.register(model);
        // dispatch({type: REGISTER_AUTH, payload: model.email});
        // dispatch(push("/"));

         //return Promise.resolve(result);

        //dispatch({type:REGISTER_BEGIN});
        const result = await authServie.register(model);  
        //console.log("Result:",result.data);
        //console.log("Result data token:",result.data.token);
        var jwt_token=result.data.token;
        var verified = jwt.decode(jwt_token); 
        dispatch({type: REGISTER_AUTH, payload: verified});//here
       // console.log("Verified.roles:",verified.roles);
        localStorage.setItem('Current user',jwt_token);
        authTokenRequest(jwt_token);
        //dispatch({type: REGISTER_AUTH, payload: verified});      
        dispatch({type:REGISTER_BEGIN});
         setTimeout(() => {
          dispatch(push("/"));
         }, 2000);
       
    }
    catch(error) {
        console.log("Problem register",error);

        //return Promise.reject(err);
    }

}