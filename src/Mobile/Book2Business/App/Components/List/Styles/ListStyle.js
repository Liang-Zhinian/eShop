import { StyleSheet } from 'react-native'
import { ApplicationStyles, Colors, Metrics } from '../../../Themes'

export default StyleSheet.create({
  ...ApplicationStyles.screen,
  // ...ApplicationStyles.linearGradient,
  container: {
    flex: 1,
    backgroundColor: Colors.snow
    // marginVertical: Metrics.baseMargin,
    // marginHorizontal: Metrics.doubleBaseMargin + 10
  },
  listContent: {
    // flex: 1,
    paddingTop: Metrics.baseMargin,
    paddingBottom: Metrics.baseMargin * 8
  },
  footerStyle: {
    padding: 7,
    alignItems: 'center',
    justifyContent: 'center'
    // borderTopWidth: 2,
    // borderTopColor: '#009688'
  },
  TouchableOpacity_style: {
    padding: 7,
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center'
    // backgroundColor: '#F44336',
    // borderRadius: 5,
  },
  TouchableOpacity_Inside_Text: {
    textAlign: 'center',
    color: Colors.lightText,
    fontSize: 15
  }
})
