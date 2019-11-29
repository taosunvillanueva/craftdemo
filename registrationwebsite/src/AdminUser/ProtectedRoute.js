import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import {AuthenticationConsumer} from './AuthenticationContext';

const ProtectedRoute = ({ component: Component, ...rest }) => (
    <AuthenticationConsumer>
        {({ isAuth, logout }) => (
            isAuth ? <Component logout={logout}/> : <Redirect to="/admin" />
        )}
    </AuthenticationConsumer>
);

export default ProtectedRoute;
