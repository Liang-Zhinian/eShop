import React, { Component } from 'react'
import { TouchableOpacity, Text, Image, View, StyleSheet } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Icon } from 'native-base'

import { Images } from '../../Themes'
import Layout from '../../Components/Appointments/ServiceItem'
import { getAppointment } from '../../Actions/serviceItems'
import Hamburger from '../../Components/Hamburger'
import AnimatedContainerWithNavbar from '../../Components/AnimatedContainerWithNavbar'

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
      serviceCategories,
      isLoading,
      navigation
    } = this.props

    let appointmentType = appointmentTypes.selectedAppointmentType
    if (!appointmentType) {
      appointmentType = { ServiceCategoryId: serviceCategories.selectedCategory.id }
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
        // content={(<View />)}
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
  appointmentTypes: state.serviceItems || {},
  serviceCategories: state.serviceCategories || {},
  isLoading: state.status.loading || false
})

const mapDispatchToProps = {
  getAppointment: getAppointment,
  onFormSubmit: function () { }
}

export default connect(mapStateToProps, mapDispatchToProps)(Appointment)

const styles = StyleSheet.create({

  textFooter: {
    color: '#fff'
  },
  menutext: {
    fontSize: 20,
    padding: 10
  },
})
