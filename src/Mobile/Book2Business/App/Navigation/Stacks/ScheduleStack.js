import CreateStackNavigator from '../CreateStackNavigator'

import ScheduleScreen from '../../Containers/Schedules/ScheduleScreen'
import TalkDetailScreen from '../../Containers/TalkDetailScreen'
import BreakDetailScreen from '../../Containers/BreakDetailScreen'

export default CreateStackNavigator({
    Home: { screen: ScheduleScreen },
    TalkDetail: { screen: TalkDetailScreen },
    BreakDetail: { screen: BreakDetailScreen }
})
