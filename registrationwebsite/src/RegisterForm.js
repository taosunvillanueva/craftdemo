import React from 'react';
import Select from 'react-select';
import './App.css';

const officeOptions = [
    { value: 'seattle', label: 'Seattle'},
    { value: 'sandiego', label: 'San Diego'},
    { value: 'newyork', label: 'New York'},
    { value: 'boston', label: 'Boston'}
]

const interestOptions = [
    { value: 'talks', label: '101 Talks & Workshops'},
    { value: 'application', label: 'Application Security'},
    { value: 'ahead', label: 'Getting Ahead of Attackers'},
    { value: 'iot', label: 'Internet of Things'},
    { value: 'offensive', label: 'Offensive Security'},
    { value: 'hack', label: 'Web Hacking'},
]

const tshirtOptions = [
    { value: 'ws', label: 'Womens S'},
    { value: 'wm', label: 'Womens M'},
    { value: 'wl', label: 'Womens L'},
    { value: 'wxl', label: 'Womens XL'},
    { value: 'wxxl', label: 'Womens XXL'},
    { value: 'w3xl', label: 'Womens 3XL'},
    { value: 'ms', label: 'Mens S'},
    { value: 'mm', label: 'Mens M'},
    { value: 'ml', label: 'Mens L'},
    { value: 'mxl', label: 'Mens XL'},
    { value: 'mxxl', label: 'Mens XXL'},
    { value: 'm3xl', label: 'Mens 3XL'}, 
]

class RegisterForm extends React.Component{
    constructor() {
        super();
        this.handleSubmit = this.handleSubmit.bind(this);
        this.state = {
            officeSelectedOption: null,
            interestSelectedOption: null,
            tshirtSelectedOption: null
        };
    }

    handleOfficeChange = officeSelectedOption => {
        this.setState({ officeSelectedOption });
    }

    handleInterestChange = interestSelectedOption => {
        this.setState({ interestSelectedOption });
    }

    handleTShirtChange = tshirtSelectedOption => {
        this.setState( { tshirtSelectedOption });
    }

    handleSubmit(event) {
        event.preventDefault();
        const data = new FormData(event.target);
    }

    render() {
        const { officeSelectedOption, interestSelectedOption, tshirtSelectedOption } = this.state;

        return (
            <form onSubmit={this.handleSubmit}>
                <div>
                    <label htmlFor="name">Enter your name</label>
                    <input id="name" name="name" type="text"></input>
                </div>

                <div>
                    <label htmlFor="email">Enter your email</label>
                    <input id="email" name="email" type="text"></input>
                </div>

                <Select className="select"
                    value={officeSelectedOption}
                    onChange={this.handleOfficeChange}
                    options={officeOptions}
                />

                <Select className="select"
                    value={interestSelectedOption}
                    onChange={this.handleInterestChange}
                    options={interestOptions}
                />

                <Select className="select"
                    value={tshirtSelectedOption}
                    onChange={this.handleTShirtChange}
                    options={tshirtOptions}
                />

                <input type="submit" value="Register" />
            </form>
        )
    };
}

export default RegisterForm;