import React from 'react';
import { usePermissions } from 'react-admin';
import NoAccess from './NoAccess';
import LoginPage from './Authentication/LoginPage';
import AuthenticationProvider from './Authentication/AuthenticationProvider'

const PageContent = () => <h1>Logged in!!!</h1>

const AdminPageContent = () => {
    const { permissions  } = usePermissions();
    if (permissions === 'admin') {
        return <PageContent />
    }
    // else {
    //     return <NoAccess />
    // }
    else {
        return <LoginPage />
    }
}

export default AdminPageContent;