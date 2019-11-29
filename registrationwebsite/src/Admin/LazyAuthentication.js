import React from 'react';
import { Admin } from 'react-admin';
import AuthProvider from './AuthenticationProvider';
import LoginPage from '../AdminUser/LoginPage';
import LogoutButton from '../AdminUser/LoginPage'

const TempProvider = () => {
    return <h1>Temporary data provider</h1>
}

const PageContent = () => {
    return <h1>Admin Page Holder!!</h1>
}

const LazyAuthentication = () => (
    <Admin authProvider={AuthProvider} dataProvider={TempProvider}>
        { permissions =>[permissions === 'admin' ? <PageContent /> : null] }
    </Admin>
);

export default LazyAuthentication;