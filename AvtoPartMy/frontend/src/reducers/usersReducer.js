import { USERS } from "../constants/actionTypes";


const initialState ={
   list:[]
}

function usersReducer(state = initialState, action)
{
    const{ type, payload }=action;
    console.log("Reducer user data :", payload);

    switch(type)
    {
        case USERS: {
            return {               
               list:payload
            }            
        }
    }
    return state;
}
export default usersReducer;