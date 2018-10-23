import React from 'react'
import {
    View,
    FlatList,
    Text,
    StyleSheet,
    ScrollView,
    Dimensions
} from 'react-native'
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

import { DateFormat, TimeFormat, MinDate } from '../../../Constants/date'
import components from '../../../../native-base-theme/components';
import { ApplicationStyles, Metrics, Colors } from '../../../Themes'

function formatTime(date) {
    return `${format(date, TimeFormat)}`
}

function buildTimeslots(date, minutesDiff = 15) {
    let timeslots = []
    // let someDate = MinDate
    let someDate = new Date(date)
    let nextDate = addDays(new Date(date), 1)

    while (someDate < nextDate) {
        timeslots.push({ time: formatTime(someDate), datetime: someDate })
        someDate = addMinutes(someDate, minutesDiff)
    }
    return timeslots
}


class Grid extends React.Component {
    constructor(props) {
        super(props)

        const timeslots = buildTimeslots(props.initialDate, props.duration)
        const data = this.getEventsByDayFromSchedule(timeslots)

        this.state = {
            data: data[0],
            rowHeight: props.rowHeight,
            timeslotWidth: props.timeslotWidth,
            initialDate: props.initialDate
        }

    }

    // scheduleList = null

    componentDidMount() {
        // this.list.scrollToIndex({ index, animated: false })
        const { data } = this.state
        const index = this.getActiveIndex(data)
        // fixes https://github.com/facebook/react-native/issues/13202
        const wait = new Promise((resolve) => setTimeout(resolve, 500))
        wait.then(() => {
            // this.refs.grid.scrollToIndex({ index, animated: true })
        })
    }

    render() {
        const { data } = this.state
        return (
            <FlatList
                ref='grid'
                style={styles.grid}
                data={data}
                extraData={this.props}
                renderItem={this.renderItem.bind(this)}
                keyExtractor={(item, idx) => item.eventStart.toString()}
                contentContainerStyle={styles.listContent}
                getItemLayout={this.getItemLayout}
                showsVerticalScrollIndicator={false}
                scrollEnabled={false}
            />
        )
    }

    renderItem({ item }) {
        const {rowHeight, timeslotWidth} = this.state
        return (
            <View style={[styles.row, { height: rowHeight }]}>
                <View style={[styles.timeslot, { width: timeslotWidth }]}>
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

        const {rowHeight, timeslotWidth} = this.state

        return { length: rowHeight, offset: rowHeight * index, index }
    }

    getActiveIndex = (data) => {
        // const { currentTime } = this.props
        let initialTime = new Date()
        const firstDay = new Date(this.state.initialDate)
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
            const eventDuration = this.props.duration //Number(e.duration)
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

export default Grid

const styles = StyleSheet.create({
    grid: {
        flex: 1,
    },
    row: {
        flex: 1,
        flexDirection: 'row',
        borderColor: 'gray',
        borderWidth: 0,
        borderBottomWidth: StyleSheet.hairlineWidth,

    },
    timeslot: {
        alignItems: 'center',
        justifyContent: 'center',
        backgroundColor: 'gray',
        borderColor: 'gray',
        borderWidth: 0,
        borderRightWidth: StyleSheet.hairlineWidth,
    },
    timeslot_text: {
        // color: 'white'
    },
    listContent: {
        paddingBottom: Metrics.baseMargin * 8
    },
})