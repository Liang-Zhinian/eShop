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
import Wallpaper from './components/Wallpaper'

export default class AuthLoadingScreen extends React.Component {
  constructor(props) {
    super(props)
    this._bootstrapAsync()
  }

  // Fetch the token from storage then navigate to our appropriate place
  _bootstrapAsync = async () => {
    const userToken = await AsyncStorage.getItem('userToken')

    // This will switch to the App screen or Auth screen and this loading
    // screen will be unmounted and thrown away.
    // this.props.navigation.navigate(userToken ? 'App' : 'Auth')
    const that = this
    setTimeout(() => {
      // that._resetRouteStack(userToken ? 'App' : 'Auth')
      that.props.navigation.navigate(userToken ? 'App' : 'Auth')
    }, 5000)

  };

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

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent:'center',
  },
  logo: {
    fontSize: 40
  }
})
