import {useEffect} from "react";
import {useDispatch} from "react-redux";
import {useSelector} from "react-redux";
import http from "../../http_common";
import { getProduct } from '../../actions/products';
import productsService from "../../services/product.service";


const HomePage = () => {

    // return (
    //     <>
    //         <h1>Головна сторінка</h1>
    //     </>
    // )


    const dispatch = useDispatch();
    //const list = useSelector(state => state.users.list);
    const { products } = useSelector(state => state.user);
     
    useEffect(()=>
    {
        dispatch(getProduct());
        console.log("Request to server");
    },[]);
    console.log("Render component users")
    return(
              
            
            <div className="offset-2 col-md-8">
                <h1>Список продуктів</h1>    
            <table className="table table-striped ">
                <thead className="thead-dark">
                    <tr>
                    
                        <th scope="col">Фото</th>
                        <th scope="col">Назва</th>
                        <th scope="col">Ціна</th>
                        
                    </tr>
                </thead>
                <tbody>
                {                    
                  products && products.map((item, index) => 
                  <tr id={item.id} key={index}>
                                <td>
                                {/* <img width="60" height="60" src={'/images/' + item.image} alt="no image"/> `/edit/${item.email}` */  }
                                    <img src={http.defaults.baseURL+item.photo} alt="no foto" width="60" height="60"/>
                                </td>
                                <td>{item.name}</td>
                                <td>{item.price}</td>
                                
                            </tr>)


                
                


                }
                </tbody>
            </table>
        </div>
    )

}

export default HomePage