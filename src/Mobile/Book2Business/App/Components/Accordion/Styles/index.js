import { StyleSheet } from 'react-native'
import { ApplicationStyles, Metrics, Colors, Fonts } from '../../../Themes/'

export default StyleSheet.create({
  ...ApplicationStyles.screen,
  title: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingVertical: 5
  },
  content: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    overflow: 'hidden',
    backgroundColor: '#EDEDED',
  },
  label: {
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
  flip: {
    transform: [{
      rotate: '180 deg'
    }]
  },
  TouchableOpacityStyle:
  {
    flex: 1,
    padding: 10,
    backgroundColor: '#00BCD4',
    borderWidth: StyleSheet.hairlineWidth,
    borderLeftWidth: 0,
    borderRightWidth: 0,
    borderBottomWidth: 0,
    borderColor: Colors.snow
  },
  TouchableOpacityTitleText:
  {
    textAlign: 'center',
    color: '#fff',
    fontSize: 20
  },

})
