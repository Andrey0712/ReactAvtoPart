import {useEffect} from "react";
import {useDispatch} from "react-redux";
import {useSelector} from "react-redux";
import http from "../../http_common";
import { GetUser } from '../../actions/users';

const UsersPage = () => {
    const dispatch = useDispatch();
    const {users}=useSelector(state=>state.user);

    useEffect(()=>
    {
        dispatch(GetUser());
        console.log("UseEffect done:");
    },[]);
    return(
        <div>            
            <table className="table">
                <thead className="table table-bordered">
                    <tr>
                        <th scope="col">Image</th>
                        <th scope="col">Name</th>
                        <th scope="col">E-mail</th>
                    </tr>
                </thead>
                <tbody>
                    {users && users.map((item) =>
                            <tr key={item.email}>
                                <td>
                                    <img src={http.defaults.baseURL + item.photo}
                                        alt="user photo"
                                        width="100"
                                    />
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