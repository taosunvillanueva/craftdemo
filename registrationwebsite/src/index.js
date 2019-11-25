import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import * as serviceWorker from './serviceWorker';
import { BrowserRouter, Route, Link, Switch } from 'react-router-dom'
import Thankyou from './ThankYouPage';
import AdminLogin from './AdminLogin';
import Notfound from './NotFound'

// const RouterApp = () => (
//     <BrowserRouter>
//         <App />
//     </BrowserRouter>
// )

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
                <Route path="/admin" component={AdminLogin} />
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
