import React from 'react';
import { AuthenticationProvider } from './AuthenticationContext';
import LoginPage from './LoginPage';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom'
import ProtectedRoute from './ProtectedRoute';
import AdminContent from './AdminContent';

const AdminLandingPage = () => (
    <div>
        <Router>
            <AuthenticationProvider>
                <LoginPage />
                <ProtectedRoute path="/admincontent" component={AdminContent} />
            </AuthenticationProvider>
        </Router>
    </div>
)

export default AdminLandingPage;