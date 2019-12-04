import React, { useState } from 'react';
import { AuthenticationConsumer } from './AuthenticationContext';
import { Validate } from './CredentialValidator'

class LoginPage extends React.Component {
    constructor() {
        super();
        this.handleSubmit = this.handleSubmit.bind(this);
        this.state = {
            username: '',
            password: '',
            errors: []
        }
    }

    onChange = (e) => {
        this.setState( { [e.target.name]: e.target.value })
    };

    handleSubmit = submitAction => async e  => {
        e.preventDefault();
        const { username, password } = this.state;

        const errors = Validate(username, password);
        if (errors.length > 0) {
            this.setState({ errors })
            return;
        }

        await submitAction({username, password})
        .then(res => {
            // login succeeded. clert the existing login credentials to be extra safe
            this.setState({ username: '', password: '', errors: [] });
        })
        .catch(ex => {
            this.setState({ errors: ['Login faield. Incorrect admin username or password.'] });
        });
    };

    render() {
        const { username, password, errors } = this.state;

        return (
            <div>
                <div>
                    {errors.map(error => (
                        <p key={error}>Error: {error}</p>
                    ))}
                </div>
                <div>
                    <AuthenticationConsumer>
                        {({ isAuth, login }) =>(
                            !isAuth &&
                            <form onSubmit={this.handleSubmit(login)}>
                                <div>
                                    <label htmlFor="name">Username:</label>
                                    <input name="username" type="username" value={username} onChange={this.onChange} />
                                </div>
                                
                                <div>
                                    <label htmlFor="password">Password:</label>
                                    <input name="password" type="password" value={password} onChange={this.onChange} />
                                </div>

                                <input type="submit" value="Login" />
                            </form>
                        )}
                    </AuthenticationConsumer>
                </div>

            </div>
        )
    }
}

export default LoginPage;