import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import ClientsScreen from '../../modules/booking/Containers/Clients/ClientsScreen'
import CreateStackNavigator from '../CreateStackNavigator'

export default CreateStackNavigator({
  ClientListing: { screen: ClientsScreen }
})
