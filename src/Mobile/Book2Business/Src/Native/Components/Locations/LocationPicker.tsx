// @flow

import React from 'react'
import MapView from 'react-native-maps'
import {
  View, TouchableOpacity, Text, Image, Dimensions
} from 'react-native'
import VenueMapCallout from '../VenueMapCallout'
import {
  Left, Right, Body, Button, Title
} from 'native-base'
import { Images, Colors } from '../../Themes'
import styles from '../Styles/LocationPickerStyles'
import Icon from 'react-native-vector-icons/FontAwesome'
import FadeIn from 'react-native-fade-in-image'
import { connect } from 'react-redux'

import List from '../List'
import { getUserPosition, watchUserPosition } from '../../Services/GeolocationService'
import { fetchNearby, setError } from '../../../Actions/nearby'

// Generate this MapHelpers file with `ignite generate map-utilities`
// You must have Ramda as a dev dependency to use this.
import { calculateRegion } from '../../Lib/MapHelpers'
import AnimatedButton from '../AnimatedButton'
import GradientView from '../GradientView'
import GradientHeader from '../GradientHeader'
import ThemeScreen from '../../../ignite/DevScreens/ThemeScreen'

const SCREEN_HEIGHT = Dimensions.get('window').height

/* ***********************************************************
* IMPORTANT!!! Before you get started, if you are going to support Android,
* PLEASE generate your own API key and add it to android/app/src/main/AndroidManifest.xml
* https://console.developers.google.com/apis/credentials
* Also, you'll need to enable Google Maps Android API for your project:
* https://console.developers.google.com/apis/api/maps_android_backend/
*************************************************************/

interface LocationPickerProps {
  screenProps: { toggle (): void }
  initialRegion: object
  locations: object[]
  scrollEnabled: boolean
  mapViewMode: boolean
  style: StyleSheet
  isLoading: boolean
  nearbyData: object
  onCloseMap (): void
  showError (err): object
  getNearby (latlng, pageSize, pageIndex): Promise<object>
  handePickButton (item): void
}

interface LocationPickerState {
  region: object
  locations: object[]
  showUserLocation: boolean
  nearbyData: object[]
  isLoading: boolean
  selectedItem: object
  pageSize: number
  pageIndex: number
  reFetchingStatus: boolean
  fetchingNextPageStatus: boolean
}

const dataArray = [
  // { id: '0', title: "Single", address: "Lorem ipsum dolor sit amet" },
  // { id: '1', title: "Complimentary", address: "Lorem ipsum dolor sit amet" },
  // { id: '2', title: "Redeemable", address: "Lorem ipsum dolor sit amet" },
  // { id: '3', title: "Package", address: "Lorem ipsum dolor sit amet" },
  // { id: '4', title: "Personal Training", address: "Lorem ipsum dolor sit amet" }
]

class LocationPicker extends React.Component<LocationPickerProps, LocationPickerState> {
  /* ***********************************************************
  * This generated code is only intended to get you started with the basics.
  * There are TONS of options available from traffic to buildings to indoors to compass and more!
  * For full documentation, see https://github.com/lelandrichardson/react-native-maps
  *************************************************************/
  constructor (props) {
    super(props)
    /* ***********************************************************
    * STEP 1
    * Set the array of locations to be displayed on your map. You'll need to define at least
    * a latitude and longitude as well as any additional information you wish to display.
    *************************************************************/
    const locations = props.locations || [
      { title: 'The Armory', latitude: 45.524166, longitude: -122.681645 }
    ]
    /* ***********************************************************
    * STEP 2
    * Set your initial region either by dynamically calculating from a list of locations (as below)
    * or as a fixed point, eg: { latitude: 123, longitude: 123, latitudeDelta: 0.1, longitudeDelta: 0.1}
    * You can generate a handy `calculateRegion` function with
    * `ignite generate map-utilities`
    *************************************************************/
    const region = props.initialRegion || calculateRegion(locations, { latPadding: 0.01, longPadding: 0.01 })
    this.state = {
      region,
      locations,
      showUserLocation: true,
      nearbyData: dataArray,
      isLoading: false,
      selectedItem: null,
      pageSize: 20,
      pageIndex: 1,
      reFetchingStatus: false,
      fetchingNextPageStatus: false
    }
    this.renderMapMarkers = this.renderMapMarkers.bind(this)
    this.onRegionChange = this.onRegionChange.bind(this)

    // getUserPosition()
    //   .then(pos => {
    //     this.getNearby(20, 1);
    //   })
    //   .catch(error => {

    //   })

  }

  componentWillReceiveProps (newProps) {
    /* ***********************************************************
    * STEP 3
    * If you wish to recenter the map on new locations any time the
    * props change, do something like this:
    *************************************************************/
    // this.setState({
    //   region: calculateRegion(newProps.locations, { latPadding: 0.1, longPadding: 0.1 })
    // })
  }

  componentDidMount () {
    this.setState({ isLoading: true })
    this.getNearby(this.state.pageSize, 1)
      .then(nearby => {
        this.setState({
          nearbyData: nearby.data,
          pageIndex: 1,
          isLoading: false
        })
      })
  }

  onRefresh = () => {
    this.setState({ reFetchingStatus: true })
    this.getNearby(this.state.pageSize, 1)
      .then(nearby => {
        this.setState({
          nearbyData: nearby.data,
          pageIndex: 1,
          isLoading: false,
          reFetchingStatus: false
        })
      })
  }

  onNextPage = async () => {
    this.setState({ fetchingNextPageStatus: true })
    const { pageSize, pageIndex, nearbyData } = this.state
    const nearby = await this.getNearby(pageSize, pageIndex + 1)
    this.setState({
      nearbyData: [...nearbyData, ...nearby.data],
      pageIndex: pageIndex + 1,
      fetchingNextPageStatus: false
    })
  }

  getNearby = async (pageSize = 20, pageIndex = 1) => {
    const { getNearby, showError } = this.props
    return getNearby(this.state.region, pageSize, pageIndex)
      .then(json => json.data)
      .catch((err) => {
        console.log(`Error: ${err}`)
        return showError(err)
      })
  }

  onRegionChange (newRegion) {
    /* ***********************************************************
    * STEP 4
    * If you wish to fetch new locations when the user changes the
    * currently visible region, do something like this:
    *************************************************************/
    // const searchRegion = {
    //   ne_lat: newRegion.latitude + newRegion.latitudeDelta,
    //   ne_long: newRegion.longitude + newRegion.longitudeDelta,
    //   sw_lat: newRegion.latitude - newRegion.latitudeDelta,
    //   sw_long: newRegion.longitude - newRegion.longitudeDelta
    // }
    // Fetch new data...
    // this.setState({
    //   region: {
    //     latitude: searchRegion.ne_lat,
    //     longitude: searchRegion.ne_long,
    //     latitudeDelta: searchRegion.sw_lat,
    //     longitudeDelta: searchRegion.sw_long
    //   }
    // })
  }

  calloutPress (location) {
    /* ***********************************************************
    * STEP 5
    * Configure what will happen (if anything) when the user
    * presses your callout.
    *************************************************************/

    // console.tron.log(location) // Reactotron
  }

  renderMapMarkers (location) {
    /* ***********************************************************
    * STEP 6
    * Customize the appearance and location of the map marker.
    * Customize the callout in ./VenueMapCallout.js
    *************************************************************/

    return (
      <MapView.Marker
        key={location.title}
        image={Images.markerIcon}
        coordinate={{ latitude: location.latitude, longitude: location.longitude }}>
        <VenueMapCallout location={location} onPress={this.calloutPress} />
      </MapView.Marker>
    )
  }

  renderMapCloseButton = () => {
    // Warning GROSS hack for Android render bug on maps
    const left = this.props.mapViewMode ? 0 : -100

    return (
      <TouchableOpacity
        onPress={this.props.onCloseMap}
        style={[styles.mapCloseButton, { left }]}
      >
        <Icon
          ref='mapCloseButton'
          name='times-circle'
          size={26}
          color={Colors.purple}
          style={[styles.mapCloseButton]}
        />
      </TouchableOpacity>
    )
  }

  renderLocationList = () => {
    return (
      <View style={{ height: (SCREEN_HEIGHT - 85 - 180) }}>
        <List
          data={this.state.nearbyData}
          renderItem={this.renderRow.bind(this)}
          keyExtractor={(item, idx) => item.id}
          error={this.props.nearbyData.error}
          loading={this.state.isLoading}
          refresh={this.onRefresh.bind(this)}
          loadMore={this.onNextPage.bind(this)}
          refreshing={this.state.reFetchingStatus}
          loadingMore={this.state.fetchingNextPageStatus} />
      </View>
    )
  }

  onSelectRow (item) {
    this.setState({
      selectedItem: item,
      locations: [{ ...item.location, title: item.title }]
    })
  }

  renderRow ({ item }) {
    const { address, title } = item
    return (
      <AnimatedButton
        onPress={() => {
          this.onSelectRow(item)
        }}>

        <View style={styles.infoText}>
          <Text style={styles.title}>{title}</Text>
          <Text style={styles.name}>{address}</Text>
        </View>
        {this.state.selectedItem === item && <FadeIn style={{ justifyContent: 'center' }}>
          <Icon name='check' size={20} />
        </FadeIn>
        }
      </AnimatedButton>
    )
  }

  renderHeader () {
    return (

      <GradientHeader>
        <View style={[styles.header]}>
          <Left>
            <TouchableOpacity style={styles.backButton} onPress={this.props.screenProps.toggle}>
              <Image style={styles.backButtonIcon} source={Images.arrowIcon} />
            </TouchableOpacity>
          </Left>
          <Body>
            <Title style={{ color: Colors.snow }}>Pick a location</Title>
          </Body>
          <Right>
            <Button transparent onPress={() => {
              this.props.handePickButton(this.state.selectedItem)
              this.props.screenProps.toggle()
            }}>
              <Title style={{ color: Colors.snow }}>Done</Title>
            </Button>
          </Right>
        </View>
      </GradientHeader>
    )
  }

  renderMapView () {
    return (
      <View ref='mapContainer'>
        <View>
          <MapView
            scrollEnabled={this.props.scrollEnabled}
            style={[styles.map]}
            initialRegion={this.state.region}
            onRegionChangeComplete={this.onRegionChange}
            showsUserLocation={this.state.showUserLocation}
          >
            {this.state.locations.map((location) => this.renderMapMarkers(location))}
          </MapView>
          {this.renderMapCloseButton()}
        </View>
      </View>
    )
  }

  render () {
    return (
      <GradientView style={[styles.linearGradient]}>
        {this.renderHeader()}
        {this.renderMapView()}
        {this.renderLocationList()}
      </GradientView>

    )
  }
}

const mapStateToProps = (state) => {
  return {
    isLoading: state.status.loading || false,
    nearbyData: state.nearby
  }
}

const mapDispatchToProps = {
  getNearby: fetchNearby,
  showError: setError
}

export default connect(mapStateToProps, mapDispatchToProps)(LocationPicker)
