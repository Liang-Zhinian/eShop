import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import ClientsScreen from '../../Containers/Clients/ClientsScreen'
import CreateStackNavigator from '../CreateStackNavigator'

export default CreateStackNavigator({
  ClientListing: { screen: ClientsScreen }
})
