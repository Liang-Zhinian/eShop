import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import ProfileSettingsScreen from '../../Containers/Profile'
import CreateStackNavigator from '../CreateStackNavigator'

export default CreateStackNavigator({
  ProfileSettings: { screen: ProfileSettingsScreen },
}, 'screen')
