import CreateStackNavigator from '../CreateStackNavigator'

import ScheduleScreen from '../../Containers/Schedules/ScheduleScreen'
import AgendaScreen from '../../Containers/Schedules/AgendaScreen'
import TalkDetailScreen from '../../Containers/TalkDetailScreen'
import BreakDetailScreen from '../../Containers/BreakDetailScreen'

export default CreateStackNavigator({
    Home: { screen: AgendaScreen },
    TalkDetail: { screen: TalkDetailScreen },
    BreakDetail: { screen: BreakDetailScreen }
})
