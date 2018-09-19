import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import styles from '../Styles/NavigationStyles'
import ReportsScreen from '../../Containers/Reports/ReportsScreen'

export default StackNavigator({
  ReportListing: { screen: ReportsScreen },
}, {
  headerMode: 'none',
  initialRouteName: 'ReportListing',
  cardStyle: styles.card
})