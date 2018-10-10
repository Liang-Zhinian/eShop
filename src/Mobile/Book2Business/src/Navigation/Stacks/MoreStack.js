import MoreScreen from '../../modules/booking/Containers/More/MoreScreen'
import AppointmentsStack from './AppointmentsStack'
import ClassesStack from './ClassesStack'
import BizInfoStack from './BizInfoStack'
import StaffsStack from './StaffsStack'
import ProfileStack from './ProfileStack'
import LocationScreen from '../../modules/chainreact/Containers/LocationScreen'
import AboutScreen from '../../modules/chainreact/Containers/AboutScreen'
import QuickDevScreen from '../../modules/booking/Containers/Dev/QuickDevScreen'

import CreateStackNavigator from '../CreateStackNavigator'

export default CreateStackNavigator({
  MoreMenu: { screen: MoreScreen },
  Appointments: { screen: AppointmentsStack },
  Classes: { screen: ClassesStack },
  BizInfo: { screen: BizInfoStack },
  Staffs: { screen: StaffsStack },
  Profile: { screen: ProfileStack },
  ChangeLocation: { screen: ProfileStack },
  Location: { screen: LocationScreen },
  About: { screen: AboutScreen },
  QuickDev: { screen: QuickDevScreen }
})
