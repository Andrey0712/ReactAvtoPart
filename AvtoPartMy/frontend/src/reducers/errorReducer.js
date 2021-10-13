import { ERRORS } from "../constants/actionTypes";

const initialState = {
    errorvalid: ''
}

function errorReducer(state = initialState, action) {
    const { type, payloads } = action;
    console.log("Reducer errors", payloads);
    switch (type) {
        case ERRORS: {
            return {

                errorvalid: payloads
            }

        }


    }
    return state;
}

export default errorReducer;