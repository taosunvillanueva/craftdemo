import React from 'react';
import { officeOptions } from '../components/RegisterForm';
import axios from 'axios';
import Select from 'react-select';
import RegistrationTable from './RegistrationTable';
import LoadingIndicator from  './LoadingIndicator';

class AdminContent extends React.Component {
    constructor(props){
        super(props);
        this.handleClick = this.handleClick.bind(this);
        this.loadingElement = React.createRef();
        this.state = {
            citySelectedOption: "",
            items: [],
            isfetching: false,
            per: 3,
            page: 1,
            totalPages: null
        }
    }

    componentWillMount() {
        this.loadUser();
    }

    handleClick() {
        this.props.logout();
    }

    loadUser = () => {
        const { per, page } = this.state;
        const url = `https://registrationapi20191122063201.azurewebsites.net/Api/GetAllRegistrations?sort=OfficeLocation&PerPage=${per}&Page=${page}`;

        await axios.get(url)
            .then(res => {
                this.setState({
                    items: res.data.registrations,
                    scrolling: false,
                    totalPages: res.data.totalPages
                 });
            })
            .catch(ex => {
                console.log(ex);
            });
    }

    handleSelectChange = name => async option => {
        this.loadingElement.current.changeLoading(true);
        this.setState({ [name]: option })
        const url = "https://registrationapi20191122063201.azurewebsites.net/Api/RegistrationsByCity/" + option.value;

        await axios.get(url)
            .then(res => {
                console.log(res);
                this.setState( { items: res.data.registrations } )
            })
            .catch(ex => {
                console.log(ex);
            })

        this.loadingElement.current.changeLoading(false);
    }

    render() {
        const { citySelectedOption, items, isfetching } = this.state;

        return (
            <div className="admincontent">
                <button onClick={this.handleClick}>Logout</button>
                <Select 
                    className="select"
                    value={citySelectedOption}
                    onChange={this.handleSelectChange('citySelectedOption')}
                    options={officeOptions}
                />
                
                <LoadingIndicator isloading={isfetching} ref={this.loadingElement} />
                { items.length > 0 && <RegistrationTable data={items}/>}
            </div>
        )
    }
}

export default AdminContent;