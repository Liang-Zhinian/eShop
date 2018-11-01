import { StyleSheet } from 'react-native'
import { ApplicationStyles, Metrics, Colors, Fonts } from '../../../Themes/'

export default StyleSheet.create({
  ...ApplicationStyles.screen,
  linearGradient: ApplicationStyles.linearGradient,
  button: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    // justifyContent: 'space-around',
    paddingVertical: 16,

  },
  label: {
    fontFamily: 'Montserrat-Medium',
    fontSize: 15,
    lineHeight: 23,
    letterSpacing: 0.5,
    color: Colors.darkPurple,
    marginRight: 10,
  },
  icon: {
    // marginHorizontal: 10,
    // tintColor: 'black',
  },
})
