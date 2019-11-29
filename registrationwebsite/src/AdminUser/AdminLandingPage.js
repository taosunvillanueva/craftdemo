import React from 'react';
import { AuthenticationProvider } from './AuthenticationContext';
import LoginPage from './LoginPage';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom'
import ProtectedComponent from './ProtectedComponent';
import AdminContent from './AdminContent';

const AdminLandingPage = () => (
    <div>
        <AuthenticationProvider>
            <LoginPage />
            <ProtectedComponent component={AdminContent} />
        </AuthenticationProvider>
    </div>
)

export default AdminLandingPage;