
import React, { Component } from 'react'
import { AppState, View, Image, FlatList, ScrollView } from 'react-native'
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
import NotificationActions from '../../Redux/NotificationRedux'
import Config from '../../Config/AppConfig'
import { Images } from '../../Themes'
import styles from './Styles/MoreScreenStyle'
import Link from '../../Components/Link'
import MainContainer from '../MainContainer'

const data = [
  {
    id: '0',
    title: 'Appointments',
    image: Images.chevronRight,
    target: 'Appointments'
  },
  {
    id: '1',
    title: 'Classes',
    image: Images.chevronRight,
    target: 'Classes'
  },
  {
    id: '2',
    title: 'Business information',
    image: Images.chevronRight,
    target: 'BizInfo'
  },
  {
    id: '10',
    title: 'Staffs',
    image: Images.chevronRight,
    target: 'Staffs'
  },
  {
    id: '3',
    title: 'Profile',
    image: Images.chevronRight,
    target: 'Profile'
  },
  {
    id: '4',
    title: 'Change location',
    image: Images.locationIcon,
    target: 'ChangeLocation'
  },
  /*{
    id: '5',
    title: 'Location',
    image: Images.inactiveLocationIcon,
    target: 'Location'
  },*/
  {
    id: '6',
    title: 'About',
    image: Images.inactiveInfoIcon,
    target: 'About'
  },
  {
    id: '7',
    title: 'Quick Dev',
    image: Images.inactiveInfoIcon,
    target: 'QuickDev'
  }
]

class MoreScreen extends Component {
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
    header: null
  }

  componentDidMount () {
    AppState.addEventListener('change', this._handleAppStateChange)
  }

  componentWillUnmount () {
    AppState.removeEventListener('change', this._handleAppStateChange)
  }

  render () {
    return (
      <MainContainer
        header={null}>
        <FlatList
          ref='moreMenuList'
          data={data}
          extraData={this.props}
          renderItem={this.renderItem}
          keyExtractor={(item, idx) => item.id}
          contentContainerStyle={styles.listContent}
          getItemLayout={this.getItemLayout}
          showsVerticalScrollIndicator={false}
        />
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

export default MoreScreen
