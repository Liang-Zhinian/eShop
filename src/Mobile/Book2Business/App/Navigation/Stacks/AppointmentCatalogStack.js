import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import CreateStackNavigator from '../CreateStackNavigator'
import AppointmentCategoryListingScreen from '../../Containers/AppointmentCatalog/AppointmentCategoryListing'
import AppointmentTypeListingScreen from '../../Containers/AppointmentCatalog/AppointmentTypeListing'
import AppointmentTypeScreen from '../../Containers/AppointmentCatalog/AppointmentType'
import StaffScheduleListingScreen from '../../Containers/StaffSchedules/StaffScheduleListing'
import StaffScheduleScreen from '../../Containers/StaffSchedules/StaffSchedule'


export default CreateStackNavigator({
  AppointmentCategoryListing: { screen: AppointmentCategoryListingScreen },
  AppointmentTypeListing: { screen: AppointmentTypeListingScreen },
  AppointmentType: { screen: AppointmentTypeScreen },
  StaffScheduleListing: {screen: StaffScheduleListingScreen},
  StaffSchedule: {screen: StaffScheduleScreen}
}, 'screen')
