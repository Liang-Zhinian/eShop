
import React, { Component } from 'react'
import {
  AppRegistry,
  StyleSheet,
  Text,
  View,
  Animated,
  ListView,
  Image,
  Dimensions,
  AlertIOS,
  TouchableOpacity,
  TouchableHighlight,
  TouchableWithoutFeedback
} from 'react-native'
import { connect } from 'react-redux'
import Icon from 'react-native-vector-icons/FontAwesome'

const { width, height } = Dimensions.get('window')

import { Images } from '../../Themes'
import recipes from './data'
import AnimatedContainerWithNavbar from '../../Components/AnimatedContainerWithNavbar'
import Layout from '../../Components/Locations/LocationInfo'
import { getLocation, setError } from '../../Actions/locations'

class LocationScreen extends Component {
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

  constructor () {
    super()
    const ds = new ListView.DataSource({ rowHasChanged: (r1, r2) => r1 !== r2 })

    this.state = {
      isMenuOpen: false,
      dataSource: ds.cloneWithRows(recipes)
    }
  }

  animatedContainerWithNavbar = null;

  componentDidMount () {
    this.setState({
      loaded: true
    })

    const { SiteId, LocationId } = this.props.member.currentLocation
    const { getLocation, showError } = this.props
    getLocation(SiteId, LocationId)
      .catch((err) => {
        console.log(`Error: ${err}`)
        return showError(err)
      })

    this.props.navigation.setParams({
      icon: 'bars',
      pressMenu: this.toggleMenu.bind(this)
    })

  }

  toggleMenu () {
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

  renderRow (rowData) {
    const img = rowData.image
    return (
      <TouchableHighlight style={[styles.containerRow]}>
        <View>
          <Image
            style={{
              width: width,
              height: 170
            }}
            source={{ uri: img }}
          />
          <View
            style={styles.imageFooter}
          >
            <View style={styles.imageFooterUser}>
              <Image
                style={styles.imageAvatar}
                source={{ uri: rowData.user }}
              />
            </View>
            <View style={styles.imageFooterText}>
              <Text style={[styles.textFooter, styles.textFooterBold]}>{rowData.food}</Text>
              <Text style={styles.textFooter}>{rowData.title}</Text>
              <Text style={[styles.textFooter, styles.textFooterLight, styles.textFooterLittle]}>By {rowData.by}</Text>
            </View>
          </View>
        </View>
      </TouchableHighlight>
    )
  }

  goTo (route) {
    setTimeout(this.toggleMenu.bind(this), 1000)
    this.props.navigation.navigate(route)
  }

  render () {
    const { locations, member, nearbyData, navigation } = this.props
    console.log(locations)

    return (
      <AnimatedContainerWithNavbar
        ref={ref => this.animatedContainerWithNavbar = ref}
        menuPosition='right'
        content={(<Layout 
          nearbyData={nearbyData} 
          locationData={locations.currentLocation} 
          navigation={navigation}
          error={locations.error}
          loading={locations.loading} />)}
        // content={(<View />)}
        menu={(
          <View>
            <TouchableOpacity style={{}} onPress={() => {
              // this.toggleMenu()
              this.goTo('UpdateInfoScreen')
            }} >
              <Text style={[styles.textFooter, styles.menutext]}>{'Name & Description'}</Text>
            </TouchableOpacity >
            <TouchableOpacity style={{}} onPress={() => {
              // this.toggleMenu()
              this.goTo('UpdateBackgroundScreen')
            }} >
              <Text style={[styles.textFooter, styles.menutext]}>Background image</Text>
            </TouchableOpacity >
            <TouchableOpacity style={{}} onPress={() => {
              // this.toggleMenu()
              this.goTo('UpdateAddressScreen')
            }} >
              <Text style={[styles.textFooter, styles.menutext]}>Address</Text>
            </TouchableOpacity >
            <TouchableOpacity style={{}} onPress={() => {
              // this.toggleMenu()
              this.goTo('UpdateGeolocationScreen')
            }} >
              <Text style={[styles.textFooter, styles.menutext]}>Geolocation</Text>
            </TouchableOpacity >
            <TouchableOpacity style={{}} onPress={() => {
              // this.toggleMenu()
              this.goTo('UpdateContactScreen')
            }} >
              <Text style={[styles.textFooter, styles.menutext]}>Contact</Text>
            </TouchableOpacity >
            <TouchableOpacity style={{}} onPress={() => {
              // this.toggleMenu()
              this.goTo('AdditionalImagesScreen')
            }} >
              <Text style={[styles.textFooter, styles.menutext]}>Additional images</Text>
            </TouchableOpacity >
          </View>
        )}
      />
    )
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#555566',
    paddingTop: 20
  },
  content: {
    zIndex: 3
  },
  containerRow: {
    marginBottom: 10
  },
  listView: {
    marginHorizontal: 10
  },
  imageFooter: {
    backgroundColor: '#555566',
    flexDirection: 'row',
    paddingHorizontal: 7,
    paddingVertical: 7
  },
  textFooter: {
    color: '#fff'
  },
  textFooterBold: {
    fontWeight: 'bold'
  },
  imageAvatar: {
    width: 50,
    height: 50,
    borderRadius: 25,
    marginRight: 7
  },
  textFooterLight: {
    fontWeight: '100'
  },
  textFooterLittle: {
    fontSize: 11
  },
  menutext: {
    fontSize: 20,
    padding: 10
  },
  menu: {
    width: width,
    height: height,
    flex: 1,
    position: 'absolute',
    left: 0,
    top: 0,
    backgroundColor: '#ff00ff'
  },
  menulist: {
    width: 200,
    position: 'absolute',
    right: 0,
    top: 100
  }
})

const mapStateToProps = (state) => {
  return {
    member: state.member || {},
    locations: state.locations || {},
    isLoading: state.status.loading || false,
    nearbyData: state.location.nearby
  }
}

const mapDispatchToProps = {
  getLocation: getLocation,
  showError: setError
}

export default connect(mapStateToProps, mapDispatchToProps)(LocationScreen)
