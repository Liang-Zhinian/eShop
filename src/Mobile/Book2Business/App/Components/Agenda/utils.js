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

import { TimeFormat } from '../../Constants/date'

function formatTime(date) {
    return `${format(date, TimeFormat)}`
}

function buildTimeslots(date, duration = 15) {
    let timeslots = []
    // let someDate = MinDate
    let someDate = new Date(date)
    let nextDate = addDays(new Date(date), 1)

    while (someDate < nextDate) {
        timeslots.push({ time: formatTime(someDate), datetime: someDate })
        someDate = addMinutes(someDate, duration)
    }
    return timeslots
}

function calculateTop(time, cellHeight = 30, duration = 15) {
    let copiedDate = new Date(time)
    copiedDate.setHours(0)
    copiedDate.setMinutes(0)
    copiedDate.setSeconds(0)

    let beginOfTheDay = copiedDate
    let mins = differenceInMinutes(time, beginOfTheDay)
    let top = calculateHeight(mins, cellHeight, duration)

    return top
}

function calculateHeight(mins, cellHeight = 30, duration = 15) {
    let height = cellHeight * (mins / duration)

    return height
}

function getEventsByDayFromSchedule(schedule, duration = 15) {

    const mergeTimes = (e) => {
        const eventDuration = duration //Number(e.duration)
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

export { formatTime, buildTimeslots, calculateTop, calculateHeight, getEventsByDayFromSchedule }