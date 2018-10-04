import { StyleSheet } from 'react-native'
import { Colors, Metrics } from '../../Themes/'

export default StyleSheet.create({
  container: {
    flex: 1,
    marginVertical: Metrics.baseMargin,
    marginHorizontal: Metrics.doubleBaseMargin + 10
  },
  currentDay: {
    marginLeft: 16,
    marginRight: 24
  },
  active: {
    marginLeft: 6,
    marginRight: 34,
    borderRadius: 5,
    shadowOffset: {
      width: 1,
      height: 1
    },
    shadowRadius: 5,
    shadowColor: Colors.redShadow,
    shadowOpacity: 1
  },
  finished: {
    opacity: 0.7
  },
  info: {
    flex: 1,
    flexDirection: 'row',
    justifyContent: 'space-between',
    padding: Metrics.doubleBaseMargin,
    borderTopLeftRadius: Metrics.cardRadius,
    borderTopRightRadius: Metrics.cardRadius,
    borderBottomLeftRadius: Metrics.cardRadius,
    borderBottomRightRadius: Metrics.cardRadius,
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
    letterSpacing: 0,
    paddingLeft: 30
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
    left: -20,
    zIndex: 4,
    width: 40,
    height: 40,
    // borderColor: Colors.avatarBorder,
    // borderWidth: 1,
    // borderRadius: 40 / 2,
    // backgroundColor: Colors.charcoal,
    resizeMode: 'center',
    tintColor: Colors.darkPurple
  },
  rightAvatar: {
    position: 'absolute',
    top: -10,
    right: -20,
    zIndex: 4,
    width: 40,
    height: 40,
    // borderColor: Colors.avatarBorder,
    // borderWidth: 1,
    // borderRadius: 40 / 2,
    // backgroundColor: Colors.charcoal,
    resizeMode: 'center',
    tintColor: Colors.darkPurple
  },
  moreInfo: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingVertical: 13,
    paddingHorizontal: Metrics.doubleBaseMargin,
    borderBottomLeftRadius: Metrics.cardRadius,
    borderBottomRightRadius: Metrics.cardRadius,
    backgroundColor: Colors.silver
  },
  details: {
    flexDirection: 'row'
  },
  detail: {
    paddingRight: Metrics.doubleBaseMargin
  },
  detailLabel: {
    fontFamily: 'Montserrat-Light',
    fontSize: 11,
    color: Colors.lightText,
    letterSpacing: 0
  },
  detailText: {
    fontFamily: 'Montserrat-SemiBold',
    fontSize: 11,
    color: Colors.darkPurple,
    letterSpacing: 0
  }
})
