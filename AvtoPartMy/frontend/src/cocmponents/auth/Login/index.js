import React from 'react'
import { Formik,Form } from 'formik'
import TextInput from '../../common/MyTextInput'
import  validate  from './validation'
import { LOGIN_AUTH } from '../../../constants/actionTypes'
import { useDispatch } from 'react-redux'
import { useHistory } from 'react-router-dom';
import jwt from 'jsonwebtoken';
import authTokenRequest from '../../../services/auth_request';
import authServie from '../../../services/auth.servie'

const Login =()=> {

    const initState = {
        email: '',
        password: ''
    }

    const dispatch = useDispatch();    
    const history = useHistory();

    const onSubmitHandler= async(values) => {

        try {

            const formData = new FormData();
              formData.append("email", values.email);
              formData.append("password", values.password);
            //Object.entries(values).forEach(([key, value]) => formData.append(key, value));
            const result = await authServie.login(formData);
          
             
            
              const {token} = result.data;
              localStorage.authToken=token;
              const authUser = (token, dispatch) => {
                var user = jwt.decode(token);
                console.log("user auth: ", user);
                authTokenRequest(token);
                dispatch({type: LOGIN_AUTH, data: user});}
              authUser(token, dispatch);
              history.push("/");
            
            // console.log("Відправлені дані: ", values);
            // console.log("Result data:", result.data.token);

            // var jwt_token = result.data.token;
            // var verified = jwt.decode(jwt_token);
            // console.log("Verified:",verified);
            // console.log("Verified.roles:", verified.roles);

            // dispatch({ type: LOGIN_AUTH, payload: verified });
            // localStorage.setItem('Current user', jwt_token);         
                   
            // authTokenRequest(jwt_token);
            // history.push("/");

        }
        catch (errors) {
            var res = errors.response.data.errors;                   
            //console.log("Errors:",res);

        }

    }
   
        return (

            <div className="row">
                <div className="offset-md-3 col-md-6">
                    <h1 className="text-center">Вхід на сайт</h1>
                    <Formik
                        initialValues={initState}
                        validationSchema={validate()}
                        onSubmit={onSubmitHandler}
                    >
                        <Form>
                            <TextInput
                                label="Email"
                                name="email"
                                id="email"
                                type="text"
                            />

                            <TextInput
                                label="Password"
                                name="password"
                                id="password"
                                type="password"
                            />

                          <input type="submit" className="btn btn-success" value="Вхід"></input>
                        </Form>
                    </Formik>
                </div>
            </div>

           
        )
    
}

export default Login;