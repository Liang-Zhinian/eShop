import React, { Component } from 'react'
import {
  Text,
  View,
  StyleSheet,
  TouchableOpacity,
  Animated,
  LayoutAnimation
} from 'react-native'
import HorizontalCalendarList from '../../../Components/HorizontalCalendarList'

export default class AgendaScreen extends Component {
  constructor(props) {
    super(props)
    this.state = {
      items: {},
      calendarHeight: new Animated.Value(40),
      showCalendarOptions: false,
    }

    this.toggleCalendar = this.toggleCalendar.bind(this)
  }

  render() {

    const { calendarHeight } = this.state

    return (
      // <Agenda
      //   items={this.props.items || {}}
      //   loadItemsForMonth={this.props.loadItemsForMonth}
      //   selected={'2017-05-16'}
      //   renderItem={this.props.renderItem}
      //   renderEmptyDate={this.renderEmptyDate.bind(this)}
      //   rowHasChanged={this.rowHasChanged.bind(this)}
      //   renderDay={this.props.renderDay}
      // />
      <View style={{ flex: 1 }}>
        <View style={{ flex: 1, height: calendarHeight }}>
          <HorizontalCalendarList />
        </View>
        <View style={{ justifyContent: 'center', alignItems: 'center', backgroundColor: 'white' }}>
          <TouchableOpacity onPress={this.toggleCalendar} style={{ width: '50%', height: 15, backgroundColor: 'gray' }}></TouchableOpacity>
        </View>
        <View style={{ flex: 1 }}>
          {this.props.renderItem()}
        </View>
      </View>
    )
  }

  toggleCalendar() {
    const { showCalendarOptions } = this.state
    // LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut);
    let calendarHeight = 0

    if (showCalendarOptions) {
      this.setState({
        showCalendarOptions: false,
      })
      calendarHeight = 40
    } else {
      this.setState({
        showCalendarOptions: true,
      })
      calendarHeight = 370
    }

    const animate = Animated.timing;
    const duration = 200
    animate(this.state.calendarHeight, {
      toValue: calendarHeight,
      duration
    })

  }

  loadItems(day) {
    setTimeout(() => {
      // for (let i = -15; i < 85; i++) {
      for (let i = 0; i < 1; i++) {
        const time = day.timestamp + i * 24 * 60 * 60 * 1000
        const strTime = this.timeToString(time)
        if (!this.state.items[strTime]) {
          this.state.items[strTime] = []
          const numItems = Math.floor(Math.random() * 5)
          for (let j = 0; j < numItems; j++) {
            this.state.items[strTime].push({
              name: 'Item for ' + strTime,
              height: Math.max(50, Math.floor(Math.random() * 150))
            })
          }
        }
      }

      const newItems = {}
      Object.keys(this.state.items).forEach(key => { newItems[key] = this.state.items[key] })
      this.setState({
        items: newItems
      })
    }, 1000)
    // console.log(`Load Items for ${day.year}-${day.month}`);
  }

  renderItem(item) {
    return (
      <View style={[styles.item, { height: item.height }]}><Text>{item.name}</Text></View>
    )
  }

  renderEmptyDate() {
    return (
      <View style={styles.emptyDate}><Text>This is empty date!</Text></View>
    )
  }

  rowHasChanged(r1, r2) {
    return r1.name !== r2.name
  }

  timeToString(time) {
    const date = new Date(time)
    return date.toISOString().split('T')[0]
  }
}

const styles = StyleSheet.create({
  item: {
    backgroundColor: 'white',
    flex: 1,
    borderRadius: 5,
    padding: 10,
    marginRight: 10,
    marginTop: 17
  },
  emptyDate: {
    height: 15,
    flex: 1,
    paddingTop: 30
  },

})
