import React from 'react';

class AdminContent extends React.Component {
    constructor(props){
        super(props);
        this.handleClick = this.handleClick.bind(this);
    }

    handleClick() {
        this.props.logout();
    }

    render() {
        return (
            <div className="admincontent">
                <button onClick={this.handleClick}>Logout</button>
                <h2>TODO: Admin Page Displaying all the registrations</h2>
            </div>
        )
    }
}

export default AdminContent;