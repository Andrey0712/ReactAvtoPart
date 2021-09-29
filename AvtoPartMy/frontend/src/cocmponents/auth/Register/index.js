import React, { Component } from 'react'
import { withRouter } from "react-router-dom";
import authService from '../../../services/auth.servie';
import TextBoxField from '../../common/TextBoxField';
import ReactDOM from 'react-dom';
import classnames from "classnames";

export class RegisterPage extends Component {
    state = {
        email: '',
        phone: '',
        firstName: '',
        secondName: '',
        password: '',
        confirmpassword: '',
        isvalid: true,
        errormessage: {
            email:'',
            phone:'',
            password:'',
            confirmpassword:'',
            firstName: '',
            secondName: ''
        }     
    }

    onChangeHandler = (e) => {
        //console.log("onChange name", e.target.name);
        //console.log("onChange value", e.target.value);
        this.setState({[e.target.name]: e.target.value});//меняем state (название и его значение)
    }

    onSubmitFormHandler = async (e) => {
        e.preventDefault();
        console.log("Посилаємо на сервер", this.state);
        

        // authService.register(this.state)
        //     .then(result=>{
        //         console.log("Server is good ", result);
        //     },
        //     error => {
        //         console.log("Server is bad ", error);
        //     })
        //     .catch(errorServer => {

        //     });


        try{
            const result = await authService.register(this.state);
            console.log("Server is good ", result);
            this.props.history.push("/");
        }
        catch(error) {
            // console.log("Server is bad ", error.response);

            let answer_errors={
                email:'',
                phone:'',
                password:'',
                confirmpassword:'',
                firstName: '',
                secondName: ''
            };

            var res = error.response.data.errors;

            if(res.Email)
            {
                let str = "";
                res.Email.forEach(element => {
                    str += element + " ";
                    console.log(element);
                });
                answer_errors.email = str;
            }
           
            if(res.Phone)
            {
                let str = "";
                    res.Phone.forEach(element => {
                        str += element + " ";
                        console.log(element);
                    });
                    answer_errors.phone = str;
           }

           if(res.FirstName)
           {
               let str = "";
                   res.FirstName.forEach(element => {
                       str += element + " ";
                       console.log(element);
                   });
                   answer_errors.firstName = str;
           }

           if(res.SecondName)
           {
               let str = "";
                   res.SecondName.forEach(element => {
                       str += element + " ";
                       console.log(element);
                   });
                   answer_errors.secondName = str;
           }

           if(res.Password)
           {
               let str = "";
                   res.Password.forEach(element => {
                       str += element + " ";
                       console.log(element);
                   });
                   answer_errors.password = str;
           }

           if(res.ConfirmPassword)
           {
               let str = "";
                   res.ConfirmPassword.forEach(element => {
                       str += element + " ";
                       console.log(element);
                   });
                   answer_errors.confirmpassword = str;
           }           
           
             this.setState({errormessage:answer_errors});
             console.log(this.state.errormessage.confirmpassword);
        }
    }

    render() {
    const { email, phone, firstName, secondName, password, confirmpassword,errormessage} = this.state;
        return (
            <div className="row">
                <div className="offset-md-3 col-md-6">
                <h1 className="text-center">Реєстрація</h1>
                <form className="row g-3 needs-validation" onSubmit= {this.onSubmitFormHandler}>
                    <TextBoxField 
                        field="email"
                        label="Електронна пошта"
                        value={email}
                        onChangeHandler={this.onChangeHandler}
                        isvalid={errormessage.email.length == 0? true : false}
                        />
                         {!!errormessage.email && <span className="text-danger">
                             {errormessage.email}</span>}

                             <TextBoxField 
                        field="phone"
                        label="Телефон"
                        value={phone}
                        onChangeHandler={this.onChangeHandler}
                        isvalid={errormessage.phone.length == 0? true : false}
                        />
                        {!!errormessage.phone && <span className="text-danger">
                            {errormessage.phone}</span>}

                    <TextBoxField 
                        field="secondName"
                        label="Прізвище"
                        value={secondName}
                        onChangeHandler={this.onChangeHandler}
                        isvalid={errormessage.secondName.length == 0? true : false}
                        />
                        {!!errormessage.secondName && <span className="text-danger">
                            {errormessage.secondName}</span>}

                    <TextBoxField 
                        field="firstName"
                        label="Ім'я"
                        value={firstName}
                        onChangeHandler={this.onChangeHandler}
                        isvalid={errormessage.firstName.length == 0? true : false}
                        />
                        {!!errormessage.firstName && <span className="text-danger">
                            {errormessage.firstName}</span>}

                    <TextBoxField 
                        field="password"
                        type="password"
                        label="Пароль"
                        value={password}
                        onChangeHandler={this.onChangeHandler}
                        isvalid={errormessage.password.length == 0? true : false}
                        />
                        {!!errormessage.password && <span className="text-danger">
                            {errormessage.password}</span>}

                    <TextBoxField 
                        field="confirmpassword"
                        type="password"
                        label="Підтвердження пароля"
                        value={confirmpassword}
                        onChangeHandler={this.onChangeHandler}
                        isvalid={errormessage.confirmpassword == 0? true : false}
                        />
                        {!!errormessage.confirmpassword && <span className="text-danger">
                            {errormessage.confirmpassword}</span>}
                    
                    <button type="submit" className="btn btn-primary">Реєстрація</button>
                </form>
                </div>

            </div>
        )
    }
}

export default withRouter(RegisterPage)
