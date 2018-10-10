import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import CreateStackNavigator from '../CreateStackNavigator'
import AppointmentCategoryListingScreen from '../../modules/booking/Containers/Appointments/ServiceCategories'
import AppointmentListingScreen from '../../modules/booking/Containers/Appointments/ServiceItemListing'
import AppointmentTypeScreen from '../../modules/booking/Containers/Appointments/Appointment'
import StaffScheduleListingScreen from '../../modules/booking/Containers/StaffSchedules/StaffScheduleListing'

export default CreateStackNavigator({
  AppointmentCategoryListing: { screen: AppointmentCategoryListingScreen },
  AppointmentListing: { screen: AppointmentListingScreen },
  AppointmentType: { screen: AppointmentTypeScreen },
  StaffScheduleListing: {screen: StaffScheduleListingScreen}
}, 'screen')
