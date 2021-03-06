
import React from 'react'
import { Link } from 'react-router-dom';
import { useSelector } from 'react-redux';
import {logoutservice} from '../../services/logout.service';
import {LOG_OUT} from '../../constants/actionTypes';
import { useHistory } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { logoutUser } from '../../actions/RegisterUser';
import { push } from 'connected-react-router';

const Header = () => {
    const history = useHistory();
    const dispatch = useDispatch();

   const logout=()=> {
    logoutservice.logout();
        dispatch({type: LOG_OUT});
        history.push('/');
    };
//или так

const onClickLogout = (e) => {
    e.preventDefault();
    dispatch(logoutUser());
    dispatch(push("/"));
}
    
        const {isAuth,username} = useSelector(res=>res.auth);
        const {cart} = useSelector(redux => redux);
   
console.log("Auth user info ", isAuth);
    return (
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <div className="container">
                <Link className="navbar-brand" to="/">Авто запчастини</Link>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                        <li className="nav-item">
                            <Link className="nav-link active" aria-current="page" to="/">Головна</Link>
                        </li>
                        {!isAuth ?
                        <li className="nav-item">
                        <Link className="nav-link" to="/register">Список юзеров доступен только после регистрации</Link>
                    </li>
                    :
                    <li className="nav-item">
                            <Link className="nav-link" to="/users">Список юзеров </Link>
                        </li>
                        }

                    </ul>

                    {!isAuth ?
                        <ul className="navbar-nav">
                            <li className="nav-item">
                                <Link className="nav-link" to="/login">Вхід</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link"  to="/register">Реєструватися</Link>
                            </li>
                           
                        </ul>
                        :
                        <ul className="navbar-nav">
                            <li className="nav-item">
                                <Link className="nav-link" to="/cart">
                                    <i className="pi pi-shopping-cart" style={ {fontSize: "2rem"}}></i>
                                    {cart.count}
                                </Link>
                            </li>
                             <li className="nav-item">
                                <Link className="nav-link" to="/profile">{username}</Link>
                            </li>
                            <li className="nav-item">
                                {/* <Link to="/" className="nav-link" onClick={logout}>Вихід</Link> */}
                                <Link to="/" className="nav-link" onClick={onClickLogout}>Вихід</Link>
                            </li>
                            
                        </ul>
                    }
                    

                </div>
            </div>
        </nav>
    )
}
export default Header



