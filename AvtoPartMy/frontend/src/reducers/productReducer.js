import { PRODUCTS } from "../constants/actionTypes";


const initialState = {
    list: []
};

function productReducer(products = initialState, action) {
    const {type, data} = action;
    switch(type){
        case PRODUCTS: {
            return {
                list: data
            }
            
        }
        default: 
            return products;
    }
}
export default productReducer;