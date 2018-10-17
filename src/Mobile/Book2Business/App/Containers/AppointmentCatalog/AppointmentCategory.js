import React, { Component } from 'react'
import { TouchableOpacity, Text } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Icon } from 'native-base'

import Layout from './Components/AppointmentCategory'
import { addOrUpdateAppointmentCategory } from '../../Actions/appointmentCategories'

class AppointmentCategory extends Component {
  static propTypes = {
    onFormSubmit: PropTypes.func.isRequired,
    isLoading: PropTypes.bool.isRequired,
    member: PropTypes.shape({}).isRequired,
    appointmentCategories: PropTypes.shape({}).isRequired,
  }

  state = {
    errorMessage: null,
    successMessage: null
  }

  constructor(props){
    super(props)
    this.actionType = this.props.navigation.getParam('ActionType') || 'Add'
  }


  onFormSubmit = (data) => {
    const { onFormSubmit } = this.props
    return onFormSubmit(data)
      .then(mes => this.setState({ successMessage: mes, errorMessage: null }))
      .catch((err) => { this.setState({ errorMessage: err, successMessage: null }); throw err })
  }

  render = () => {
    const {
      member,
      appointmentCategories,
      isLoading
    } = this.props

    const { successMessage, errorMessage } = this.state

    let appointmentCategory = this.actionType == 'Update' ? appointmentCategories.selectedAppointmentCategory : {
      ScheduleTypeId: 4,
      SiteId: member.SiteId
    }

    return (
      <Layout
        appointmentCategory={appointmentCategory}
        loading={isLoading}
        error={errorMessage}
        success={successMessage}
        onFormSubmit={this.onFormSubmit}
      />
    )
  }
}

const mapStateToProps = state => ({
  member: state.member || {},
  appointmentCategories: state.appointmentCategories || {},
  isLoading: state.status.loading || false,
})

const mapDispatchToProps = {
  onFormSubmit: addOrUpdateAppointmentCategory
}

export default connect(mapStateToProps, mapDispatchToProps)(AppointmentCategory)
