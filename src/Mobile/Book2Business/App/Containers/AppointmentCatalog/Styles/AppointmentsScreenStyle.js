import { StyleSheet } from 'react-native'
import { ApplicationStyles, Metrics, Colors } from '../../../Themes'

export default StyleSheet.create({
  ...ApplicationStyles.screen,
  container: {
    flex: 1,
    backgroundColor: Colors.snow
    // marginTop: 101,
    // marginBottom: Metrics.doubleBaseMargin,
    // marginHorizontal: Metrics.doubleBaseMargin
  },
  backButton: {
    // position: 'absolute',
    // top: -59,
    // left: -10,
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
  },
  row: {
    flex: 1,
    backgroundColor: Colors.snow,
    marginVertical: Metrics.smallMargin
  },
  boldLabel: {
    fontWeight: 'bold',
    color: Colors.text
  },
  label: {
    color: Colors.text
  },
  listContent: {
    paddingTop: Metrics.baseMargin,
    paddingBottom: Metrics.baseMargin * 8
  },
  timeline: {
    width: 2,
    backgroundColor: '#6E3C7B',
    position: 'absolute',
    top: 85,
    bottom: 0,
    right: 11
  },
  input: {
    marginTop: 10,
    fontSize: 20,
    lineHeight: 26,
    padding: 3,
    borderBottomWidth: 1
  },
  text:{ fontSize: 20 }
})
