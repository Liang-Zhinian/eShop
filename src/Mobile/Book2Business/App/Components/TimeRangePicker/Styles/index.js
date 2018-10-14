import { StyleSheet } from 'react-native'
import { ApplicationStyles, Metrics, Colors, Fonts } from '../../../Themes/'

export default StyleSheet.create({
  ...ApplicationStyles.screen,
  headingContainer: {
    alignItems: 'center',
    justifyContent: 'center',
    // marginTop: 40
  },
  mainHeading: {
    fontFamily: 'Montserrat-SemiBold',
    fontSize: 31,
    letterSpacing: 0.2,
    color: Colors.snow
  },
  directionsIcon: {
    alignItems: 'center',
    flex: 1
  },
  directionsLabel: {
    fontFamily: 'Montserrat-Medium',
    fontSize: 11,
    letterSpacing: 0,
    color: Colors.darkPurple
  },
  getRide: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingVertical: 16,
  },
  getRideLabel: {
    fontFamily: 'Montserrat-Medium',
    fontSize: 15,
    lineHeight: 23,
    letterSpacing: 0.5,
    color: Colors.darkPurple
  },
  getRideIcon: {
    marginHorizontal: 10,
    width: 15,
    height: 15,
    tintColor: 'black',
  },
  rideButton: {
    margin: 1.2 * Metrics.smallMargin
  },
  flip: {
    transform: [{
      rotate: '180 deg'
    }]
  },
  slackButton: {
    width: 300,
    margin: 10
  },

  singleDateOptions: {
    alignItems: 'center',
    justifyContent: 'center',
    height: 0,
    overflow: 'hidden',
    backgroundColor: '#EDEDED',
  },
  singleDatePicker: {
    alignItems: 'center',
    paddingVertical: 10,
    paddingHorizontal: 5
  },
  dateRangeOptions: {
    alignItems: 'center',
    justifyContent: 'center',
    height: 0,
    overflow: 'hidden',
    backgroundColor: '#EDEDED',
  },
  dateRangePicker: {
    alignItems: 'center',
    paddingVertical: 10,
    paddingHorizontal: 5
  }
})
