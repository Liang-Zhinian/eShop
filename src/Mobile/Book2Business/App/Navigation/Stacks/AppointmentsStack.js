import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import AppointmentCategoryListingScreen from '../../Containers/Appointments/ServiceCategories'
import AppointmentListingScreen from '../../Containers/Appointments/ServiceItemListing'
import CreateStackNavigator from '../CreateStackNavigator'

export default CreateStackNavigator({
  AppointmentCategoryListing: { screen: AppointmentCategoryListingScreen },
  AppointmentListing: { screen: AppointmentListingScreen }
}, 'screen')
