import React, { Component } from 'react'
import { TouchableOpacity, Text } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Icon } from 'native-base'
// import { Actions } from 'react-native-router-flux';

import site from '../../Constants/site'
import { getAppointment } from '../../Actions/serviceItems'

class Appointment extends Component {
  static propTypes = {
    Layout: PropTypes.func.isRequired,
    appointment: PropTypes.shape({}).isRequired,
    onFormSubmit: PropTypes.func.isRequired,
    isLoading: PropTypes.bool.isRequired
  }

  static defaultProps = {
    match: null
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
    const { siteId } = site

        // this.props.getAppointment(siteId, locationId);
  }

  state = {
    errorMessage: null,
    successMessage: null
  }

  onFormSubmit = (data) => {
        // const { onFormSubmit } = this.props;
        // return onFormSubmit(data)
        //   .then(mes => this.setState({ successMessage: mes, errorMessage: null }))
        //   .catch((err) => { this.setState({ errorMessage: err, successMessage: null }); throw err; });
  }

  render = () => {
    const {
            appointments,
            Layout,
            isLoading
        } = this.props

    console.log('UpdateLocationInfo', this)

    const { successMessage, errorMessage } = this.state

    return (
      <Layout
        appointment={appointments.appointment}
        loading={isLoading}
        error={errorMessage}
        success={successMessage}
        onFormSubmit={this.onFormSubmit}
            />
    )
  }
}

const mapStateToProps = state => ({
  locations: state.locations || {},
  isLoading: state.status.loading || false
})

const mapDispatchToProps = {
  getAppointment: getAppointment,
  onFormSubmit: function () { }
}

export default connect(mapStateToProps, mapDispatchToProps)(UpdateAddress)
