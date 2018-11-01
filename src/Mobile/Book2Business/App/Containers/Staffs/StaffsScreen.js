import React, { Component } from 'react'
import {
  AppState,
  Image,
  TouchableOpacity,
} from 'react-native'
import Icon from 'react-native-vector-icons/FontAwesome'

import { Images } from '../../Themes'
import AnimatedContainerWithNavbar from '../../Components/AnimatedContainerWithNavbar'
import Container from './StaffListingContainer'
import Layout from './Components/StaffListing'

class StaffsScreen extends Component {
  constructor(props) {
    super(props)

    const appState = AppState.currentState

    this.state = { appState, isMenuOpen: false }
  }

  animatedContainerWithNavbar = null;

  static navigationOptions = ({ navigation }) => {
    const { pressMenu, icon } = navigation.state.params || {
      pressMenu: () => null,
      icon: 'bars'
    }
    return {
      title: 'Staffs',
      headerRight: (
        <TouchableOpacity style={{ marginRight: 20 }} onPress={pressMenu || (() => { alert('Missing handler') })} >
          <Icon name={icon || 'bars'} size={20} color='#fff' />
        </TouchableOpacity >),
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

  toggleMenu() {
    let icon = 'times'
    // let pressMenu = this.showMenu.bind(this);
    if (this.state.isMenuOpen) {
      icon = 'bars'
      this.animatedContainerWithNavbar.closeMenu()
    } else {
      icon = 'times'
      this.animatedContainerWithNavbar.showMenu()
    }
    this.props.navigation.setParams({
      icon
    })

    this.setState({
      isMenuOpen: !this.state.isMenuOpen
    })
  }

  componentDidMount() {
    AppState.addEventListener('change', this._handleAppStateChange)

    this.props.navigation.setParams({
      icon: 'bars',
      pressMenu: this.toggleMenu.bind(this)
    })
  }

  componentWillUnmount() {
    AppState.removeEventListener('change', this._handleAppStateChange)
  }

  _handleAppStateChange = (nextAppState) => {
    const { appState } = this.state
    if (appState.match(/inactive|background/) && nextAppState === 'active') {
      // this.props.getScheduleUpdates()
    }
    this.setState({ appState: nextAppState })
  }

  render() {
    return (
      <AnimatedContainerWithNavbar
        ref={ref => this.animatedContainerWithNavbar = ref}
        menuPosition='right'
        content={(
          <Container Layout={Layout} />
        )}
        // content={(<View />)}
        menu={[
          { text: 'Add Staff', onPress: () => { this.props.navigation.navigate('AddStaff') } }
        ]}
      />
    )
  }
}

export default StaffsScreen
