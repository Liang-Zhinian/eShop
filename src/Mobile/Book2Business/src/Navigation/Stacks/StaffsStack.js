import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import StaffListingScreen from '../../modules/booking/Containers/Staffs/StaffsScreen'
// import AppointmentListingScreen from '../../Containers/Appointments/ServiceItemListing'
import CreateStackNavigator from '../CreateStackNavigator'

export default CreateStackNavigator({
  StaffListing: { screen: StaffListingScreen }
  // AppointmentListing: { screen: AppointmentListingScreen }
}, 'screen')
