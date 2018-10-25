import React, { Component } from 'react'
import { AppState, View, Image, StyleSheet, Text } from 'react-native'
import { connect } from 'react-redux'
import {
  format,
} from 'date-fns'
import Icon from 'react-native-vector-icons/MaterialIcons';

import GradientView from '../../Components/GradientView'
import { TimeFormat } from '../../Constants/date'
import Agenda from '../../Components/Agenda'
import { Images } from '../../Themes'
import styles from './Styles/ScheduleScreenStyle'
import GradientHeader, { Header } from '../../Components/GradientHeader'
import AnimatedTouchable from '../../Components/AnimatedTouchable'

const schedule = require('../../Fixtures/schedule').schedule

function formatTime(date) {
  return `${format(date, TimeFormat)}`
}

class ScheduleScreen extends Component {

  static navigationOptions = {
    tabBarLabel: 'Schedule',
    tabBarIcon: ({ focused }) => (
      <Image
        source={
          focused
            ? Images.activeScheduleIcon
            : Images.inactiveScheduleIcon
        }
      />
    ),
    title: 'Schedule'
  }

  constructor(props) {
    super(props)

    this.state = {
      items: {}
    }
  }

  render() {
    return (
      <GradientView style={[styles.linearGradient, { flex: 1 }]}>
        <GradientHeader>
          <Header title='Schedule' goBack={_ => null} />
        </GradientHeader>
        <Agenda
          items={this.state.items}
          loadItemsForMonth={this.loadItems.bind(this)}
          selected={this.props.currentTime}
          renderItem={this.renderItem.bind(this)}
          renderEmptyDate={this.renderEmptyDate.bind(this)}
          rowHasChanged={this.rowHasChanged.bind(this)}
          theme={{
            gridCellHeight: 40
          }}
        />
        <AnimatedTouchable
          onPress={this.handleAddButton.bind(this)}
        >
          <View style={styles.addButton}><Icon style={{ color: 'white' }} size={40} name='add' /></View>
        </AnimatedTouchable>
      </GradientView>
    )
  }

  loadItems(day) {
    this.state.items[day.dateString] = [];
    setTimeout(() => {
      for (let i = 0; i < schedule.length; i++) {
        const event = schedule[i]
        const time = event.time;
        const strTime = this.timeToString(time);
        // console.log('strTime', strTime, day)
        if (day.dateString != strTime) continue

        if (!this.state.items[strTime]) {
          this.state.items[strTime] = [];
        }

        this.state.items[strTime].push({
          name: 'Item for ' + strTime,
          height: Math.max(50, Math.floor(Math.random() * 150)),
          ...event
        });
      }

      // console.log(this.state.items);
      const newItems = {};
      Object.keys(this.state.items).forEach(key => { newItems[key] = this.state.items[key]; });
      this.setState({
        items: newItems
      });
    }, 1000);
    // console.log(`Load Items for ${day.year}-${day.month}`);
  }

  renderItem(item) {
    const time = new Date(item.time)
    const when = formatTime(time)
    const who = item.type == 'talk' ? item.speaker : null
    const what = item.type
    const where = 'Green Room'

    return (
      <View style={[styles.item, { display: 'flex' }]}>
        <Text style={styles.when}>
          {when}
        </Text>
        {
          item.type == 'talk' && <Text style={styles.who}>
            {who}
          </Text>
        }
        <Text style={styles.what}>
          {what}
        </Text>
        {
          item.type == 'talk' && <Text style={styles.where}>
            {where}
          </Text>
        }
      </View>
    );
  }

  renderEmptyDate() {
    return (
      <View style={styles.emptyDate}><Text>This is empty date!</Text></View>
    );
  }

  rowHasChanged(r1, r2) {
    return r1.name !== r2.name;
  }

  timeToString(time) {
    const date = new Date(time)
    return date.toISOString().split('T')[0]
  }

  handleAddButton() {
    this.props.navigation.navigate('AppointmentSchedule', { ActionType: 'Add' })
  }
}


const mapStateToProps = (state) => {
  return {
    member: state.member || {},
    currentTime: new Date(state.schedule.currentTime),
    schedule: state.schedule.speakerSchedule,
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(ScheduleScreen)
