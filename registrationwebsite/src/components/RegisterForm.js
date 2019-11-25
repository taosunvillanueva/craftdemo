import React from 'react';
import Select from 'react-select';
import axios from 'axios';
import { withRouter } from 'react-router-dom'
import { Validate } from './Validator'
import '../App.css';

const officeOptions = [
    { value: 'Seattle', label: 'Seattle'},
    { value: 'San Diego', label: 'San Diego'},
    { value: 'New York', label: 'New York'},
    { value: 'Boston', label: 'Boston'}
]

const interestOptions = [
    { value: '101 Talks & Workshops', label: '101 Talks & Workshops'},
    { value: 'Application Security', label: 'Application Security'},
    { value: 'Getting Ahead of Attackers', label: 'Getting Ahead of Attackers'},
    { value: 'Internet of Things', label: 'Internet of Things'},
    { value: 'Offensive Security', label: 'Offensive Security'},
    { value: 'Web Hacking', label: 'Web Hacking'},
]

const tshirtOptions = [
    { value: 'Womens S', label: 'Womens S'},
    { value: 'Womens M', label: 'Womens M'},
    { value: 'Womens L', label: 'Womens L'},
    { value: 'Womens XL', label: 'Womens XL'},
    { value: 'Womens XXL', label: 'Womens XXL'},
    { value: 'Womens 3XL', label: 'Womens 3XL'},
    { value: 'Mens S', label: 'Mens S'},
    { value: 'Mens M', label: 'Mens M'},
    { value: 'Mens L', label: 'Mens L'},
    { value: 'Mens XL', label: 'Mens XL'},
    { value: 'Mens XXL', label: 'Mens XXL'},
    { value: 'Mens 3XL', label: 'Mens 3XL'}, 
]

class RegisterForm extends React.Component{
    constructor() {
        super();
        this.handleSubmit = this.handleSubmit.bind(this);
        this.state = {
            userName: "",
            userEmail: "",
            officeSelectedOption: "",
            interestSelectedOption: "",
            tshirtSelectedOption: "",
            errors: []
        };
    }

    onChange = (e) => {
        this.setState({ [e.target.name]: e.target.value })
    }

    handleSelectChange = name => option => {
        this.setState({ [name]: option })
    }

    handleSubmit = async (event) => {
        event.preventDefault();
        const { userName, userEmail, officeSelectedOption, interestSelectedOption, tshirtSelectedOption } = this.state;

        const errors = Validate(userName, userEmail, officeSelectedOption, interestSelectedOption, tshirtSelectedOption);
        if (errors.length > 0) {
            this.setState({ errors })
            return;
        }

        var data = {
            name: userName,
            email: userEmail,
            officeLocation: officeSelectedOption.value,
            securityInterest: interestSelectedOption.value,
            shirtSize: tshirtSelectedOption.value
        }

        await axios.post(
            'https://registrationapi20191122063201.azurewebsites.net/Api/RegisterUser',
            { headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' } },
            { data }
        )
        .then( res => {
            console.log(res.data);
            this.props.history.push('/thankyou')
        })
        .catch(ex => {
            console.log(ex);
        })
    }

    render() {
        const { userName, userEmail, officeSelectedOption, interestSelectedOption, tshirtSelectedOption, errors } = this.state;

        return (
            <form onSubmit={this.handleSubmit}>
                {errors.map(error => (
                    <p key={error}>Error: {error}</p>
                ))}

                <div>
                    <label htmlFor="name">Enter your name</label>
                    <input 
                        name="userName" 
                        type="text" 
                        value={userName} 
                        onChange={this.onChange}/>
                </div>

                <div>
                    <label htmlFor="email">Enter your email</label>
                    <input 
                        name="userEmail" 
                        type="text" 
                        value={userEmail}
                        onChange={this.onChange}/>
                </div>

                <Select 
                    className="select"
                    value={officeSelectedOption}
                    onChange={this.handleSelectChange('officeSelectedOption')}
                    options={officeOptions}
                />

                <Select 
                    className="select"
                    value={interestSelectedOption}
                    onChange={this.handleSelectChange('interestSelectedOption')}
                    options={interestOptions}
                />

                <Select 
                    className="select"
                    value={tshirtSelectedOption}
                    onChange={this.handleSelectChange('tshirtSelectedOption')}
                    options={tshirtOptions}
                />

                <input type="submit" value="Register" />
            </form>
        )
    };
}

export default withRouter(RegisterForm);