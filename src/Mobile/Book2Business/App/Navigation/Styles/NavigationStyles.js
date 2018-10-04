import { StyleSheet } from 'react-native'
import { Colors } from '../../Themes/'

export default StyleSheet.create({
  headerGradient: {
    shadowOffset: {
      width: 0,
      height: 0
    },
    shadowRadius: 20,
    shadowColor: 'black',
    shadowOpacity: 0.8,
    elevation: 20,
    backgroundColor: 'black'
  },
  tabBar: {
    height: 54,
    paddingTop: 5,
    paddingBottom: 1,
    paddingHorizontal: 28,
    borderTopWidth: 1,
    borderTopColor: 'rgba(255,255,255,0.3)',
    backgroundColor: Colors.darkPurple
  },
  tabBarLabel: {
    fontFamily: 'Montserrat-Medium',
    fontSize: 9,
    letterSpacing: 0,
    color: Colors.snow
  },
  card: {
    opacity: 1,
    backgroundColor: Colors.darkPurple
  },
  header: {
    backgroundColor: Colors.background
  }
})
