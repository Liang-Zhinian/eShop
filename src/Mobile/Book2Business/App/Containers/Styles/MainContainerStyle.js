import { StyleSheet } from 'react-native'
import { ApplicationStyles, Colors, Metrics } from '../../Themes/'

export default StyleSheet.create({
  ...ApplicationStyles.screen,
  container: {
    flex: 1,
    marginTop: Metrics.doubleBaseMargin,
    marginBottom: Metrics.doubleBaseMargin
    // marginHorizontal: Metrics.doubleBaseMargin
  },
  backButton: {
    // position: 'absolute',
    // top: -59,
    // left: -10,
    flexDirection: 'row',
    alignItems: 'center'
    // padding: 20
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
  header: {
    flexDirection: 'row',
    marginHorizontal: Metrics.doubleBaseMargin
  }
})
