import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import styles from '../Styles/NavigationStyles'
import ClientsScreen from '../../Containers/Clients/ClientsScreen'

export default StackNavigator({
    ClientListing: { screen: ClientsScreen },
  }, {
    headerMode: 'none',
    initialRouteName: 'ClientListing',
    cardStyle: styles.card
  })