import { REGISTER_AUTH,LOGIN_AUTH,LOG_OUT,REGISTER_BEGIN } from "../constants/actionTypes";


const initialState ={
    isAuth: false,
    username: "",
    role:"",
    Loading:false
}


function authReducer(state = initialState, action) {
    const {type, payload} = action;
    console.log("reducer data", payload);

    switch(type){
        case REGISTER_AUTH:
            {
                return {
                    isAuth: true,
                    username: payload.name,
                    role:payload.roles,
                    load:false
                }
            }
           case LOGIN_AUTH:  
         {
            return {
                isAuth: true,
                username: payload.name,
                role:payload.roles,
                load:false
            }
        }
        case LOG_OUT: {
            return {
                isAuth: false,
                username: "",
                role:""
            }
        }
        case REGISTER_BEGIN:{
            return{
                ...state,
                Loading:true

            }
        }

    }
    return state;
}

export default authReducer;