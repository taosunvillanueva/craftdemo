import React from 'react';
import axios from 'axios';
import decodeJwt from 'jwt-decode';

const AuthenticationProvider = {
    login: async ({username, password}) => {
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

            if (decodedToken.unique_name === username) {
                localStorage.setItem('permissions', 'admin');
            }
        })
        .catch(ex => {
            console.log(ex);
            if (ex.response.status === 401 || ex.response.status === 403) {
                return Promise.reject();
            }
        });
    },
    logout: () => {
        localStorage.removeItem('token');
        localStorage.removeItem('permissions');
        return Promise.resolve();
    },
    checkError: (error) => {
        const status = error.status;
        if (status === 401 || status === 403) {
            return Promise.reject();
        }

        return Promise.resolve();
    },
    checkAuth: () => {
        const storedToken = localStorage.getItem('token');
        return storedToken ? Promise.resolve(): Promise.reject();
    },
    getPermissions: () => {
        const role = localStorage.getItem('permissions');
        return role ? Promise.resolve(role) : Promise.reject();
    }
}

export default AuthenticationProvider;