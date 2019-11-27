import React from 'react'

export const RegistrationFailed = () => {
    return (
        <p>Registration failed. Please try again later.</p>
    )
}

export const EmailExists = (props) => {
    return (
    <p>Sorry. Your email <b>{props.location.state.email}</b> has already been previously registered.</p>
    )
}
