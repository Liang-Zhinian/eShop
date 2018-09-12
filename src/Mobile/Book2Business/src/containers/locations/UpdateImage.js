import React, { Component } from 'react';
import { TouchableOpacity, Text } from 'react-native';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { Icon } from 'native-base';
import { Actions } from 'react-native-router-flux';


import site from '../../constants/site';
import { getLocation } from '../../actions/locations';

class UpdateImage extends Component {
    static propTypes = {
        Layout: PropTypes.func.isRequired,
        locations: PropTypes.shape({}).isRequired,
        onFormSubmit: PropTypes.func.isRequired,
        isLoading: PropTypes.bool.isRequired,
    }

    static defaultProps = {
        match: null,
    }

    // static renderRightButton = (props) => {
    //     return (
    //         <TouchableOpacity
    //             onPress={() => {
    //                 Actions.appointment_category({ match: { params: { action: 'ADD' } } })
    //             }}
    //             style={{ marginRight: 10 }}>
    //             <Icon name='add' />
    //         </TouchableOpacity>
    //     );
    // }

    componentDidMount = () => {
        const { siteId, locationId } = site;

        this.props.getLocation(siteId, locationId);
    }


    state = {
        errorMessage: null,
        successMessage: null,
    }

    onFormSubmit = (data) => {
        // const { onFormSubmit } = this.props;
        // return onFormSubmit(data)
        //   .then(mes => this.setState({ successMessage: mes, errorMessage: null }))
        //   .catch((err) => { this.setState({ errorMessage: err, successMessage: null }); throw err; });
    }

    render = () => {
        const {
            locations,
            Layout,
            isLoading,
        } = this.props;

        console.log('UpdateLocationInfo', this)

        const { successMessage, errorMessage } = this.state;

        return (
            <Layout
                location={locations.location}
                loading={isLoading}
                error={errorMessage}
                success={successMessage}
                onFormSubmit={this.onFormSubmit}
            />
        );
    }
}

const mapStateToProps = state => ({
    locations: state.locations || {},
    isLoading: state.status.loading || false,
});

const mapDispatchToProps = {
    getLocation: getLocation,
    onFormSubmit: function () { },
};

export default connect(mapStateToProps, mapDispatchToProps)(UpdateImage);
