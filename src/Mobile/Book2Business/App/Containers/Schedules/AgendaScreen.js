import React, { Component } from 'react'
import { AppState, View, Image, FlatList, Text } from 'react-native'
import {
  compareAsc,
  isSameDay,
  addMinutes,
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
import Agenda from './Components/Agenda'
import Config from '../../Config/AppConfig'
import ReservationList from './Components/ReservationList'

const isActiveCurrentDay = (currentTime, activeDay) =>
  isSameDay(currentTime, new Date(Config.conferenceDates[activeDay]))

const addSpecials = (specialTalksList, talks) =>
  map((talk) => assoc('special', contains(talk.title, specialTalksList), talk), talks)

export default class AgendaScreen extends Component {
  constructor (props) {
    super(props)
    const appState = AppState.currentState

    this.state = { agendaData: {} }
  }

  render () {
    // const { isCurrentDay, activeDay, data } = this.state
    return (
      <Agenda
          // specify how each date should be rendered. day can be undefined if the item is not first in that day.
        renderDay={(day, item) => {
          return (
            <View />
          )
        }}
        // renderDay={(day, item) => (<Text>{day ? day.day : 'item'}</Text>)}
        items={this.state.agendaData}
        renderItem={(item) => {
          return <ReservationList /> //<ScheduleScreen navigation={this.props.navigation} />
        }}
        loadItemsForMonth={(month) => {
          const time = month.timestamp
          const strTime = this.timeToString(time)
          if (!this.state.agendaData[strTime]) {
            this.state.agendaData[strTime] = []
              // const numItems = data.length;
              // for (let j = 0; j < numItems; j++) {
              //   this.state.agendaData[strTime].push({
              //     name: data[j].title,
              //     ...data[j]
              //   });
              // }
            this.state.agendaData[strTime].push({
              name: strTime
            })
          }
          const newItems = {}
          Object.keys(this.state.agendaData).forEach(key => { newItems[key] = this.state.agendaData[key] })
          this.setState({
            agendaData: newItems
          })
        }} />
    )
  }

  timeToString (time) {
    const date = new Date(time)
    return date.toISOString().split('T')[0]
  }
}
