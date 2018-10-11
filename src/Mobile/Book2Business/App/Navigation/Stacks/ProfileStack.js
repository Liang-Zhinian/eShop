import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import ProfileSettingsScreen from '../../Containers/Member/Profile'
import CreateStackNavigator from '../CreateStackNavigator'

export default CreateStackNavigator({
  ProfileSettings: { screen: ProfileSettingsScreen },
}, 'screen')
