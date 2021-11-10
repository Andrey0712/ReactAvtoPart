import { REGISTER_AUTH,LOGIN_AUTH,LOG_OUT,REGISTER_BEGIN, REGISTER_FAILED,SET_ROLE} from "../constants/actionTypes";


const initialState ={
    isAuth: false,
    username: "",
    role:"",
    //user: {},
    loading:false
    
}


function authReducer(state = initialState, action) {
    const {type, payload} = action;
    console.log("reducer data", payload);

    switch(type){
        case REGISTER_AUTH:
            {
                return {
                    isAuth: false,
                    //role:payload.roles,
                    loading:false
                }
            }
           case LOGIN_AUTH:  
         {
            return {
                isAuth: true,
                username: payload.name,
                role:payload.roles,
                //user: payload,
                loading:false
            }
        }
        case LOG_OUT: {
            return {
                isAuth: false
                //username: "",
                //role:""
            }
        }
        case REGISTER_BEGIN:{
            return{
                ...state,
                loading:true

            }
        }
        case REGISTER_FAILED:{
            return{
                ...state,
                loading:false
                

            }
        }
        case SET_ROLE: {
            return { 
                ...state,
                role: action.payload
            }
        }
        default: {
            return state;
        }
    }
    // return state;
}

export default authReducer;