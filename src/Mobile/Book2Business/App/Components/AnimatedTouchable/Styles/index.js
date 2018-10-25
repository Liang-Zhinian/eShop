import { StyleSheet } from 'react-native'
import { ApplicationStyles, Colors, Metrics } from '../../../Themes'

export default StyleSheet.create({
  ...ApplicationStyles.screen,
  container: {
    flex: 1,
    marginVertical: 0,
    marginHorizontal: 0
  },
  info: {
    flex: 1,
    flexDirection:'column',
    justifyContent:'center',
    // flexDirection: 'row',
    // justifyContent: 'space-between',
    // padding: Metrics.doubleBaseMargin,
    // borderRadius: Metrics.cardRadius,
    // borderTopLeftRadius: Metrics.cardRadius,
    // borderTopRightRadius: Metrics.cardRadius,
    // borderBottomLeftRadius: Metrics.cardRadius,
    // borderBottomRightRadius: Metrics.cardRadius,
    // backgroundColor: Colors.snow
  }
})
