import React from 'react';
import { Admin } from 'react-admin';
import AuthProvider from './Authentication/AuthenticationProvider';
import LoginPage from './Authentication/LoginPage';
import LogoutButton from './Authentication/LoginPage'
import NoAccess from './NoAccess';
import { usePermissions } from 'react-admin';
import AdminPageContent from './AdminPageContent';

const PageContent = () => {
    return <h1>LOGGED IN!!!</h1>;
}

const LazyAuthentication = () => (
    // <Admin authProvider={AuthProvider} dataProvider={PageContent} loginPage={LoginPage} LogoutButton={LogoutButton} >
    //     <PageContent />
    // </Admin>
    <Admin authProvider={AuthProvider} dataProvider={NoAccess} loginPage={LoginPage}>
        <AdminPageContent />
    </Admin>
);

export default LazyAuthentication;