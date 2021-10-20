// import React, { Component } from 'react'
// import { withRouter } from "react-router-dom";
// import authService from '../../../services/auth.servie';
// import TextBoxField from '../../common/TextBoxField';
// import ReactDOM from 'react-dom';
// import classnames from 'classnames';

// export class RegisterPage extends Component {
//     state = {
//         email: '',
//         phone: '',
//         firstName: '',
//         secondName: '',
//         password: '',
//         confirmpassword: '',
//         isvalid: true,
//         errormessage: {
//             email:'',
//             phone:'',
//             password:'',
//             confirmpassword:'',
//             firstName: '',
//             secondName: ''
//         }     
//     }

//     onChangeHandler = (e) => {
//         //console.log("onChange name", e.target.name);
//         //console.log("onChange value", e.target.value);
//         this.setState({[e.target.name]: e.target.value});//меняем state (название и его значение)
//     }

//     onSubmitFormHandler = async (e) => {
//         e.preventDefault();
//         console.log("Посилаємо на сервер", this.state);
//         try{
//             const result = await authService.register(this.state);
//             console.log("Server is good ", result);
//             this.props.history.push("/");
//         }
//         catch(error) {
//             // console.log("Server is bad ", error.response);

//             let answer_errors={
//                 email:'',
//                 phone:'',
//                 password:'',
//                 confirmpassword:'',
//                 firstName: '',
//                 secondName: ''
//             };

//             var res = error.response.data.errors;
           
           
//             if(res.Email)
//             {
//                 let str = "";
//                 res.Email.forEach(element => {
//                     str += element + " ";
//                     console.log(element);
//                 });
//                 answer_errors.email = str;
//             }
           
//             if(res.Phone)
//             {
//                 let str = "";
//                     res.Phone.forEach(element => {
//                         str += element + " ";
//                         console.log(element);
//                     });
//                     answer_errors.phone = str;
//            }

//            if(res.FirstName)
//            {
//                let str = "";
//                    res.FirstName.forEach(element => {
//                        str += element + " ";
//                        console.log(element);
//                    });
//                    answer_errors.firstName = str;
//            }

//            if(res.SecondName)
//            {
//                let str = "";
//                    res.SecondName.forEach(element => {
//                        str += element + " ";
//                        console.log(element);
//                    });
//                    answer_errors.secondName = str;
//            }

//            if(res.Password)
//            {
//                let str = "";
//                    res.Password.forEach(element => {
//                        str += element + " ";
//                        console.log(element);
//                    });
//                    answer_errors.password = str;
//            }

//            if(res.ConfirmPassword)
//            {
//                let str = "";
//                    res.ConfirmPassword.forEach(element => {
//                        str += element + " ";
//                        console.log(element);
//                    });
//                    answer_errors.confirmpassword = str;
//            }           
           
//              this.setState({errormessage:answer_errors});
//              console.log(this.state.errormessage.confirmpassword);
//         }
//     }

//     render() {
//     const { email, phone, firstName, secondName, password, confirmpassword,errormessage} = this.state;
//         return (
//             <div className="row">
//                 <div className="offset-md-3 col-md-6">
//                 <h1 className="text-center">Реєстрація</h1>
//                 <form className="row g-3 needs-validation" onSubmit= {this.onSubmitFormHandler}>
//                     <TextBoxField 
//                         field="email"
//                         label="Електронна пошта"
//                         value={email}
//                         onChangeHandler={this.onChangeHandler}
//                         isvalid={errormessage.email.length == 0? true : false}
//                         />
//                          {!!errormessage.email && <span className="text-danger">
//                              {errormessage.email}</span>}

//                              <TextBoxField 
//                         field="phone"
//                         label="Телефон"
//                         value={phone}
//                         onChangeHandler={this.onChangeHandler}
//                         isvalid={errormessage.phone.length == 0? true : false}
//                         />
//                         {!!errormessage.phone && <span className="text-danger">
//                             {errormessage.phone}</span>}

//                     <TextBoxField 
//                         field="secondName"
//                         label="Прізвище"
//                         value={secondName}
//                         onChangeHandler={this.onChangeHandler}
//                         isvalid={errormessage.secondName.length == 0? true : false}
//                         />
//                         {!!errormessage.secondName && <span className="text-danger">
//                             {errormessage.secondName}</span>}

//                     <TextBoxField 
//                         field="firstName"
//                         label="Ім'я"
//                         value={firstName}
//                         onChangeHandler={this.onChangeHandler}
//                         isvalid={errormessage.firstName.length == 0? true : false}
//                         />
//                         {!!errormessage.firstName && <span className="text-danger">
//                             {errormessage.firstName}</span>}

//                     <TextBoxField 
//                         field="password"
//                         type="password"
//                         label="Пароль"
//                         value={password}
//                         onChangeHandler={this.onChangeHandler}
//                         isvalid={errormessage.password.length == 0? true : false}
//                         />
//                         {!!errormessage.password && <span className="text-danger">
//                             {errormessage.password}</span>}

//                     <TextBoxField 
//                         field="confirmpassword"
//                         type="password"
//                         label="Підтвердження пароля"
//                         value={confirmpassword}
//                         onChangeHandler={this.onChangeHandler}
//                         isvalid={errormessage.confirmpassword == 0? true : false}
//                         />
//                         {!!errormessage.confirmpassword && <span className="text-danger">
//                             {errormessage.confirmpassword}</span>}
                    
//                     <button type="submit" className="btn btn-primary">Реєстрація</button>
//                 </form>
//                 </div>

//             </div>
//         )
//     }
// }

// export default withRouter(RegisterPage)

import React, {useRef} from 'react'
import { Formik, Form } from 'formik'
import TextInput from '../../common/MyTextInput'
import { useDispatch } from 'react-redux';
import validate from './validation'
import { ERRORS } from '../../../constants/actionTypes';
import { useSelector } from 'react-redux'
import MyPhotoInput from '../../common/MyPhotoInput';
import { RegisterUser } from '../../../actions/RegisterUser';
import EclipseWidget from '../../common/louding';

const Register = () => {

    const initState = {
        email: '',
        name: '',
        photo: null,
        password: '',
        confirmpassword: ''          

    }

    //const history = useHistory();
    const dispatch = useDispatch();
    const { loading } = useSelector(state => state.auth);
    const refFormik = useRef();

    const onSubmitHandler = async (values) => {

        try {
            // console.log("submit data ", values);

            // console.log("Server submit file", JSON.stringify(
            //     { 
            //       fileName: values.photo.name, 
            //       type: values.photo.type,
            //       size: `${values.photo.size} bytes`
            //     }
            //   ));

                    // const result = await authServie.register(values);
                
                    // console.log("Відправлені дані: ", values);
                    // console.log("Result data:",result.data.token);

                    // var jwt_token=result.data.token;

                    // var verified = jwt.decode(jwt_token);            
                    // console.log("Verified.roles:",verified.roles);
                    // dispatch({type: REGISTER_AUTH, payload: verified});

            // var formData = new FormData();
            // formData.append("email", values.email);
            // formData.append("password", values.password);
            // formData.append("photo", values.photo);

            // const result = await authService.register(formData);
            // console.log("Server is good ", result);
            // dispatch({type: REGISTER, payload: values.email});
            // history.push("/");
            
            const formData = new FormData();
            Object.entries(values).forEach(([key, value]) => formData.append(key, value));
            dispatch(RegisterUser(formData));
            //const result =  await dispatch(RegisterUser(formData));
            //history.push("/");
           
            // localStorage.setItem('Current user',jwt_token);
            // console.log("Local:",localStorage);
            // authTokenRequest(jwt_token);
            // history.push("/");
        }
        catch (problem) {
            //обробка помилок валідації на стороні сервера.
            var res = problem.response.data.errors;
                   
            console.log("Errors:",res);
            let answer_errors={
                    email:'',                    
                };

            if (res.Email) {
                let str = "";
                res.Email.forEach(element => {
                    str += element + " ";
                   // console.log(element);
                });
                answer_errors.email = str;
            }
            dispatch({type:ERRORS,payloads:answer_errors.email});          
       }

    }


    const {errorvalid} = useSelector(res=>res.valid);
    console.log("Error valid",errorvalid);

    return (

        <div className="row">
            <div className="offset-md-3 col-md-6">
                <h1 className="text-center">Реєстрація</h1>

                <Formik
                    innerRef = {refFormik}
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
                         {!!errorvalid &&<span className="text-danger">{errorvalid}</span> }

                        <TextInput
                            label="Name"
                            name="name"
                            id="name"
                            type="text"
                        />
                        <MyPhotoInput
                        refFormik={refFormik}
                        field="Photo"/>

                        <TextInput

                            label="Password"
                            name="password"
                            id="password"
                            type="password"
                        />

                        <TextInput
                            label="Confirm password"
                            name="confirmpassword"
                            id="confirmpassword"
                            type="password"
                        />
                        <button type="submit" className="btn btn-primary">Реєстрація</button>
                    </Form>
                </Formik>
            </div>

            {loading && <EclipseWidget />}
        </div>

    )

}

export default Register;
