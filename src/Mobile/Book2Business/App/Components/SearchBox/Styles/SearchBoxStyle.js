import { StyleSheet } from 'react-native'
import { ApplicationStyles, Colors, Metrics } from '../../../Themes/'

export default StyleSheet.create({
  // Merge in the screen styles from application styles
  ...ApplicationStyles.screen,
  container: {
    flex: 1,
    alignItems: 'center'
  },
})
