import { StyleSheet, Platform } from 'react-native'
import { Colors, Metrics } from '../../Themes/'

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
  body: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-around',
    paddingTop: Metrics.doubleBaseMargin,
    height: 85,
    backgroundColor: Colors.clear
  },
  title: {
    fontSize: Platform.OS === 'ios' ? 17 : 20,
    fontWeight: Platform.OS === 'ios' ? '700' : '500',
    color: 'rgba(255, 255, 255, .9)',
    textAlign: Platform.OS === 'ios' ? 'center' : 'left',
    marginHorizontal: 16,
  }
})
