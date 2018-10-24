import React from 'react'
import {
    View,
    Text,
    StyleSheet,
    ScrollView,
    ActivityIndicator
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

import dateutils from './dateutils'
import styleConstructor from './Styles/styleConstructor';
import { DateFormat, TimeFormat, MinDate } from '../../Constants/date'
import Reservation from './Reservation'
import {
    formatTime,
    buildTimeslots,
    calculateTop,
    calculateHeight,
    getEventsByDayFromSchedule
} from './utils'

const reservationDate = '7/10/2017'

const TimeslotWidth = 80

class ReservationList extends React.Component {
    constructor(props) {
        super(props)

        this.styles = styleConstructor(props.theme);
        this.heights = [];
        this.selectedDay = props.selectedDay;
        this.scrollOver = true;

        let initialDate = props.selectedDay ? new Date(props.selectedDay) : new Date()
        initialDate.setHours(0)
        initialDate.setMinutes(0)
        initialDate.setSeconds(0)
        const duration = props.duration || 15
        const rowHeight = props.rowHeight || 30

        const timeslots = buildTimeslots(initialDate, duration)
        const gridData = this.getEventsByDayFromSchedule(timeslots, duration)

        this.state = {
            gridData: gridData[0],
            gridRowHeight: rowHeight,
            initialDate: initialDate,

            columnWidth: '100%',
            rootWidth: '100%',
            containerWidth: '100%',
            // items: props.items,
            reservations: []
        };

        console.log('reservations', props.reservations)
    }

    componentWillMount() {
        this.updateDataSource(this.getReservations(this.props).reservations);
    }

    updateDataSource(reservations) {
        this.setState({
            reservations
        });
    }

    updateReservations(props) {
        const reservations = this.getReservations(props);
        if (this.list && !dateutils.sameDate(props.selectedDay, this.selectedDay)) {
            let scrollPosition = 0;
            for (let i = 0; i < reservations.scrollPosition; i++) {
                scrollPosition += this.heights[i] || 0;
            }
            this.scrollOver = false;
            this.list.scrollToOffset({ offset: scrollPosition, animated: true });
        }
        this.selectedDay = props.selectedDay;
        this.updateDataSource(reservations.reservations);
    }

    componentWillReceiveProps(props) {
        if (!dateutils.sameDate(props.topDay, this.props.topDay)) {
            this.setState({
                reservations: []
            }, () => {
                this.updateReservations(props);
            });
        } else {
            this.updateReservations(props);
        }
    }

    onRowLayoutChange(ind, event) {
        this.heights[ind] = event.nativeEvent.layout.height;
    }

    renderRow({ item, index }) {
        return (
            <View onLayout={this.onRowLayoutChange.bind(this, index)}>
                <Reservation
                    item={item}
                    renderItem={this.props.renderItem}
                    renderDay={this.props.renderDay}
                    renderEmptyDate={this.props.renderEmptyDate}
                    theme={this.props.theme}
                    rowHasChanged={this.props.rowHasChanged}
                />
            </View>
        );
    }

    getReservationsForDay(iterator, props) {
        const day = iterator.clone();
        const res = props.reservations[day.toString('yyyy-MM-dd')];
        if (res && res.length) {
            return res.map((reservation, i) => {
                return {
                    reservation,
                    date: i ? false : day,
                    day
                };
            });
        } else if (res) {
            return [{
                date: iterator.clone(),
                day
            }];
        } else {
            return false;
        }
    }

    onListTouch() {
        this.scrollOver = true;
    }

    getReservations(props) {
        if (!props.reservations || !props.selectedDay) {
            return { reservations: [], scrollPosition: 0 };
        }
        let reservations = [];
        if (this.state.reservations && this.state.reservations.length) {
            const iterator = this.state.reservations[0].day.clone();
            while (iterator.getTime() < props.selectedDay.getTime()) {
                const res = this.getReservationsForDay(iterator, props);
                if (!res) {
                    reservations = [];
                    break;
                } else {
                    reservations = reservations.concat(res);
                }
                iterator.addDays(1);
            }
        }
        const scrollPosition = reservations.length;
        const iterator = props.selectedDay.clone();
        for (let i = 0; i < 31; i++) {
            const res = this.getReservationsForDay(iterator, props);
            if (res) {
                reservations = reservations.concat(res);
            }
            iterator.addDays(1);
        }

        return { reservations, scrollPosition };
    }

    componentDidMount() {
        // this.list.scrollToIndex({ index, animated: false })
        const { gridData } = this.state
        const index = this.getActiveIndex(gridData)
        const offsetY = calculateTop(gridData[index].eventStart)

        // fixes https://github.com/facebook/react-native/issues/13202
        const wait = new Promise((resolve) => setTimeout(resolve, 500))
        wait.then(() => {
            this.refs.scheduleList.scrollTo({ x: 0, y: offsetY, animated: true })
        })
    }

    render() {

        return (
            <View style={styles.container}>
                {this.renderLoading()}
                <ScrollView
                    ref='scheduleList'
                    style={styles.container}
                    onLayout={(e) => {
                        const width = e.nativeEvent.layout.width
                        this.setState({
                            rootWidth: width,
                            columnWidth: (width - TimeslotWidth)
                        })
                    }}>
                    {this.renderGrid()}
                    {this.renderItems()}
                </ScrollView>
            </View>
        )
    }

    renderLoading() {
        if (!this.props.reservations || !this.props.reservations[this.props.selectedDay.toString('yyyy-MM-dd')]) {
            if (this.props.renderEmptyData) {
                return (<View style={styles.emptydata}>{this.props.renderEmptyData()}</View>)

            }
            return (<View style={styles.loading}><ActivityIndicator style={{ marginTop: 80 }} /></View>);
        }
    }

    renderGrid() {
        const { gridData } = this.state
        return gridData.map((item, index) => {
            return this.renderGridRow({ item, index })
        })
    }

    renderGridRow({ item, index }) {
        const { gridRowHeight } = this.state
        return (
            <View key={item.eventStart.toString()}
                style={[styles.row, {
                    height: gridRowHeight
                }]}
            >
                <View style={[styles.timeslot, { width: 80 }]}>
                    <Text style={styles.timeslot_text}>{item.time}</Text>
                </View>
                <View></View>
            </View>
        )
    }

    getActiveIndex = (data) => {
        let initialTime = new Date()
        const firstDay = new Date(this.state.initialDate)
        initialTime.setFullYear(firstDay.getFullYear())
        initialTime.setMonth(firstDay.getMonth())
        initialTime.setDate(firstDay.getDate())
        const currentTime = new Date(initialTime)

        // console.log('currentTime',currentTime)

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

    getEventsByDayFromSchedule = (schedule, duration) => {
        return getEventsByDayFromSchedule(schedule, duration)
    }

    renderItems() {
        const { rootWidth, columnWidth, reservations } = this.state
        return (
            <View
                style={[styles.contentRoot, {
                    width: (rootWidth - TimeslotWidth)
                }]}
            >
                {
                    reservations && reservations.map((schedule, index) => {
                        console.log(schedule)
                        const reservation = schedule.reservation
                        if (!reservation) return null

                        const time = new Date(reservation.time)

                        if (format(time, DateFormat) != format(new Date(reservationDate), DateFormat)) return null

                        const duration = reservation.duration
                        return (

                            // <View style={[styles.contentColumn, {
                            //     width: columnWidth
                            // }]}>
                                <Reservation
                                    style={{
                                        height: calculateHeight(duration),
                                        width: columnWidth - 10,
                                        top: calculateTop(time),
                                    }}
                                    when={formatTime(time)}
                                    who={reservation.speaker}
                                    what={'Men\'s cut'}
                                    where={'Green Room'}
                                    onPress={() => null}
                                />
                            // </View>
                        )
                    })
                }
            </View>
        )


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
    emptydata: {
        flex: 1,
        position: 'absolute',
        zIndex: 99,
        backgroundColor: '#ffffffaa',
        left: 0,
        top: 0,
        right: 0,
        bottom: 0
    },
    loading: {
        flex: 1,
        position: 'absolute',
        zIndex: 99,
        backgroundColor: '#ffffffaa',
        left: 0,
        top: 0,
        right: 0,
        bottom: 0
    }
})