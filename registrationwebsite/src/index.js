import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import * as serviceWorker from './serviceWorker';
import { BrowserRouter, Route, Link, Switch } from 'react-router-dom'
import Thankyou from './components/ThankYouPage';
import AdminPage from './components/AdminPage';
import Notfound from './components/NotFound';
import NoAccess from './components/NoAccess';
import {RegistrationFailed, EmailExists } from './components/RegistrationFailed';

const Routing = () => (
    <BrowserRouter>
        <div>
            <ul>
                <li>
                    <Link to="/">Register</Link>
                </li>
                <li>
                    <Link to="/admin">Admin Login</Link>
                </li>
            </ul>
            <Switch>
                <Route exact path="/" component={App} />
                <Route path="/thankyou" component={Thankyou} />
                <Route path="/failed" component={RegistrationFailed} />
                <Route path="/exists" component={EmailExists} />
                {/* <Route paht="/noaccess" component={NoAccess} /> */}
                <Route path="/admin" component={AdminPage} />
                <Route component={Notfound} />
            </Switch>
        </div>
    </BrowserRouter>
)

ReactDOM.render(<Routing />, document.getElementById('root'));

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
