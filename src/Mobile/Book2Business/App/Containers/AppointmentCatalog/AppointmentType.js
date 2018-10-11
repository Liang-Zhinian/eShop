import React, { Component } from 'react'
import { TouchableOpacity, Text, Image, View, StyleSheet } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Icon } from 'native-base'

import { Images } from '../../Themes'
import Layout from './Components/AppointmentType'
import { updateAppointmentType } from '../../Actions/appointmentTypes'
import Hamburger from '../../Components/Hamburger'
import AnimatedContainerWithNavbar from '../../Components/AnimatedContainerWithNavbar'

class Appointment extends Component {
  static propTypes = {
    appointmentTypes: PropTypes.shape({}).isRequired,
    appointmentCategories: PropTypes.shape({}).isRequired,
    onFormSubmit: PropTypes.func.isRequired,
    isLoading: PropTypes.bool.isRequired
  }

  static defaultProps = {
    match: null
  }

  static navigationOptions = ({ navigation }) => {
    const { pressHamburger, icon } = navigation.state.params || {
      pressHamburger: () => null,
    }
    return {
      title: 'Appointment Type',
      headerRight: (<Hamburger onPress={pressHamburger} />),
      tabBarLabel: 'More',
      tabBarIcon: ({ focused }) => (
        <Image
          source={
            focused
              ? Images.activeInfoIcon
              : Images.inactiveInfoIcon
          }
        />
      )
    }
  };

  componentDidMount = () => {
    this.props.navigation.setParams({
      pressHamburger: this.pressHamburger.bind(this)
    })
  }

  state = {
    errorMessage: null,
    successMessage: null
  }

  animatedContainerWithNavbar = null;

  onFormSubmit = (data) => {
    // const { onFormSubmit } = this.props;
    // return onFormSubmit(data)
    //   .then(mes => this.setState({ successMessage: mes, errorMessage: null }))
    //   .catch((err) => { this.setState({ errorMessage: err, successMessage: null }); throw err; });
  }

  render = () => {
    const {
      appointmentTypes,
      appointmentCategories,
      isLoading,
      navigation
    } = this.props

    let appointmentType = appointmentTypes.selectedAppointmentType
    if (!appointmentType) {
      appointmentType = { ServiceCategoryId: appointmentCategories.selectedAppointmentCategory.Id }
    }

    let passedInAppointmentType = navigation.getParam('AppointmentType');
    if (passedInAppointmentType)
      appointmentType = passedInAppointmentType

    const { successMessage, errorMessage } = this.state

    return (

      <AnimatedContainerWithNavbar
        ref={ref => this.animatedContainerWithNavbar = ref}
        menuPosition='right'
        content={(
          <Layout
            appointmentType={appointmentType}
            loading={isLoading}
            error={errorMessage}
            success={successMessage}
            onFormSubmit={this.onFormSubmit}
          />
        )}
        menu={[{
          text: 'Schedules',
          onPress: () => {
            console.log(this)
            this.props.navigation.navigate('StaffScheduleListing')
          }
        }]}
      />
    )
  }

  pressHamburger(isMenuOpen) {
    if (!isMenuOpen) {
      this.animatedContainerWithNavbar.closeMenu()
    } else {
      this.animatedContainerWithNavbar.showMenu()
    }
  }
}

const mapStateToProps = state => ({
  appointmentTypes: state.appointmentTypes || {},
  appointmentCategories: state.appointmentCategories || {},
  isLoading: state.status.loading || false
})

const mapDispatchToProps = {
  onFormSubmit: updateAppointmentType
}

export default connect(mapStateToProps, mapDispatchToProps)(Appointment)

