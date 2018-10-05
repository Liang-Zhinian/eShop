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
import { getLocation } from '../../Actions/locations'
import statusMessage from '../../Actions/status'
import { getItem } from '../../Services/StorageService'

function isExpired(token) {
  let currentTime = new Date();
  let auth_time = new Date(token.auth_time);
  let expires_date = auth_time.setSeconds(auth_time.getSeconds() + token.expires_in);
  return currentTime > expires_date;
}

class AuthLoadingScreen extends React.Component {
  static defaultProps = {
    // isLoading: true
  }

  constructor(props) {
    super(props)
    this._bootstrapAsync()
    this.state = {
      accessChecked: false,
      locationChecked: false,
      // isLoading: true
    }
  }

  userToken = null

  // Fetch the token from storage then navigate to our appropriate place
  _bootstrapAsync = async () => {
    // await this.props.statusMessage(true)

    // check if token was expired
    this.userToken = await getItem('identity')
    if (!this.userToken) {
      console.log('userToken', this.userToken)
      this.props.navigation.navigate('Auth')
      return
    }

    let expired = isExpired(this.userToken);
    if (expired) {
      // refresh token
      console.log('token expired')
      setTimeout(() => {
        // this.props.isLoading = false
        console.log('token refreshed')
        this.props.navigation.navigate('App')

      }, 3000)
    }


    setTimeout(() => {
      // this.setState({ isLoading: false })
      console.log('token refreshed')
      if (this.props.locations.error) {
        // re-fetch locations
        this.props.navigation.navigate('Auth')
        return
      }
      this.props.navigation.navigate('App')
    }, 4000)
  }

  _resetRouteStack = (routeName) => {
    this.props.navigation.dispatch(NavigationActions.reset({
      index: 0,
      actions: [NavigationActions.navigate({ routeName: routeName })]
    }))
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
  // isLoading: state.status.loading,
  // tokenCheck: state.member.tokenCheck || false,
  member: state.member,
  locations: state.locations
})

const mapDispatchToProps = (dispatch) => ({
  statusMessage: async (loading) => { await statusMessage(dispatch, 'loading', loading) }
})

export default connect(mapStateToProps, mapDispatchToProps)(AuthLoadingScreen)

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  logo: {
    fontSize: 40
  }
})
