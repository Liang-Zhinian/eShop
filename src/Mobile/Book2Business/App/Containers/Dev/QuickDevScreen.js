
import React, { Component } from 'react'
import { AppState, View, Image, FlatList, ScrollView, Button } from 'react-native'
import GradientView from '../../Components/GradientView'
import {
  compareAsc,
  isSameDay,
  addMinutes,
  isWithinRange,
  subMilliseconds
} from 'date-fns'
import {
  merge,
  groupWith,
  contains,
  assoc,
  map,
  sum,
  findIndex
} from 'ramda'
import {
  Container, Content, List, ListItem, Body, Left, Text, Icon
} from 'native-base'
import { connect } from 'react-redux'
import NotificationActions from '../../Redux/NotificationRedux'
import Config from '../../Config/AppConfig'
import { Images } from '../../Themes'
import styles from './Styles/QuickDevScreenStyle'
import Link from '../../Components/Link'
import LocationPickerButton from '../../Components/Locations/LocationPickerButton'
import ImageCropperButton from '../../Components/Locations/ImageCropperButton'
import switchTheme from '../../Actions/theme'
import MainContainer from '../MainContainer'

class QuickDevScreen extends Component {
  constructor (props) {
    super(props)

    const appState = AppState.currentState

    this.state = { appState }
  }

  static navigationOptions = {
    tabBarLabel: 'More',
    tabBarIcon: ({ focused }) => (
      <Image
        source={
          focused
            ? Images.activeInfoIcon
            : Images.inactiveInfoIcon
        }
      />
    ),
    title: 'Quick dev'
  }
  // static navigationOptions = ({ navigation }) => {
  //   return {
  //     tabBarLabel: 'More',
  //     tabBarIcon: ({ focused }) => (
  //       <Image
  //         source={
  //           focused
  //             ? Images.activeInfoIcon
  //             : Images.inactiveInfoIcon
  //         }
  //       />
  //     ),
  //     title: 'Quick dev'
  //   }
  // };

  componentDidMount () {
    AppState.addEventListener('change', this._handleAppStateChange)
  }

  componentWillUnmount () {
    AppState.removeEventListener('change', this._handleAppStateChange)
  }

  render () {
    // console.log('MainContainer', this.props.theme)
    return (

      <MainContainer
        navigation={this.props.navigation}
        header={{
          headerTitle: 'Quick Dev'
        }}
        scrollEnabled
        style={[{ paddingHorizontal: 10 }]}
      >
        <LocationPickerButton
          mapViewMode={false}
          scrollEnabled
          initialRegion={{ title: 'Chanel@Elements', 'latitude': 22.3048708, 'longitude': 114.1615219, latitudeDelta: 0.05, longitudeDelta: 0.01 }}
          locations={[{ title: 'Chanel@Elements', 'latitude': 22.3048708, 'longitude': 114.1615219 }]} />

        <Button title='Update Location Image' onPress={() => { this.props.navigation.navigate('UpdateBackgroundScreen') }} />

        <ImageCropperButton
          handePickButton={(image) => {
          }} />

        <Button title='Switch theme' onPress={() => { this.props.switchTheme('Lite') }} />
      </MainContainer>
    )
  }

  _handleAppStateChange = (nextAppState) => {
    const { appState } = this.state
    if (appState.match(/inactive|background/) && nextAppState === 'active') {
      // this.props.getScheduleUpdates()
    }
    this.setState({ appState: nextAppState })
  }

  renderItem = ({ item }) => {
    return (
      <Link
        avatarURL={item.image}
        title={item.title}
        onPress={() => this.onEventPress(item)}
      />
    )
  }

  getItemLayout = (data, index) => {
    const item = data[index]
    const itemLength = (item, index) => {
      if (item.type === 'talk') {
        // use best guess for variable height rows
        return 138 + (1.002936 * item.title.length + 6.77378)
      } else {
        return 145
      }
    }
    const length = itemLength(item)
    const offset = sum(data.slice(0, index).map(itemLength))
    return { length, offset, index }
  }

  onEventPress = (item) => {
    const { navigation } = this.props

    navigation.navigate(item.target)
  }
}

const mapStateToProps = (state) => {
  return {
    theme: state.theme.name
  }
}

const mapDispatchToProps = {
  switchTheme: switchTheme
}

export default connect(mapStateToProps, mapDispatchToProps)(QuickDevScreen)
