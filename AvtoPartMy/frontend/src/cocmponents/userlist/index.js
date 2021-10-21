import {useEffect} from "react";
import {useDispatch} from "react-redux";
import {useSelector} from "react-redux";
import http from "../../http_common";
import { getUsers } from '../../actions/users';

const UsersPage = () => {
    const dispatch = useDispatch();
    //const list = useSelector(state => state.users.list);
    const { list } = useSelector(state => state.user);

    useEffect(()=>
    {
        dispatch(getUsers());
        console.log("Request to server");
    },[]);
    console.log("Render component users")
    return(
              
            
            <div className="offset-2 col-md-8">
                <h1>Список користувачів</h1>    
            <table className="table table-striped ">
                <thead className="thead-dark">
                    <tr>
                        <th scope="col">Фото</th>
                        <th scope="col">Имя</th>
                        <th scope="col">E-mail</th>
                    </tr>
                </thead>
                <tbody>
                {list &&
                        list.map((item, index) =>
                            <tr key={index}>
                                <td>
                                {/* <img width="60" height="60" src={'/images/' + item.image} alt="no image"/> */}
                                    <img src={http.defaults.baseURL+item.photo} alt="no image" width="60" height="60"/>
                                </td>
                                <td>{item.name}</td>
                                <td>{item.email}</td>
                            </tr>)}
                </tbody>
            </table>
        </div>
    )
}

export default UsersPage;