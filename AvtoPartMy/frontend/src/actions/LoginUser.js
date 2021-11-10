
import jwt from 'jsonwebtoken';
import { LOGIN_AUTH, REGISTER_BEGIN, SET_ROLE } from '../constants/actionTypes';
import authServie from '../services/auth.servie';
import authTokenRequest from '../services/auth_request';
import {push} from 'connected-react-router';
//import setAuthorizationToken from '../Utils/setAuthorizationToken';
//import jwt from "jsonwebtoken";

// export const LoginUser = (model) => async (dispatch) => {

//     try {
//         dispatch({type: REGISTER_BEGIN});
//         const result = await authServie.login(model);
//         const token = result.data.token;
//         console.log("login reuslt", result);
//         localStorage.authToken = token;
//         dispatch(authUser(token));
//         return Promise.resolve(token);
        
//     }
//     catch(err) {
//         const {data} = err.response;
//         return Promise.reject(data);
//     }
// }

// export const authUser = (token) => (dispatch) => {
//     var user = jwt.decode(token);
//     setAuthorizationToken(token);
//     dispatch({type: LOGIN_AUTH, payload: user});
// }

export const LoginUser=(model)=>async(dispatch)=>{

    try {

        
        const result = await authServie.login(model);  
        
        var jwt_token=result.data.token;
        var verified = jwt.decode(jwt_token); 
        dispatch({type: LOGIN_AUTH, payload: verified});
        localStorage.setItem('Current user',jwt_token);
        authTokenRequest(jwt_token);  
        dispatch({type:REGISTER_BEGIN});
                 
         if(verified.roles=='admin')
         {
             console.log("roles: ",verified.roles);
         dispatch(push("/admin"));
         return;
         }
         
                // if (isRole(user, 'admin')) {
                //     dispatch(push("/admin"));
                //     return;
                // }
                else
                dispatch(push("/"));
                
    }
    catch(error) {
        console.log("Problem login",error);

        //return Promise.reject(err);
    }

    

}

export const isRole = (user, role) => {
        if(Array.isArray(user.roles)) {
            for(let i =0; i < user.roles.length; i++)
            {
                if(user.roles[i]==role)
                    return true;
            }
            return false;
        }
        else {
            return user.roles==role;
        }
    }