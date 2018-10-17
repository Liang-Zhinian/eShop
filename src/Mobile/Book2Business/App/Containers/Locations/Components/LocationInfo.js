import React from 'react'
import {
  AppState,
  ScrollView,
  TouchableOpacity,
  View,
  Image,
  Text,
  Linking,
  Animated,
  PanResponder,
  LayoutAnimation
} from 'react-native'
import PropTypes from 'prop-types'
import Secrets from 'react-native-config'

import GradientView from '../../../Components/GradientView'
import Messages from '../../../Components/Messages'
import Loading from '../../../Components/Loading'
import VenueMap from '../../../Components/VenueMap'
import Gallery from '../../../Components/Gallery'
import RoundedButton from '../../../Components/RoundedButton'
import { Images, Metrics } from '../../../Themes'
import styles from './Styles/LocationInfoStyle'

const VENUE_LATITUDE = 45.524166
const VENUE_LONGITUDE = -122.681645
const { UBER_CLIENT_ID } = Secrets
const MAP_TAP_THRESHOLD = 100

export default class LocationInfo extends React.Component {
  static propTypes = {
    error: PropTypes.string,
    loading: PropTypes.bool.isRequired,
    locationData: PropTypes.shape({}).isRequired,
  }

  constructor(props) {
    super(props)

    const appState = AppState.currentState
    this.state = {
      showRideOptions: false,
      showGalleryOptions: false,
      scrollY: new Animated.Value(0),
      mapTouchStart: '',
      mapViewMode: false,
      appState,
      shouldComponentUpdate: false
    }

    this.scrollSpot = Metrics.screenHeight / 4.25
    this.activeMapHeight = Metrics.screenHeight - this.scrollSpot
  }

  componentWillMount() {
    // Get the map tap check
    this._panResponder = PanResponder.create({
      onStartShouldSetPanResponder: () => true,
      onPanResponderGrant: (e) => this.setState({ mapTouchStart: e.nativeEvent.timestamp }),
      onPanResponderRelease: this.checkMapTap
    })
  }

  componentDidMount() {
    AppState.addEventListener('change', this._handleAppStateChange)
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

  checkMapTap = (e) => {
    if (e.nativeEvent.timestamp - this.state.mapTouchStart < MAP_TAP_THRESHOLD) {
      LayoutAnimation.configureNext({ ...LayoutAnimation.Presets.linear, duration: 500 })
      this.refs.scrolly.scrollTo({ y: this.scrollSpot, animated: true })
      this.setState({ mapViewMode: true })
    }
    this.setState({ mapTouchStart: '' })
  }

  openMaps(daddr = '128+NW+Eleventh+Ave+Portland,+OR+97209') {
    const googleMaps = `comgooglemaps://?daddr=${daddr}`
    const appleMaps = `http://maps.apple.com?daddr=${daddr}`

    Linking.canOpenURL(googleMaps).then((supported) => {
      if (supported) {
        Linking.openURL(googleMaps)
      } else {
        Linking.canOpenURL(appleMaps).then((supported) =>
          supported
            ? Linking.openURL(appleMaps)
            : window.alert('Unable to find maps application.')
        )
      }
    })
  }

  openLink(url) {
    Linking.canOpenURL(url).then((supported) => {
      if (supported) {
        Linking.openURL(url)
      } else {
        window.alert('Unable to open link')
      }
    })
  }

  openLyft() {
    const lat = `destination[latitude]=${VENUE_LATITUDE}`
    const lng = `destination[longitude]=${VENUE_LONGITUDE}`
    const lyft = `lyft://ridetype?${lat}&${lng}`

    Linking.canOpenURL(lyft).then((supported) => {
      if (supported) {
        Linking.openURL(lyft)
      } else {
        window.alert('Unable to open Lyft.')
      }
    })
  }

  openUber() {
    const pickup = 'action=setPickup&pickup=my_location'
    const client = `client_id=${UBER_CLIENT_ID}`
    const lat = `dropoff[latitude]=${VENUE_LATITUDE}`
    const lng = `dropoff[longitude]=${VENUE_LONGITUDE}`
    const nick = `dropoff[nickname]=The%20Armory`
    const daddr = `dropoff[formatted_address]=128%20NW%20Eleventh%20Ave%2C%20Portland%2C%20OR%2097209`
    const uber = `uber://?${pickup}&${client}&${lat}&${lng}&${nick}&${daddr}`

    Linking.canOpenURL(uber).then((supported) => {
      if (supported) {
        Linking.openURL(uber)
      } else {
        window.alert('Unable to open Uber.')
      }
    })
  }

  toggleRides = () => {
    const { showRideOptions, scrollY } = this.state
    if (!showRideOptions && scrollY._value < 200) {
      this.refs.scrolly.scrollTo({ x: 0, y: 200, animated: true })
    }
    this.setState({ showRideOptions: !this.state.showRideOptions })
  }

  toggleGallery = () => {
    const { showGalleryOptions, scrollY } = this.state
    if (!showGalleryOptions && scrollY._value < 200) {
      this.refs.scrolly.scrollTo({ x: 0, y: 200, animated: true })
    }
    this.setState({ showGalleryOptions: !this.state.showGalleryOptions })
  }

  renderBackground = () => {
    const { ImageUri } = this.props.locationData
    const height = Metrics.locationBackgroundHeight
    const { scrollY } = this.state
    return (
      <Animated.Image
        style={[styles.venue, { position: 'absolute' }, {
          width: '100%',
          height: height,
          transform: [{
            translateY: scrollY.interpolate({
              inputRange: [-height, 0, height],
              outputRange: [height, 0, 0]
            })
          }, {
            scale: scrollY.interpolate({
              inputRange: [-height, 0, height],
              outputRange: [0.9, 1, 1.5]
            })
          }]
        }]}
        source={{ uri: ImageUri }}
        resizeMode='cover'
      />
    )
  }

  renderHeader = () => {
    const { Name, Address: { Street } } = this.props.locationData
    const height = Metrics.locationBackgroundHeight - 24
    const { scrollY } = this.state
    return (
      <Animated.View style={{
        position: 'relative',
        height: height,
        padding: 0,
        opacity: scrollY.interpolate({
          inputRange: [-height, 0, height * 0.4, height * 0.9],
          outputRange: [1, 1, 1, 0]
        }),
        transform: [{
          translateY: scrollY.interpolate({
            inputRange: [-height, 0, height * 0.45, height],
            outputRange: [0, 0, height * 0.45, height * 0.4]
          })
        }]
      }}>
        <View style={styles.headingContainer}>
          <Text style={styles.mainHeading}>{Name}</Text>
          <Text style={styles.address}>
            {Street}
          </Text>
        </View>
        <View style={styles.headingContainer}>
          <RoundedButton
            text='Change background image'
            onPress={() => null}
            style={styles.slackButton}
          />
          {/* <RoundedButton
            text='Change address'
            onPress={() => null}
            style={styles.slackButton}
          /> */}
        </View>
      </Animated.View>
    )
  }

  renderMap() {
    const {
      Name,
      Geolocation
    } = this.props.locationData
    const { mapViewMode } = this.state
    return (
      <View ref='mapContainer' {...this._panResponder.panHandlers}>
        <VenueMap
          locations={[{ title: Name, latitude: Geolocation.Latitude, longitude: Geolocation.Longitude }]}
          mapViewMode={mapViewMode}
          onCloseMap={this.onCloseMap}
          scrollEnabled={mapViewMode}
          style={[styles.map, mapViewMode && { height: this.activeMapHeight }]}
        />
      </View>
    )
  }

  onCloseMap = () => {
    LayoutAnimation.configureNext({ ...LayoutAnimation.Presets.linear, duration: 400 })
    this.setState({ mapViewMode: false })
  }

  updateAddress = () => {
    const { Street } = this.props.locationData.Address
    this.openMaps(Street)
  }

  render() {
    const { loading, error, success } = this.props
    if (loading) return <Loading />
    if (error) return <Messages message={error} />


    // if (!this.props.locationData || !this.props.locationData.Id) return null

    const { showRideOptions, showGalleryOptions } = this.state
    const { nearbyData } = this.props
    const { event } = Animated
    const {
      Name,
      Address: { Street },
      ContactInformation: {
        ContactName, EmailAddress, PrimaryTelephone, SecondaryTelephone
      },
      AdditionalLocationImages
    } = this.props.locationData

    let gallery = {
      'Gallery': AdditionalLocationImages.map((item, index) => {
        return {
          "name": item.Image,
          "image": "deschutesBrewery",
          "address": "210 NW 11th Ave, Portland, OR 97209",
          "link": item.ImageUrl
        }
      })
    }

    return (

      <GradientView style={[styles.linearGradient, { flex: 1 }]}>
        <ScrollView
          ref='scrolly'
          onScroll={event([{ nativeEvent: { contentOffset: { y: this.state.scrollY } } }])}
          scrollEventThrottle={10}
          scrollEnabled={!this.state.mapViewMode}>
          <View style={styles.container}>
            {this.renderBackground()}
            {this.renderHeader()}
            {this.renderMap()}
            <View style={styles.mapActions}>
              <TouchableOpacity onPress={() => this.updateAddress()}>
                <View style={styles.getDirections}>
                  <View style={styles.addressContainer}>
                    <Text style={styles.venueName}>
                      {Name}
                    </Text>
                    <Text style={styles.venueAddress}>
                      {Street}
                    </Text>
                  </View>
                  <View style={styles.directionsIcon}>
                    <Image source={Images.directionsIcon} />
                    <Text style={styles.directionsLabel}>Update address</Text>
                  </View>
                </View>
              </TouchableOpacity>
              <TouchableOpacity onPress={() => this.toggleRides()}>
                <View style={styles.getRide}>
                  <Text style={styles.getRideLabel}>
                    Contact information
                  </Text>
                  <Image
                    style={[styles.getRideIcon, showRideOptions && styles.flip]}
                    source={Images.chevronIcon}
                  />
                </View>
              </TouchableOpacity>
            </View>
            <View style={[styles.rideOptions, showRideOptions && { height: 270 }]}>
              {/* <TouchableOpacity onPress={() => this.openLyft()}>
                <Image style={styles.rideButton} source={Images.lyftButton} />
              </TouchableOpacity> */}
              <View style={styles.liveHelp}>
                <Text style={styles.liveHelpPhone}>
                  {PrimaryTelephone}
                </Text>
                <Text style={styles.liveHelpText}>
                  Text or call {ContactName} at anytime for directions, suspicious activity or any other concern,
        or email us via <Text style={styles.link} onPress={() => Linking.openURL('http://confcodeofconduct.com')}>{EmailAddress}</Text>.
      </Text>
                <RoundedButton
                  text='Update contact information'
                  onPress={() => Linking.openURL('sms:3605620450')}
                  style={styles.liveHelpButton}
                />
              </View>
              {/* <TouchableOpacity onPress={() => this.openUber()}>
                <Image style={styles.rideButton} source={Images.uberButton} />
              </TouchableOpacity> */}
            </View>
            <View style={styles.nearby}>
              {/* <Text style={styles.mainHeading}>
                Additional images
              </Text> */}
              <TouchableOpacity onPress={() => this.toggleGallery()}>
                <View style={styles.getRide}>
                  <Text style={styles.mainHeading}>
                    Additional images
                  </Text>
                  <Image
                    style={[styles.getRideIcon, showGalleryOptions && styles.flip]}
                    source={Images.chevronIcon}
                  />
                </View>
              </TouchableOpacity>
            </View>
            <Gallery
              data={{...nearbyData, ...gallery}}
              onItemPress={(link) => this.openLink(link)}
            />
          </View>
        </ScrollView>
      </GradientView>
    )
  }
}
