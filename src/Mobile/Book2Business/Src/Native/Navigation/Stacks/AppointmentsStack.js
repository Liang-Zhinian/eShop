import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import AppointmentCategoryListingScreen from '../../Containers/Appointments/ServiceCategories'
import AppointmentListingScreen from '../../Containers/Appointments/ServiceItemListing'
import AppointmentTypeScreen from '../../Containers/Appointments/Appointment'
<<<<<<< HEAD:src/Mobile/Book2Business/Src/Native/Navigation/Stacks/AppointmentsStack.js
import StaffScheduleListingScreen from '../../Containers/StaffSchedules/StaffScheduleListing'
import StaffScheduleScreen from '../../Containers/StaffSchedules/StaffSchedule'
=======
import CreateStackNavigator from '../CreateStackNavigator'
>>>>>>> parent of 690f502... add staff schedule screen:src/Mobile/Book2Business/App/Navigation/Stacks/AppointmentsStack.js

export default CreateStackNavigator({
  AppointmentCategoryListing: { screen: AppointmentCategoryListingScreen },
  AppointmentListing: { screen: AppointmentListingScreen },
  AppointmentType: { screen: AppointmentTypeScreen },
<<<<<<< HEAD:src/Mobile/Book2Business/Src/Native/Navigation/Stacks/AppointmentsStack.js
  StaffScheduleListing: {screen: StaffScheduleListingScreen},
  StaffSchedule: {screen: StaffScheduleScreen}
=======
>>>>>>> parent of 690f502... add staff schedule screen:src/Mobile/Book2Business/App/Navigation/Stacks/AppointmentsStack.js
}, 'screen')
