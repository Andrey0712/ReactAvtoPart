import React from 'react';

//const ListUsers = React.lazy(() => import("../cocmponents/admin/users/List"));
const MainAdminPage = React.lazy(() => import("../cocmponents/admin"));
const UsersPage = React.lazy(() => import("../cocmponents/userlist"));
const EditPage = React.lazy(() => import("../cocmponents/userlist/Edit"));
//const LoginPage = React.lazy(() => import('../cocmponents/auth/Login'));

const adminRoutes = [
    // { path: '/login', exact: true, name: 'Вхід', component: LoginPage  },
    { path: '/users', exact: true, name: 'Users', component: UsersPage  },
    { path: '/edit', exact: true, name: 'UsersEdit', component: EditPage  },
    { path: '/admin', exact: true, name: 'Админ панель', component: MainAdminPage  }
];
export default adminRoutes;