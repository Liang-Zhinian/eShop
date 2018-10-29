import { StyleSheet } from 'react-native'
import { ApplicationStyles, Metrics, Colors, Fonts } from '../../../Themes/'

export default StyleSheet.create({
  ...ApplicationStyles.screen,
  button: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'stretch',
    // justifyContent: 'center',
    paddingVertical: 16
  },
  label: {
    // flex: 1,
    fontFamily: 'Montserrat-Medium',
    fontSize: 15,
    lineHeight: 23,
    letterSpacing: 0.5,
    color: Colors.darkPurple
  },
  icon: {
    marginHorizontal: 10,
    width: 15,
    height: 15,
    tintColor: 'black',
  },
})
