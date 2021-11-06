import { PRODUCTS } from "../constants/actionTypes";
import productService from "../services/product.service";


export const getProduct= () => async (dispatch) => {
    try {
        const res = await productService.get_list_prod();
        dispatch({
            type: PRODUCTS,
            data: res.data
        });
    } catch(err) {
        console.log(err);
    }
}