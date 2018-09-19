import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import styles from '../Styles/NavigationStyles'
import MoreScreen from '../../Containers/More/MoreScreen'

export default StackNavigator({
  MoreMenu: { screen: MoreScreen },
}, {
  headerMode: 'none',
  initialRouteName: 'MoreMenu',
  cardStyle: styles.card
})