import React, { Component } from 'react';

import { CalendarList } from 'react-native-calendars';
import { View, Text, StyleSheet } from 'react-native';

export default class HorizontalCalendarList extends Component {
  constructor(props) {
    super(props);
    this.state = {};
    this.onDayPress = this.onDayPress.bind(this);
  }

  onDayPress(day) {
    this.setState({
      selected: day.dateString
    });
    this.props.onDayPress && this.props.onDayPress(day)
  }

  render() {
    let markedDates = {
      [this.state.selected]: { selected: true, disableTouchEvent: true, selectedDotColor: 'orange' }
    }

    if (this.props.markedDates) {
      this.props.markedDates.map(date=>{
        if (date) markedDates[date] = { selected: true, disableTouchEvent: true, selectedDotColor: 'orange' }
      })
    }

    return (
      <CalendarList
        current={'2018-10-09'}
        horizontal
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

const styles = StyleSheet.create({
  calendar: {
    borderTopWidth: 1,
    paddingTop: 5,
    borderBottomWidth: 1,
    borderColor: '#eee',
    height: 350
  },
  text: {
    textAlign: 'center',
    borderColor: '#bbb',
    padding: 10,
    backgroundColor: '#eee'
  },
  container: {
    flex: 1,
    backgroundColor: 'gray'
  }
});