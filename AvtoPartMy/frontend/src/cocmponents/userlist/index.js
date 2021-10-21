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
        <div>        
            <h1>Список користувачів</h1>    
            <table className="table">
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
                                    <img src={http.defaults.baseURL+item.image} alt="my image" width="100"/>
                                </td>
                                <td>{item.user}</td>
                                <td>{item.email}</td>
                            </tr>)}
                </tbody>
            </table>
        </div>
    )
}

export default UsersPage;