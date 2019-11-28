import React, { useState } from 'react';
import { useLogin, useNotify } from 'react-admin'

const LoginPage = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const login = useLogin();
    const notify = useNotify();
    const submit = (e) => {
        e.preventDefault();
        login({username, password})
            .catch(() => notify('Invalid username or password'));
    };

    return  (
        <form onSubmit={submit}>
            <div>
                <label htmlFor="name">Username:</label>
                <input name="username" type="username" value={username} onChange={e => setUsername(e.target.value)} />
            </div>
            
            <div>
                <label htmlFor="password">Password:</label>
                <input name="password" type="password" value={password} onChange={e => setPassword(e.target.value)} />
            </div>

            <input type="submit" value="Login" />
        </form>
    );
}

export default LoginPage;