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

const minutesArray = ['00', '15', '30', '45'];

function formatTime(date) {
    return `${format(date, TimeFormat)}`
}

function buildTimeslots(minutesDiff = 15) {
    let timeslots = []
    let someDate = MinDate
    let nextDate = addDays(MinDate, 1)

    while (someDate < nextDate) {
        timeslots.push({ time: formatTime(someDate) })
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
    console.log('mins ==> top', mins, top)
    return top
}


const schedules = require('../../../Fixtures/schedule').schedule

const RowHeight = 30
const TimeslotWidth = 80

const MinutesDiff = 15

const timeslots = buildTimeslots(MinutesDiff)

class ReservationList extends React.Component {

    list = null

    componentDidMount() {
        // this.list.scrollToIndex({ index, animated: false })
        // const { data } = this.state
        const index = this.getActiveIndex(schedules)
        // fixes https://github.com/facebook/react-native/issues/13202
        const wait = new Promise((resolve) => setTimeout(resolve, 200))
        wait.then(() => {
            this.refs.scheduleList.scrollToIndex({ index, animated: false })
        })
    }

    render() {
        return (
            <View>
                <FlatList
                    ref={ref => this.list = ref}
                    data={timeslots}
                    renderItem={this.renderItem}
                    keyExtractor={(item, index) => item.time}
                />
                {this.renderReservations()}
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

    renderReservations() {
        return schedules.map((schedule, index) => {
            const time = new Date(schedule.time)
            const duration = schedule.duration
            return (
                <View style={[
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
        const currentTime = new Date('7/10/2017')
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
}

export default ReservationList

const styles = StyleSheet.create({
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
    }
})