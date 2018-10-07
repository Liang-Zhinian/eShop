import React, { Component } from 'react'
import {
  ScrollView,
  Keyboard,
  LayoutAnimation
} from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Logo, Form, Wallpaper, ButtonSubmit, SignupSection } from './components'
import { login } from '../../Actions/member'
import { Images, Metrics } from '../../Themes'
import LoginActions from '../../Redux/LoginRedux'
import styles from './Styles/LoginScreenStyles'

class LoginScreen extends Component {
  static propTypes = {
    dispatch: PropTypes.func,
    fetching: PropTypes.bool,
    attemptLogin: PropTypes.func
  }

  isAttempting = false
  keyboardDidShowListener = {}
  keyboardDidHideListener = {}

  constructor (props) {
    super(props)
    this.state = {
      username: '',
      password: '',
      visibleHeight: Metrics.screenHeight,
      topLogo: { width: Metrics.screenWidth }
    }
    this.isAttempting = false
  }

  componentWillReceiveProps (newProps) {
    this.forceUpdate()
    // Did the login attempt complete?
    if (this.isAttempting && !newProps.fetching) {
      this.props.navigation.goBack()
    }
  }

  componentWillMount () {
    // Using keyboardWillShow/Hide looks 1,000 times better, but doesn't work on Android
    // TODO: Revisit this if Android begins to support - https://github.com/facebook/react-native/issues/3468
    // this.keyboardDidShowListener = Keyboard.addListener('keyboardDidShow', this.keyboardDidShow)
    // this.keyboardDidHideListener = Keyboard.addListener('keyboardDidHide', this.keyboardDidHide)
  }

  componentWillUnmount () {
    // this.keyboardDidShowListener.remove()
    // this.keyboardDidHideListener.remove()
  }

  keyboardDidShow = (e) => {
    // Animation types easeInEaseOut/linear/spring
    LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut)
    let newSize = Metrics.screenHeight - e.endCoordinates.height
    this.setState({
      visibleHeight: newSize,
      topLogo: { width: 100, height: 70 }
    })
  }

  keyboardDidHide = (e) => {
    // Animation types easeInEaseOut/linear/spring
    LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut)
    this.setState({
      visibleHeight: Metrics.screenHeight,
      topLogo: { width: Metrics.screenWidth }
    })
  }

  handlePressLogin = () => {
    const { username, password } = this.state
    this.isAttempting = true
    // attempt a login - a saga is listening to pick it up from here.
    this.props.attemptLogin(username, password)
  }

  handleChangeUsername = (text) => {
    this.setState({ username: text })
  }

  handleChangePassword = (text) => {
    this.setState({ password: text })
  }

  render () {
    return (
      <Wallpaper>
        <Logo />
        <Form
          username={this.state.username}
          password={this.state.password}
          onChangeUsername={this.onChangeUsername}
          onChangePassword={this.onChangePassword}
        />
        <SignupSection />
        <ButtonSubmit
          navigation={this.props.navigation}
          onPress={this.login}
          loading={this.props.member.loading}
        />
      </Wallpaper>
    )
  }

  onChangeUsername (value) {
    this.setState({ username: value })
  }

  onChangePassword (value) {
    this.setState({ password: value })
  }
}

const mapStateToProps = (state) => {
  return {
    member: state.member || {},
    fetching: state.login.fetching
  }
}

const mapDispatchToProps = {
  attemptLogin: LoginActions.loginRequest
}

export default connect(mapStateToProps, mapDispatchToProps)(LoginScreen)
