import React, { Component } from 'react'
import { TouchableOpacity, Text, Image, View, StyleSheet } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Icon } from 'native-base'

import { Images } from '../../Themes'
import Layout from './Components/AppointmentType'
import Hamburger from '../../Components/Hamburger'
import AnimatedContainerWithNavbar from '../../Components/AnimatedContainerWithNavbar'
import { addOrUpdateAppointmentType } from '../../Actions/appointmentTypes'

class AppointmentType extends Component {
  static propTypes = {
    member: PropTypes.shape({}).isRequired,
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
  }

  constructor(props){
    super(props)
    this.actionType = this.props.navigation.getParam('ActionType') || 'Add'
  }

  state = {
    errorMessage: null,
    successMessage: null
  }

  animatedContainerWithNavbar = null;

  componentDidMount = () => {
    this.props.navigation.setParams({
      pressHamburger: this.pressHamburger.bind(this)
    })
  }

  onFormSubmit = (data) => {
    const { onFormSubmit } = this.props;
    return onFormSubmit(data)
      .then(mes => this.setState({ successMessage: mes, errorMessage: null }))
      .catch((err) => { this.setState({ errorMessage: err, successMessage: null }); throw err; });
  }

  render = () => {
    const {
      member,
      appointmentTypes,
      appointmentCategories,
      isLoading,
      navigation
    } = this.props


    let appointmentType = this.actionType == 'Update' ? appointmentTypes.selectedAppointmentType : {
      ServiceCategoryId: appointmentCategories.selectedAppointmentCategory.Id,
      SiteId: member.SiteId
    }

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
  member: state.member || {},
  appointmentTypes: state.appointmentTypes || {},
  appointmentCategories: state.appointmentCategories || {},
  isLoading: state.status.loading || false
})

const mapDispatchToProps = {
  onFormSubmit: addOrUpdateAppointmentType
}

export default connect(mapStateToProps, mapDispatchToProps)(AppointmentType)

