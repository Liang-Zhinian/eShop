import MoreScreen from '../../Containers/More/MoreScreen'
import AppointmentsStack from './AppointmentsStack'
import ClassesStack from './ClassesStack'
import BizInfoStack from './BizInfoStack'
import ProfileStack from './ProfileStack'
import LocationScreen from '../../Containers/LocationScreen'
import AboutScreen from '../../Containers/AboutScreen'
import QuickDevScreen from '../../Containers/Dev/QuickDevScreen'

import CreateStackNavigator from '../CreateStackNavigator'

export default CreateStackNavigator({
  MoreMenu: { screen: MoreScreen },
  Appointments: { screen: AppointmentsStack },
  Classes: { screen: ClassesStack },
  BizInfo: { screen: BizInfoStack },
  Profile: { screen: ProfileStack },
  ChangeLocation: { screen: ProfileStack },
  Location: { screen: LocationScreen },
  About: { screen: AboutScreen },
  QuickDev: { screen: QuickDevScreen }
})
