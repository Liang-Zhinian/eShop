import React from 'react'
import {
    View,
    Text,
    StyleSheet,
    ScrollView
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

import Grid from './Grid'
import { DateFormat, TimeFormat, MinDate } from '../../../Constants/date'
import { ApplicationStyles, Metrics, Colors } from '../../../Themes'
import Reservation from './Reservation'

const reservationDate = '7/10/2017'

function formatTime(date) {
    return `${format(date, TimeFormat)}`
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

function calculateHeight(duration) {
    let height = RowHeight * (duration / MinutesDiff)

    return height
}

const schedules = require('../../../Fixtures/schedule').schedule

const RowHeight = 30
const TimeslotWidth = 80

const MinutesDiff = 15

class ReservationList extends React.Component {
    constructor(props) {
        super(props)

        this.state = {
            columnWidth: '100%',
            rootWidth: '100%',
            containerWidth: '100%'
        }
    }

    render() {
        const { columnWidth, rootWidth } = this.state
        return (
            <ScrollView
                // ref='scheduleList'
                style={styles.container}
                onLayout={(e) => {
                    const width = e.nativeEvent.layout.width
                    this.setState({
                        rootWidth: width,
                        columnWidth: (width - TimeslotWidth) / 2
                    })
                }}>
                <Grid
                    rowHeight={RowHeight}
                    timeslotWidth={TimeslotWidth}
                    duration={MinutesDiff}
                    initialDate={reservationDate}
                />
                <View
                    style={[styles.contentRoot, {
                        width: (rootWidth - TimeslotWidth)
                    }]}
                >
                    <View style={[styles.contentColumn, {
                        width: columnWidth
                    }]}>
                        {this.renderReservations()}
                    </View>
                    <View style={[styles.contentColumn, {
                        width: columnWidth
                    }]}>
                        {this.renderReservations()}
                    </View>
                </View>
            </ScrollView>
        )
    }

    renderReservations() {
        const { columnWidth } = this.state
        return schedules.map((schedule, index) => {
            const time = new Date(schedule.time)

            if (format(time, DateFormat) != format(new Date(reservationDate), DateFormat)) return null

            const duration = schedule.duration
            return (
                <Reservation
                    style={{
                        height: calculateHeight(duration),
                        width: columnWidth - 10,
                        top: calculateTop(time),
                    }}
                    when={formatTime(time)}
                    who={schedule.speaker}
                    what={'Men\'s cut'}
                    where={'Green Room'}
                    onPress={() => null}
                />
            )
        })
    }
}

export default ReservationList

const styles = StyleSheet.create({
    container: {
        flex: 1,
        // position: 'relative',
    },
    contentRoot: {
        flex: 1,
        flexDirection: 'row',
        height: '100%',
        marginLeft: TimeslotWidth,
        position: 'absolute',
        borderRightWidth: StyleSheet.hairlineWidth,
        borderColor: 'gray',
    },
    contentColumn: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        position: 'relative',
        borderRightWidth: StyleSheet.hairlineWidth,
        borderColor: 'gray',
        paddingHorizontal: 3,
    },
})