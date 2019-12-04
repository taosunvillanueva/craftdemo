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
            per: 15,
            page: 0,
            totalPages: null
        }
    }

    componentDidMount() {
        this.loadUser();
        window.addEventListener('scroll', this.handleScroll);
    };
    
    componentWillUnmount() {
        window.removeEventListener('scroll', this.handleScroll);
    };

    handleClick() {
        this.props.logout();
    }

    handleScroll = (e) => {
        const table = document.querySelector("Table");
        const offsetTop = table.offsetTop;

        const lastRow = document.querySelector("tbody tr:last-child");
        const lastRowOffset = lastRow.offsetTop + lastRow.clientHeight / 2;
        const pageOffset = window.pageYOffset + window.innerHeight;

        if (pageOffset - offsetTop > lastRowOffset) {
            this.loadMore();
        }
    };

    loadUser = async () => {
        const { per, page } = this.state;
        const url = `https://registrationapi20191122063201.azurewebsites.net/Api/GetAllRegistrations/OfficeLocation?page=${page}&perPage=${per}`;

        await axios.get(url)
            .then(res => {
                this.setState(prevState => ({
                    items: [
                        ...prevState.items,
                        ...JSON.parse(res.data.result)
                    ],
                    scrolling: false,
                    totalPages: res.data.totalPages
                }))
            })
            .catch(ex => {
                console.log(ex);
            });
    }

    loadMore = () => {
        this.setState(
            prevState => ({
                page: prevState.page + 1,
                scrollig: true
            }),
            this.loadUser
        );
    };

    handleLoadAll = () => {
        this.setState({ 
            page: 0, 
            per: 15, 
            items: [], 
            citySelectedOption: ""
            },
            this.loadUser
        );

        window.addEventListener('scroll', this.handleScroll);
    }

    handleSelectChange = name => async option => {
        window.removeEventListener('scroll', this.handleScroll);

        this.loadingElement.current.changeLoading(true);
        this.setState({ [name]: option, items: [] });
        const url = "https://registrationapi20191122063201.azurewebsites.net/Api/RegistrationsByCity/" + option.value;

        await axios.get(url)
            .then(res => {
                console.log(res);
                this.setState( { items: JSON.parse(res.data.result) } );
                this.loadingElement.current.changeLoading(false);
            })
            .catch(ex => {
                console.log(ex);
                this.loadingElement.current.changeLoading(false);
            })
    }

    render() {
        const { citySelectedOption, items, isfetching } = this.state;

        return (
            <div className="admincontent">
                <button onClick={this.handleClick}>Logout</button>
                <button onClick={this.handleLoadAll}>Load All Registration</button>
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