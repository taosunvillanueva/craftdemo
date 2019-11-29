import React from 'react';
import axios from 'axios';
import decodeJwt from 'jwt-decode';

const AuthContext = React.createContext();

class AuthenticationProvider extends React.Component {
    state = { isAuth: false }
    constructor() {
        super();
        this.login = this.login.bind(this);
        this.logout = this.logout.bind(this);
    }

    async login({username, password}) {
        var data = {
            username: username,
            password: password
        }

        await axios.post(
            'https://registrationapi20191122063201.azurewebsites.net/Api/AdminLogin',
            { headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' } },
            { data }
        )
        .then( response => {
            if (response.status !== 200) {
                throw new Error(response.statusText);
            }

            const token = response.data.token;
            const decodedToken = decodeJwt(token);
            localStorage.setItem('token', token);

            this.setState({ isAuth: true });
            return Promise.resolve();
        })
        .catch(ex => {
            console.log(ex);
            if (ex.response.status === 401 || ex.response.status === 403) {
                return Promise.reject();
            }
        });
    }

    logout() {
        localStorage.removeItem('token');
        this.setState( { isAuth: false, username: '', password: '' });
    }

    render() {
        return (
            <AuthContext.Provider 
                value={{
                    isAuth: this.state.isAuth,
                    login: this.login,
                    logout: this.logout
                }}
            >
                { this.props.children }
            </AuthContext.Provider>
        );
    }
}

const AuthenticationConsumer = AuthContext.Consumer;

export { AuthenticationProvider, AuthenticationConsumer }
