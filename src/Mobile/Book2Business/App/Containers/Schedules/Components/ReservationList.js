import React from 'react'
import { View, FlatList, Text, StyleSheet } from 'react-native'
import {
    format,
    addMinutes,
    addDays,
    differenceInMinutes,
    compareAsc,
    isSameDay,
    isWithinRange,
    subMilliseconds
} from 'date-fns'
import {
    merge,
    groupWith,
    contains,
    assoc,
    map,
    sum,
    findIndex
} from 'ramda'

import { TimeFormat, MinDate } from '../../../Constants/date'
import components from '../../../../native-base-theme/components';
import { ApplicationStyles, Metrics, Colors } from '../../../Themes'

const minutesArray = ['00', '15', '30', '45'];

function formatTime(date) {
    return `${format(date, TimeFormat)}`
}

function buildTimeslots(minutesDiff = 15) {
    let timeslots = []
    // let someDate = MinDate
    let someDate = new Date('7/10/2017')
    let nextDate = addDays(new Date('7/10/2017'), 1)

    while (someDate < nextDate) {
        timeslots.push({ time: formatTime(someDate), datetime: someDate })
        someDate = addMinutes(someDate, minutesDiff)
    }
    return timeslots
}

function calculateTop(time) {
    let copiedDate = new Date(time)
    copiedDate.setHours(0)
    copiedDate.setMinutes(0)
    copiedDate.setSeconds(0)

    let beginOfTheDay = copiedDate
    let mins = differenceInMinutes(time, beginOfTheDay)
    let top = RowHeight * (mins / MinutesDiff)

    return top
}


const schedules = require('../../../Fixtures/schedule').schedule

const RowHeight = 30
const TimeslotWidth = 80

const MinutesDiff = 15

const timeslots = buildTimeslots(MinutesDiff)

class ReservationList extends React.Component {
    constructor(props) {
        super(props)

        const data = this.getEventsByDayFromSchedule(timeslots)

        this.state = { data: data[0] }
    }

    // scheduleList = null

    componentDidMount() {
        // this.list.scrollToIndex({ index, animated: false })
        const { data } = this.state
        const index = this.getActiveIndex(data)
        // console.log(index)
        // fixes https://github.com/facebook/react-native/issues/13202
        const wait = new Promise((resolve) => setTimeout(resolve, 500))
        wait.then(() => {
            console.log(this.refs.scheduleList)
            this.refs.scheduleList.scrollToIndex({ index:20, animated: true })
        })
    }

    render() {
        const { data } = this.state
        // console.log(data)
        return (
            <View style={styles.container}>
                <FlatList
                    style={{ flex: 1 }}
                    ref='scheduleList'
                    data={data}
                    extraData={this.props}
                    renderItem={this.renderItem}
                    keyExtractor={(item, idx) => item.eventStart.toString()}
                    contentContainerStyle={styles.listContent}
                    getItemLayout={this.getItemLayout}
                    showsVerticalScrollIndicator={false}
                />
                {/* {this.renderReservations()} */}
            </View>
        )
    }

    renderItem({ item }) {
        return (
            <View style={styles.row}>
                <View style={styles.timeslot}>
                    <Text style={styles.timeslot_text}>{item.time}</Text>
                </View>
                <View></View>
            </View>
        )
    }

    getItemLayout = (data, index) => {
        // const item = data[index]
        // const itemLength = (item, index) => {
        //     return RowHeight
        // }
        // const length = itemLength(item)
        // const offset = sum(data.slice(0, index).map(itemLength))
        // return { length, offset, index }

        return { length: 30, offset: 30 * index, index }
    }

    renderReservations() {
        return schedules.map((schedule, index) => {
            const time = new Date(schedule.time)
            const duration = schedule.duration
            return (
                <View key={schedule.speaker + '@' + schedule.time} style={[
                    styles.reservationContainer,
                    {
                        height: RowHeight * (duration / MinutesDiff),
                        top: calculateTop(time),
                    }
                ]}>
                    <Text>
                        {schedule.speaker}
                    </Text>
                    <Text>
                        {schedule.title}
                    </Text>
                </View>
            )
        })
    }

    getActiveIndex = (data) => {
        // const { currentTime } = this.props
        let initialTime = new Date()
        const firstDay = new Date('7/10/2017')
        initialTime.setFullYear(firstDay.getFullYear())
        initialTime.setMonth(firstDay.getMonth())
        initialTime.setDate(firstDay.getDate())
        const currentTime = new Date(initialTime)

        const foundIndex = findIndex((i) => isWithinRange(currentTime, i.eventStart, i.eventEnd))(data)

        // handle pre-event and overscroll
        if (foundIndex < 0) {
            return 0
        } else if (foundIndex > data.length - 3) {
            return data.length - 3
        } else {
            return foundIndex
        }
    }

    getEventsByDayFromSchedule = (schedule) => {
        const mergeTimes = (e) => {
            const eventDuration = MinutesDiff //Number(e.duration)
            const eventStart = new Date(e.datetime)
            const eventFinal = addMinutes(eventStart, eventDuration)
            // ends 1 millisecond before event
            const eventEnd = subMilliseconds(eventFinal, 1)

            return merge(e, { eventStart, eventEnd, eventDuration, eventFinal })
        }
        const sorted = [...schedule].map(mergeTimes).sort((a, b) => {
            return compareAsc(a.eventStart, b.eventStart)
        })
        return groupWith((a, b) => isSameDay(a.eventStart, b.eventStart), sorted)
    }
}

export default ReservationList

const styles = StyleSheet.create({
    container: {
        flex: 1,
    },
    row: {
        flex: 1,
        flexDirection: 'row',
        borderColor: 'gray',
        borderWidth: 0,
        borderBottomWidth: StyleSheet.hairlineWidth,
        height: RowHeight,

    },
    timeslot: {
        alignItems: 'center',
        justifyContent: 'center',
        width: TimeslotWidth,
        backgroundColor: 'gray',
        borderColor: 'gray',
        borderWidth: 0,
        borderRightWidth: StyleSheet.hairlineWidth,
    },
    timeslot_text: {
        // color: 'white'
    },
    reservationContainer: {
        backgroundColor: 'yellow',
        width: 200,
        position: 'absolute',
        left: TimeslotWidth + 10,
        borderColor: 'blue',
        borderRadius: 10,
        borderWidth: StyleSheet.hairlineWidth,
    },
    listContent: {
        paddingTop: Metrics.baseMargin,
        paddingBottom: Metrics.baseMargin * 8
    },
})