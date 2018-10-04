import { StyleSheet } from 'react-native'
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
  backButton: {
    position: 'absolute',
    // top: -59,
    // left: -10,
    flexDirection: 'row',
    alignItems: 'center',
    padding: 20,
    zIndex: 4
  },
  backButtonIcon: {
    marginRight: 5
  },
  backButtonText: {
    fontFamily: 'Montserrat-Light',
    fontSize: 17,
    letterSpacing: 0,
    backgroundColor: Colors.transparent,
    color: 'rgba(255,255,255,0.80)'
  },
  dayToggle: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-around',
    paddingTop: Metrics.doubleBaseMargin,
    height: 85,
    backgroundColor: Colors.clear
  },
  inactiveDay: {
    backgroundColor: Colors.clear,
    fontFamily: 'Montserrat-Light',
    fontSize: 20,
    color: 'rgba(255,255,255,0.80)',
    letterSpacing: 0
  },
  activeDay: {
    backgroundColor: Colors.clear,
    fontFamily: 'Montserrat-SemiBold',
    fontSize: 20,
    color: Colors.snow,
    letterSpacing: 0
  },
  timeline: {
    width: 2,
    backgroundColor: '#6E3C7B',
    position: 'absolute',
    top: 85,
    bottom: 0,
    right: 11
  }

})
