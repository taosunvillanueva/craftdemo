import React from 'react';
import { Admin } from 'react-admin';
import AuthProvider from './AuthenticationProvider';
import LoginPage from './LoginPage';
import LogoutButton from './LoginPage'

const TempProvider = () => {
    return <h1>Temporary data provider</h1>
}

const LazyAuthentication = () => (
    <Admin authProvider={AuthProvider} dataProvider={TempProvider}>
        { permissions =>[permissions === 'admin' ? <PageContent /> : null] }

        {/* <PageContent /> */}
    </Admin>
);

export default LazyAuthentication;