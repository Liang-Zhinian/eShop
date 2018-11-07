require("core-js")
import { StyleSheet } from 'react-native'
import { ApplicationStyles, Colors, Metrics } from '../../Themes/'
import BackButtonStyles from '../BackButton/BackButtonStyle'

// Enable this if you have app-wide application styles
// import { ApplicationStyles } from '../../Themes/'

export default StyleSheet.create({
  // Merge in the screen styles from application styles
  ...ApplicationStyles.screen,
  // ...ApplicationStyles.linearGradient,
  ...BackButtonStyles,
  container: {
    flex: 1,
    // justifyContent: 'flex-end',
    alignItems: 'center'
  },
  map: {
    width: '100%',
    height: 180
    // zIndex: 2
  },
  mapCloseButton: {
    width: 30,
    height: 30,
    alignSelf: 'flex-end',
    margin: Metrics.smallMargin,
    position: 'absolute',
    left: 0,
    zIndex: 100
  },
  listContent: {
    paddingTop: Metrics.baseMargin,
    paddingBottom: Metrics.baseMargin * 8
  },
  infoText: {
    flex: 1,
    paddingRight: Metrics.doubleBaseMargin
  },
  title: {
    fontFamily: 'Montserrat-SemiBold',
    fontSize: 17,
    color: Colors.darkPurple,
    letterSpacing: 0
  },
  name: {
    fontFamily: 'Montserrat-Light',
    fontSize: 13,
    color: Colors.lightText,
    letterSpacing: 0,
    lineHeight: 18
  },
  leftAvatar: {
    position: 'absolute',
    top: -11,
    left: -38,
    zIndex: 4,
    width: 40,
    height: 40,
    borderColor: Colors.avatarBorder,
    borderWidth: 1,
    borderRadius: 40 / 2,
    backgroundColor: Colors.charcoal,
    resizeMode: 'center'
  },
  rightAvatar: {
    position: 'absolute',
    top: -11,
    right: -38,
    zIndex: 4,
    width: 40,
    height: 40,
    borderColor: Colors.avatarBorder,
    borderWidth: 1,
    borderRadius: 40 / 2,
    backgroundColor: Colors.charcoal,
    resizeMode: 'center'
  },
  header: {
    flexDirection: 'row',
    // marginHorizontal: Metrics.doubleBaseMargin
  }
})
