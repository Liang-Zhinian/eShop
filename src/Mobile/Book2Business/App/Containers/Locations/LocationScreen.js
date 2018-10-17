import React, { Component } from 'react'
import {
  Text,
  View,
  ListView,
  Image,
  Dimensions,
  TouchableOpacity,
  TouchableHighlight,
} from 'react-native'
import Icon from 'react-native-vector-icons/FontAwesome'
import { connect } from 'react-redux'
import PropTypes from 'prop-types'

const { width, height } = Dimensions.get('window')

import { Images } from '../../Themes'
import AnimatedContainerWithNavbar from '../../Components/AnimatedContainerWithNavbar'

import Layout from './Components/LocationInfo'
import { getLocation, setError } from '../../Actions/locations'

class LocationScreen extends Component {
  static propTypes = {
    // Layout: PropTypes.func.isRequired,
    member: PropTypes.shape({}).isRequired,
    locations: PropTypes.shape({
      loading: PropTypes.bool.isRequired,
      error: PropTypes.string,
      currentLocation: PropTypes.shape({}).isRequired,
    }).isRequired,
    fetchLocation: PropTypes.func.isRequired,
    showError: PropTypes.func.isRequired,
  }

  static navigationOptions = ({ navigation }) => {
    const { pressMenu, icon } = navigation.state.params || {
      pressMenu: () => null,
      icon: 'bars'
    }
    return {
      title: 'Business Information',
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

  constructor() {
    super()

    this.state = {
      isMenuOpen: false,
      loaded: false,
      isLoading: false
    }
  }

  animatedContainerWithNavbar = null;

  componentDidMount() {
    this.setState({
      loaded: true
    })
    this.fetchLocation()

    this.props.navigation.setParams({
      icon: 'bars',
      pressMenu: this.toggleMenu.bind(this)
    })
  }

  render() {
    if (this.state.isLoading) return null

    const { locations, nearbyData, navigation } = this.props

    return (
      <AnimatedContainerWithNavbar
        ref={ref => this.animatedContainerWithNavbar = ref}
        menuPosition='right'
        content={(<Layout
          nearbyData={nearbyData} 
          locationData={locations.currentLocation}
          navigation={navigation}
          error={locations.error}
          loading={locations.loading}
        />)}
        // content={(<View />)}
        menu={[
          {
            text: 'Name & Description',
            onPress: () => { this.goTo('UpdateInfoScreen') }
          },
          {
            text: 'Background image',
            onPress: () => { this.goTo('UpdateBackgroundScreen') }
          },
          {
            text: 'Address',
            onPress: () => { this.goTo('UpdateAddressScreen') }
          },
          {
            text: 'Geolocation',
            onPress: () => { this.goTo('UpdateGeolocationScreen') }
          },
          {
            text: 'Contact',
            onPress: () => { this.goTo('UpdateContactScreen') }
          },
          {
            text: 'Additional images',
            onPress: () => { this.goTo('AdditionalImagesScreen') }
          },
        ]}
      />
    )
  }

  toggleMenu() {
    let icon = 'times'

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

  goTo(route) {
    setTimeout(this.toggleMenu.bind(this), 1000)
    this.props.navigation.navigate(route)
  }

  fetchLocation() {
    const { fetchLocation, showError, member } = this.props
    const { SiteId, LocationId } = member.currentLocation
    
    fetchLocation(SiteId, LocationId)
      .then(() => {
        this.setState({ isLoading: false })
      })
      .catch((err) => {
        console.log(`Error: ${err}`)
        this.setState({ isLoading: false })
        return showError(err)
      })
  }
}

const mapStateToProps = (state) => ({
  member: state.member || {},
  locations: state.locations || {},
  isLoading: state.status.loading || false,
  nearbyData: state.location.nearby
})

const mapDispatchToProps = {
  fetchLocation: getLocation,
  showError: setError
}

export default connect(mapStateToProps, mapDispatchToProps)(LocationScreen)
