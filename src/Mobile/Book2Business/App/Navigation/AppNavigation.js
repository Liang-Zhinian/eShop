import React, { Component } from 'react'
import { StackNavigator, SwitchNavigator } from 'react-navigation'

import LoadingScreen from '../Containers/AuthLoadingScreen'
import LoggedInStackNavigator from './LoggedInStackNavigator'
import NotLoggedInStackNavigator from './NotLoggedInStackNavigator'

import styles from './Styles/NavigationStyles'

export const AppNavigation = SwitchNavigator({
  AuthLoading: { screen: LoadingScreen },
  App: { screen: LoggedInStackNavigator },
  Auth: { screen: NotLoggedInStackNavigator }
}, {
  headerMode: 'none',
  navigationOptions: {
    headerStyle: styles.header
  }
})


