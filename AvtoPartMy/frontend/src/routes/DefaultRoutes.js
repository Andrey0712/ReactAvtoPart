import React from 'react';

const HomePage = React.lazy(() => import("../cocmponents/home"));
// const Dialog = React.lazy(() => import("../coсmponents/dialog"));
const LoginPage = React.lazy(() => import('../cocmponents/auth/Login'));
const RegisterPage = React.lazy(() => import("../cocmponents/auth/Register"));
const UsersPage = React.lazy(() => import("../cocmponents/userlist"));
const EditPage = React.lazy(() => import("../cocmponents/userlist/Edit"));

const defaultRoutes = [
    { path: '/', exact: true, name: 'Головна', component: HomePage  },
    // { path: '/dialog', exact: true, name: 'Діалог', component: Dialog  },
    { path: '/login', exact: true, name: 'Вхід', component: LoginPage  },
    { path: '/register', exact: true, name: 'Реєстрація', component: RegisterPage  },
    { path: '/users', exact: true, name: 'Users', component: UsersPage  },
    { path: '/edit', exact: true, name: 'UsersEdit', component: EditPage  }
];
export default defaultRoutes;