import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import ReportsScreen from '../../Containers/Reports/ReportsScreen'
import CreateStackNavigator from '../CreateStackNavigator'

export default CreateStackNavigator({
  ReportListing: { screen: ReportsScreen }
})
