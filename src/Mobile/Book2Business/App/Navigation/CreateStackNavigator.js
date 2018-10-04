import React from 'react'
import { StatusBar, Platform, View } from 'react-native'
import { Header, StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'
import LinearGradient from 'react-native-linear-gradient'

import styles from './Styles/NavigationStyles'
import BackButton from '../Components/BackButton'

export default (routes, headerMode) => {
  return StackNavigator(routes, {
    headerMode: headerMode || 'none',
        // initialRouteName: 'MoreMenu',
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
            colors={['#46114E', '#521655', '#571757']}
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
