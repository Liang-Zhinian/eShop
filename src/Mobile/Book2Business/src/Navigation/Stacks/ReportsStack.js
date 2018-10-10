import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import ReportsScreen from '../../modules/booking/Containers/Reports/ReportsScreen'
import CreateStackNavigator from '../CreateStackNavigator'

export default CreateStackNavigator({
  ReportListing: { screen: ReportsScreen }
})
