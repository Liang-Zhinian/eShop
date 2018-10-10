import React from 'react'
import {
  ActivityIndicator,
  AsyncStorage,
  StatusBar,
  StyleSheet,
  View,
  Image,
  Text
} from 'react-native'
import { NavigationActions } from 'react-navigation'
import { connect } from 'react-redux'
import Wallpaper from './components/Wallpaper'
import statusMessage from '../../Actions/status'
import book2, { retrieveToken } from '../../Services/Auth'
import { Api } from '../../Services/api'

function isExpired(token) {
  let currentTime = new Date()
  let auth_time = new Date(token.auth_time)
  let expires_date = auth_time.setSeconds(auth_time.getSeconds() + token.expires_in)
  return currentTime > expires_date
}

class AuthLoadingScreen extends React.Component {
  static defaultProps = {
  }

  constructor(props) {
    super(props)
    this._bootstrapAsync()
    this.state = {
    }
  }

  userToken = null

  // Fetch the token from storage then navigate to our appropriate place
  _bootstrapAsync = async () => {

    // check if token was expired
    this.userToken = await retrieveToken()
    if (!this.userToken) {
      this.props.navigation.navigate('Auth')
      return
    }

    let expired = book2.auth().isExpired()
    if (expired === true) {
      book2.auth().refreshToken().then(async (identity) => {
        this.userToken = await retrieveToken()
      })
    }
  }

  _resetRouteStack = (routeName) => {
    this.props.navigation.dispatch(NavigationActions.reset({
      index: 0,
      actions: [NavigationActions.navigate({ routeName: routeName })]
    }))
  }

  printProps(title = 'auth loading ...') {
    const { member } = this.props
    console.log('printProps ' + title, this.userToken, member)
  }

  componentWillReceiveProps(nextProps) {
    const { member } = nextProps
    console.log('member', member)
    if (this.userToken && member.uid && member.currentLocation) this.props.navigation.navigate('App')
    else this.props.navigation.navigate('Auth')
  }

  // Render any loading content that you like here
  render() {
    return (
      <Wallpaper>
        {/* <Image style={{flex: 1, resizeMode: 'contain'}} source={require('./images/background.jpg')} /> */}
        <StatusBar barStyle='default' />
        <View style={styles.container}>
          <Text style={styles.logo}>Book2</Text></View>
      </Wallpaper>
    )
  }
}

const mapStateToProps = (state, props) => ({
  member: state.member,
})

const mapDispatchToProps = (dispatch) => ({
  statusMessage: async (loading) => { await statusMessage(dispatch, 'loading', loading) }
})

export default connect(mapStateToProps, mapDispatchToProps)(AuthLoadingScreen)

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center'
  },
  logo: {
    fontSize: 40
  }
})
