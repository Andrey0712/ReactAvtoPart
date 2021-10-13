import {applyMiddleware, combineReducers, createStore} from "redux";
import { composeWithDevTools } from "redux-devtools-extension";
import thunk from "redux-thunk";
import authReducer from "./reducers/authReducer";
import errorReducer from "./reducers/errorReducer";

const middleware = [thunk];

const rootReducer = combineReducers({
    auth: authReducer,
    valid:errorReducer,
});

const store = createStore(
    rootReducer,
    {},
    composeWithDevTools(applyMiddleware(...middleware))
);
export default store;