import { StyleSheet } from 'react-native'
import { ApplicationStyles, Metrics, Colors } from '../../Themes'

export default StyleSheet.create({
  backButton: {
    flexDirection: 'row',
    alignItems: 'center',
    padding: 20
  },
  backButtonIcon: {
    marginRight: 5
  },
  backButtonText: {
    fontFamily: 'Montserrat-Light',
    fontSize: 17,
    letterSpacing: 0,
    backgroundColor: Colors.transparent,
    color: 'rgba(255,255,255,0.80)'
  }
})
