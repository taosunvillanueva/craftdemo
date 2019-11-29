import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import {AuthenticationConsumer} from './AuthenticationContext';

const ProtectedComponent = ({ component: Component, ...rest }) => (
    <AuthenticationConsumer>
        {({ isAuth, logout }) => (
            isAuth ? <Component logout={logout}/> : <Redirect to="/admin" />
        )}
    </AuthenticationConsumer>
);

export default ProtectedComponent;
