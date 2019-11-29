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
                <h2>TODO: Admin Page Displaying all the registrations</h2>
                <button onClick={this.handleClick}>Logout</button>
            </div>
        )
    }
}

export default AdminContent;