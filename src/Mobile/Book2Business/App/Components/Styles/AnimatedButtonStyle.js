import { StyleSheet } from 'react-native'
import { ApplicationStyles, Colors, Metrics } from '../../Themes'

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
    borderBottomLeftRadius: Metrics.cardRadius,
    borderBottomRightRadius: Metrics.cardRadius,
    backgroundColor: Colors.snow
  }
})
