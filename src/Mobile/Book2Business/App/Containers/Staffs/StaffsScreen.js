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
import Icon from 'react-native-vector-icons/FontAwesome'
import PropTypes from 'prop-types'

import Staff from './Components/Staff'
import * as StaffActions from '../../Actions/staffs'
import { Images } from '../../Themes'
import styles from './Styles/StaffsScreenStyle'
import AnimatedContainerWithNavbar from '../../Components/AnimatedContainerWithNavbar'
import List from '../../Components/List/List'

class StaffsScreen extends Component {
  static propTypes = {
    member: PropTypes.shape({}).isRequired,
    staffs: PropTypes.shape({
      loading: PropTypes.bool.isRequired,
      error: PropTypes.string,
      staffs: PropTypes.arrayOf(PropTypes.shape({})),
    }).isRequired,
    match: PropTypes.shape({
      params: PropTypes.shape({})
    }),
    fetchStaffs: PropTypes.func.isRequired,
    showError: PropTypes.func.isRequired
  }

  static defaultProps = {
    match: null
  }

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

  onEventPress = (item) => {
    const { navigation, setSelectedStaff } = this.props
    setSelectedStaff(item)

    navigation.navigate('Staff')
  }

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

    this.fetchStaffs()
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

  renderItem = ({ item }) => {
    return (
      <Staff
        type={'talk'}
        name={item.FirstName + ' ' + item.LastName}
        avatarURL={item.ImageUri || ''}
        title={item.Bio}
        onPress={() => this.onEventPress(item)}
      />
    )
  }

  fetchStaffs() {
    const { member, fetchStaffs, showError } = this.props

    fetchStaffs(member.SiteId)
      .catch((err) => {
        console.log(`Error: ${err}`)
        return showError(err)
      })
  }

  render() {
    const { navigation, staffs, member } = this.props

    let listViewData = staffs && staffs.staffs ? staffs.staffs : null

    return (
      <AnimatedContainerWithNavbar
        ref={ref => this.animatedContainerWithNavbar = ref}
        menuPosition='right'
        content={(
          <List
            headerTitle='Staffs'
            navigation={navigation}
            data={listViewData}
            renderItem={this.renderItem.bind(this)}
            keyExtractor={(item, idx) => item.Id}
            contentContainerStyle={styles.listContent}
            showsVerticalScrollIndicator={false}
            reFetch={this.fetchStaffs.bind(this)}
            error={staffs.error}
            loading={staffs.loading}
          />
        )}
        // content={(<View />)}
        menu={[
          {text:'Add Staff',onPress:()=>{ this.props.navigation.navigate('AddStaff')}}
        ]}
      />
    )
  }
}

const mapStateToProps = (state) => {
  return {
    staffs: state.staffs || {},
    member: state.member || {}
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    setSelectedStaff: (staff) => dispatch(StaffActions.setSelectedStaff(staff)),
    fetchStaffs: (siteId) => dispatch(StaffActions.getStaffs(siteId)),
    showError: (err) => dispatch(StaffActions.setError(err))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(StaffsScreen)
