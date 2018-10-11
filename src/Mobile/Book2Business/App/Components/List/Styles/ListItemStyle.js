import { StyleSheet } from 'react-native'
import { ApplicationStyles, Colors, Metrics } from '../../../Themes'

export default StyleSheet.create({
  ...ApplicationStyles.screen,
  container: {
    flex: 1,
    marginVertical: Metrics.baseMargin,
    marginHorizontal: Metrics.doubleBaseMargin
  },
  info: {
    flex: 1,
    flexDirection: 'row',
    justifyContent: 'space-between',
    padding: Metrics.doubleBaseMargin,
    borderTopLeftRadius: Metrics.cardRadius,
    borderTopRightRadius: Metrics.cardRadius,
    // borderBottomLeftRadius: Metrics.cardRadius,
    // borderBottomRightRadius: Metrics.cardRadius,
    backgroundColor: Colors.snow
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
    top: -10,
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
    // top: -10,
    right: -38,
    zIndex: 4,
    width: 40,
    height: 40,
    borderColor: Colors.avatarBorder,
    borderWidth: 1,
    borderRadius: 40 / 2,
    backgroundColor: Colors.charcoal,
    resizeMode: 'center'
  }
  // listContent: {
  //   paddingTop: Metrics.baseMargin,
  //   paddingBottom: Metrics.baseMargin * 8
  // },
})
