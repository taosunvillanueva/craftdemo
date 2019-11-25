import React from 'react';
import { Router, Route } from 'react-router-dom';
import RegisterForm from './RegisterForm'
import Thankyou from './ThankYouPage'

const Routes = () => (
    <Router>
        <Route exact path="/" component={RegisterForm} />
        <Route path="/thankyou" component={Thankyou} />
    </Router>
);

export default Routes;