import React from 'react'
import { StatusBar, Platform, View } from 'react-native'
import { Header, StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'
import LinearGradient from 'react-native-linear-gradient'

import styles from './Styles/NavigationStyles'
import BackButton from '../Components/BackButton'
import { ComponentStyles } from '../Themes/'

export default (routes, headerMode) => {
  return StackNavigator(routes, {
    headerMode: headerMode || 'none',
    cardStyle: styles.card,
    navigationOptions: ({navigation}) => ({
      headerStyle: {
        backgroundColor: 'transparent',
        elevation: 0,
        paddingTop: Platform.OS === 'ios' ? 0 : StatusBar.currentHeight
      },
      headerTintColor: '#fff',
      headerTitleStyle: {
        fontWeight: 'bold'
      },
      header: props => {
        return (
          <LinearGradient
            start={{ x: 0, y: 0 }}
            end={{ x: 1, y: 1 }}
            locations={[0.0, 0.38, 1.0]}
            colors={ComponentStyles.navBar.colors}
            style={styles.headerGradient}
                    >
            <View>
              <Header {...props} style={{ backgroundColor: 'transparent' }} />
            </View>
          </LinearGradient>
        )
      },
      headerLeft: (
        <BackButton navigation={navigation} />
            )
    })

  })
}
