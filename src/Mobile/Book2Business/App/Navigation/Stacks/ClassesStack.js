import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import AppointmentCategoryListingScreen from '../../Containers/AppointmentCatalog/AppointmentCategoryListing'
import AppointmentListingScreen from '../../Containers/AppointmentCatalog/AppointmentTypeListing'
import CreateStackNavigator from '../CreateStackNavigator'

export default CreateStackNavigator({
  AppointmentCategoryListing: { screen: AppointmentCategoryListingScreen },
  AppointmentListing: { screen: AppointmentListingScreen }
}, 'screen')
