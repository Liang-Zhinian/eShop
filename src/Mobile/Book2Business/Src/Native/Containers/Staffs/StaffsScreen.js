import React, { Component } from 'react'
import {
  AppState,
  FlatList,
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
import GradientView from '../../Components/GradientView'
import DayToggle from '../../Components/DayToggle'
import Staff from '../../Components/Staffs/Staff'
import Break from '../../Components/Break'
import ScheduleActions from '../../../Redux/ScheduleRedux'
import * as StaffActions from '../../../Actions/staffs'
import { connect } from 'react-redux'
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
import NotificationActions from '../../../Redux/NotificationRedux'
import Config from '../../../Config/AppConfig'
import { Images } from '../../Themes'
import styles from './Styles/StaffsScreenStyle'
import AnimatedContainerWithNavbar from '../../Components/AnimatedContainerWithNavbar'
import Icon from 'react-native-vector-icons/FontAwesome'

const isActiveCurrentDay = (currentTime, activeDay) =>
  isSameDay(currentTime, new Date(Config.conferenceDates[activeDay]))

const addSpecials = (specialTalksList, talks) =>
  map((talk) => assoc('special', contains(talk.title, specialTalksList), talk), talks)

class StaffsScreen extends Component {
  constructor (props) {
    super(props)

    const appState = AppState.currentState

    this.state = { data: [], appState, isMenuOpen: false }
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

  onEventPress = (item) => {
    const { navigation, setSelectedEvent } = this.props
    setSelectedEvent(item)

    // item.type === 'talk'
    //   ? navigation.navigate('TalkDetail')
    //   : navigation.navigate('BreakDetail')
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

  componentDidMount () {
    AppState.addEventListener('change', this._handleAppStateChange)

    const { member, getStaffs } = this.props

    getStaffs(member.currentLocation.SiteId)

    this.props.navigation.setParams({
      icon: 'bars',
      pressMenu: this.toggleMenu.bind(this)
    })
  }

  componentWillUnmount () {
    AppState.removeEventListener('change', this._handleAppStateChange)
  }

  _handleAppStateChange = (nextAppState) => {
    const { appState } = this.state
    if (appState.match(/inactive|background/) && nextAppState === 'active') {
      // this.props.getScheduleUpdates()
    }
    this.setState({ appState: nextAppState })
  }

  componentWillReceiveProps (newProps) {
    const { staffs } = newProps

    this.setState({data: staffs.staffs})
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

  // if value exists, create the function calling it, otherwise false
  funcOrFalse = (func, val) => val ? () => func.call(this, val) : false

  renderItem = ({ item }) => {
    return (
      <Staff
        type={'talk'}
        name={item.FirstName + ' ' + item.LastName}
        avatarURL={item.ImageUri||''}
        title={item.Bio}
        onPress={() => this.onEventPress(item)}
        />
    )
  }

  renderContent () {
    const { isCurrentDay, activeDay, data } = this.state
    return (
      <GradientView style={styles.linearGradient}>
        <DayToggle
          activeDay={activeDay}
          onPressIn={this.setActiveDay}
        />
        <FlatList
          ref='scheduleList'
          data={data}
          extraData={this.props}
          renderItem={this.renderItem}
          keyExtractor={(item, idx) => item.eventStart}
          contentContainerStyle={styles.listContent}
          getItemLayout={this.getItemLayout}
          showsVerticalScrollIndicator={false}
        />
      </GradientView>
    )
  }

  render () {
    const { data } = this.state
    const { navigation } = this.props

    return (
      <AnimatedContainerWithNavbar
        ref={ref => this.animatedContainerWithNavbar = ref}
        menuPosition='right'
        content={(
          <GradientView style={[styles.linearGradient, { flex: 1 }]}>
            <FlatList
              ref='scheduleList'
              data={data}
              extraData={this.props}
              renderItem={this.renderItem}
              keyExtractor={(item, idx) => item.eventStart}
              contentContainerStyle={styles.listContent}
              getItemLayout={this.getItemLayout}
              showsVerticalScrollIndicator={false}
            />
          </GradientView>
        )}
        // content={(<View />)}
        menu={(
          <View>
            <TouchableOpacity style={{}} onPress={() => {
              // this.toggleMenu()
              // this.goTo('UpdateInfoScreen')
            }} >
              <Text style={[styles.textFooter, styles.menutext]}>{'Name & Description'}</Text>
            </TouchableOpacity >
          </View>
        )}
      />
    )
  }
}

const mapStateToProps = (state) => {
  return {
    // currentTime: new Date(state.schedule.currentTime),
    // schedule: state.schedule.speakerSchedule,
    // specialTalks: state.notifications.specialTalks,
    staffs: state.staffs,
    member: state.member,
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    // getScheduleUpdates: () => dispatch(ScheduleActions.getScheduleUpdates()),
    // setSelectedEvent: data => dispatch(ScheduleActions.setSelectedEvent(data)),
    // onPressGithub: url => dispatch(ScheduleActions.visitGithub(url)),
    // onPressTwitter: url => dispatch(ScheduleActions.visitTwitter(url)),
    // setReminder: title => dispatch(NotificationActions.addTalk(title)),
    // removeReminder: title => dispatch(NotificationActions.removeTalk(title)),
    getStaffs: siteId => dispatch(StaffActions.getStaffs(siteId))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(StaffsScreen)
