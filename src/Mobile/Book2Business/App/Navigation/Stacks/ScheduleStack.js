import CreateStackNavigator from '../CreateStackNavigator'

import ScheduleScreen from '../../Containers/Schedules/ScheduleScreen'
import TalkDetailScreen from '../../Containers/TalkDetailScreen'
import BreakDetailScreen from '../../Containers/BreakDetailScreen'
import AppointmentScheduleScreen from '../../Containers/Schedules/AppointmentScheduleScreen'

export default CreateStackNavigator({
    Home: { screen: ScheduleScreen },
    TalkDetail: { screen: TalkDetailScreen },
    BreakDetail: { screen: BreakDetailScreen },
    AppointmentSchedule: { screen: AppointmentScheduleScreen }
})
