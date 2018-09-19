
import React, { Component } from 'react'
import { AppState, View, Image, FlatList } from 'react-native'
import PurpleGradient from '../../Components/PurpleGradient'
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
import NotificationActions from '../../Redux/NotificationRedux'
import Config from '../../Config/AppConfig'
import { Images } from '../../Themes'
import styles from '../Styles/ScheduleScreenStyle'

class MoreScreen extends Component {
  constructor (props) {
    super(props)

    const appState = AppState.currentState

    this.state = {appState}
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
    )
  }

  componentDidMount () {
    AppState.addEventListener('change', this._handleAppStateChange)
  }

  componentWillUnmount () {
    AppState.removeEventListener('change', this._handleAppStateChange)
  }

  render () {
    return (
      <PurpleGradient style={styles.linearGradient}>
        
      </PurpleGradient>
    )
  }
}

export default MoreScreen
