import React, { Component } from 'react';

import { CalendarList as RNCalendarList } from 'react-native-calendars';
import { View, Text, StyleSheet } from 'react-native';

import styles from './Styles'

export default class CalendarList extends Component {
  constructor(props) {
    super(props);
    this.state = {};
    this.horizontal = typeof (props.horizontal) != undefined ? props.horizontal : true
    this.onDayPress = this.onDayPress.bind(this);
    this.scrollToDay = this.scrollToDay.bind(this)
  }

  onDayPress(day) {
    this.setState({
      selected: day.dateString
    });
    this.props.onDayPress && this.props.onDayPress(day)
  }

  scrollToDay(day, offset, animated){
    this.calendar.scrollToDay(day, offset, animated)
  }

  render() {
    let markedDates = {
      [this.state.selected]: { selected: true, disableTouchEvent: true, selectedDotColor: 'orange' }
    }

    if (this.props.markedDates) {

      if (this.props.markedDates instanceof Array) {
        this.props.markedDates.map(date => {
          if (date) markedDates[date] = { selected: true, disableTouchEvent: true, selectedDotColor: 'orange' }
        })
      } else {
        markedDates = this.props.markedDates
      }
    }

    const {
      calendarWidth, 
      onVisibleMonthsChange,
      current,
      scrollingEnabled,
      hideExtraDays,
      onLayout,
    } = this.props

    return (
      <RNCalendarList
        horizontal={this.horizontal}
        onLayout={onLayout}
        calendarWidth={calendarWidth}
        onVisibleMonthsChange={onVisibleMonthsChange}
        ref={(c) => this.calendar = c}
        current={current}
        scrollingEnabled={scrollingEnabled}
        hideExtraDays={hideExtraDays}
        pagingEnabledpagingEnabled={true}
        style={styles.calendar}
        onDayPress={this.onDayPress}
        markedDates={markedDates}

        theme={{
          textDisabledColor: '#d9e1e8',
        }}
      />
    );
  }
}