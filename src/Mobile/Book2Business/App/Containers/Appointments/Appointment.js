import React, { Component } from 'react'
import { TouchableOpacity, Text } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Icon } from 'native-base'

import Layout from '../../Components/Appointments/ServiceItem'
import { getAppointment } from '../../Actions/serviceItems'

class Appointment extends Component {
  static propTypes = {
    appointmentTypes: PropTypes.shape({}).isRequired,
    serviceCategories: PropTypes.shape({}).isRequired,
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
      appointmentTypes,
      serviceCategories,
      isLoading,
      navigation
    } = this.props

    console.log('UpdateAppointmentType', this.props)
    let appointmentType = appointmentTypes.selectedAppointmentType
    if (!appointmentType) {
      appointmentType = { ServiceCategoryId: serviceCategories.selectedCategory.id }
    }

    let passedInAppointmentType = navigation.getParam('AppointmentType');
    if (passedInAppointmentType)
      appointmentType = passedInAppointmentType

    const { successMessage, errorMessage } = this.state

    return (
      <Layout
        appointmentType={appointmentType}
        loading={isLoading}
        error={errorMessage}
        success={successMessage}
        onFormSubmit={this.onFormSubmit}
      />
    )
  }
}

const mapStateToProps = state => ({
  appointmentTypes: state.serviceItems || {},
  serviceCategories: state.serviceCategories || {},
  isLoading: state.status.loading || false
})

const mapDispatchToProps = {
  getAppointment: getAppointment,
  onFormSubmit: function () { }
}

export default connect(mapStateToProps, mapDispatchToProps)(Appointment)
