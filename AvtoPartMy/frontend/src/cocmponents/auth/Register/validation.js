import * as Yup from 'yup';

const validate=()=>{


    return Yup.object({
        email: Yup.string()
            .email('Введіть валідні дані')
            .required('Введіть пошту'),
        name: Yup.string()
            .required("Вкажіть прізвище"),  
        password: Yup.string()
            .required('Вкажіть пароль.')
            .min(3, 'Пароль має містить мінімум 3 символів.')
            .matches(/[0-9]/, 'Пароль має містить хоча б одну цифру.'),
        confirmpassword: Yup.string()
            .oneOf([Yup.ref('password'), null], 'Passwords must match')
            .required('Confirm Password is required'),

    });
}
export default validate;