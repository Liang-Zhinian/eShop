import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import CreateStackNavigator from '../CreateStackNavigator'
import AppointmentCategoryListingScreen from '../../Containers/Appointments/ServiceCategories'
import AppointmentListingScreen from '../../Containers/Appointments/ServiceItemListing'
import AppointmentTypeScreen from '../../Containers/Appointments/Appointment'
import StaffScheduleListingScreen from '../../Containers/StaffSchedules/StaffScheduleListing'
import StaffScheduleScreen from '../../Containers/StaffSchedules/StaffSchedule'

export default CreateStackNavigator({
  AppointmentCategoryListing: { screen: AppointmentCategoryListingScreen },
  AppointmentListing: { screen: AppointmentListingScreen },
  AppointmentType: { screen: AppointmentTypeScreen },
  StaffScheduleListing: {screen: StaffScheduleListingScreen},
  StaffSchedule: {screen: StaffScheduleScreen}
}, 'screen')
