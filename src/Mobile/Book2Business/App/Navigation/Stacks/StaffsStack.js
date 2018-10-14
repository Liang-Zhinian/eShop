import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import CreateStackNavigator from '../CreateStackNavigator'
import StaffListingScreen from '../../Containers/Staffs/StaffsScreen'
import AddStaffScreen from '../../Containers/Staffs/AddStaffScreen'

export default CreateStackNavigator({
  StaffListing: { screen: StaffListingScreen },
  AddStaff: { screen: AddStaffScreen }
}, 'screen')
