import React, { Component } from 'react'
import { TouchableOpacity, Text } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Icon } from 'native-base'

import GradientView from '../../Components/GradientView'
import GradientHeader, { Header } from '../../Components/GradientHeader'
import Layout from './Appointments/AppointmentSchedule'
import { addOrUpdateAppointmentSchedule } from '../../Actions/appointmentSchedules'
import styles from './Styles/AppointmentScheduleScreenStyle'

class AppointmentScheduleScreen extends Component {
  static propTypes = {
    onFormSubmit: PropTypes.func.isRequired,
    isLoading: PropTypes.bool.isRequired,
    member: PropTypes.shape({}).isRequired,
    appointmentSchedules: PropTypes.shape({}).isRequired,
  }

  state = {
    errorMessage: null,
    successMessage: null
  }

  constructor(props) {
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
      isLoading,
      navigation
    } = this.props

    const { successMessage, errorMessage } = this.state

    let appointmentSchedule = this.actionType == 'Update' ? appointmentSchedules.selectedAppointmentSchedule : {
      // ScheduleTypeId: 4,
      SiteId: member.currentLocation.SiteId,
      LocationId: member.currentLocation.LocationId
    }

    return (
      <GradientView style={[styles.linearGradient, { flex: 1 }]}>
        <GradientHeader>
          <Header title='Book Appointment'
            goBack={() => { navigation.goBack(null) }} />
        </GradientHeader>
        <Layout
          member={member}
          appointmentSchedule={appointmentSchedule}
          loading={isLoading}
          error={errorMessage}
          success={successMessage}
          onFormSubmit={this.onFormSubmit}
          navigation={this.props.navigation}
        />
      </GradientView>
    )
  }
}

const mapStateToProps = state => ({
  member: state.member || {},
  appointmentSchedules: state.appointmentSchedules || {},
  isLoading: state.status.loading || false,
})

const mapDispatchToProps = {
  onFormSubmit: addOrUpdateAppointmentSchedule
}

export default connect(mapStateToProps, mapDispatchToProps)(AppointmentScheduleScreen)
