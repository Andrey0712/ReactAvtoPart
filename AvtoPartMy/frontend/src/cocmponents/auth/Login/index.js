import React, {useRef,useState } from 'react'
import { Formik,Form } from 'formik'
import TextInput from '../../common/MyTextInput'
import  validateLog  from './validation'
import { useDispatch } from 'react-redux'
import { useSelector } from 'react-redux'
import EclipseWidget from '../../common/louding';
import { LoginUser, isRole } from '../../../actions/LoginUser';
import jwt from 'jsonwebtoken';
import { push } from 'connected-react-router';

const Login =()=> {

    const initState = {
        email: '',
        password: ''
    }


const dispatch = useDispatch();
const { loading } = useSelector(state => state.auth);
const refFormik = useRef();
const [invalid, setInvalid] = useState([]);
const titleRef = useRef();

const onSubmitHandler = async (values) => {

    try {
        
        
        const formData = new FormData();
        Object.entries(values).forEach(([key, value]) => formData.append(key, value));
        dispatch(LoginUser(formData));
        
        
    }
    catch (problem) {
        //обробка помилок валідації на стороні сервера.
        var err = problem.response.data.errors;
        console.log("Error valid",err); 
             
   }

}

// const onSubmitHandler=(values) => {
//     try {            
       
//         dispatch(LoginUser(values))
//             .then(result => {
//                 let user =jwt.decode(result);
//                 if (isRole(user, 'admin')) {
//                     dispatch(push("/admin"));
//                     return;
//                 }
//                 dispatch(push("/"));
//             })
//             .catch(ex => {
//                 console.log("exception: ", ex);
//                 setInvalid(ex.errors.invalid);
//                 titleRef.current.scrollIntoView({ behavior: 'smooth' })
                
//             });
//     }
//     catch (error) {
//         console.log("Server is bad register from", error);
//     }
// }

// const {errorvalid} = useSelector(res=>res.valid);
// console.log("Error valid",errorvalid);

return (

    <div className="row">
        <div className="offset-md-3 col-md-6">
            <h1 className="text-center">Login</h1>

            <Formik
                innerRef = {refFormik}
                initialValues={initState}
                validationSchema={validateLog()}
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

                    
                    <button type="submit" className="btn btn-primary">Підтвердити</button>
                </Form>
            </Formik>
        </div>

        {loading && <EclipseWidget />}
    </div>
  )

 }



export default Login;