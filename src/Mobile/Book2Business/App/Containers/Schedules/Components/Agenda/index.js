import React, { Component } from 'react'
import {
  Text,
  View,
  StyleSheet,
  TouchableOpacity,
  Animated,
  LayoutAnimation,
  Dimensions
} from 'react-native'

import styleConstructor from './style';
import CalendarList from '../../../../Components/CalendarList'

const XDate = require('xdate');

function weekDayNames(firstDayOfWeek = 0) {
  let weekDaysNames = XDate.locales[XDate.defaultLocale].dayNamesShort;
  const dayShift = firstDayOfWeek % 7;
  if (dayShift) {
    weekDaysNames = weekDaysNames.slice(dayShift).concat(weekDaysNames.slice(0, dayShift));
  }
  return weekDaysNames;
}

const HEADER_HEIGHT = 104;
const KNOB_HEIGHT = 24;

export default class AgendaScreen extends Component {
  constructor(props) {
    super(props)
    this.state = {
      items: {},
      calendarHeight: 120, //new Animated.Value(120),
      showCalendarOptions: false,
      scrollY: new Animated.Value(0),
    }

    this.toggleCalendar = this.toggleCalendar.bind(this)
    const windowSize = Dimensions.get('window');
    this.viewHeight = windowSize.height;
    this.viewWidth = windowSize.width;
    this.styles = styleConstructor(props.theme);
  }

  render() {

    const { calendarHeight } = this.state
    const weekDaysNames = weekDayNames();

    const agendaHeight = Math.max(0, this.viewHeight - HEADER_HEIGHT);
    const weekdaysStyle = [this.styles.weekdays, {
      opacity: this.state.scrollY.interpolate({
        inputRange: [agendaHeight - HEADER_HEIGHT, agendaHeight],
        outputRange: [0, 1],
        extrapolate: 'clamp',
      }),
      transform: [{ translateY: this.state.scrollY.interpolate({
        inputRange: [Math.max(0, agendaHeight - HEADER_HEIGHT), agendaHeight],
        outputRange: [-HEADER_HEIGHT, 0],
        extrapolate: 'clamp',
      })}]
    }];

    return (
      <View style={styles.container}>
        <View style={[styles.calendarContainer]}>
          <View style={[styles.calendar, { height: calendarHeight }]}>
            <CalendarList horizontal={false} />
          </View>

          <View style={weekdaysStyle}>
            {weekDaysNames.map((day, index) => (
              <Text allowFontScaling={false} key={day + index} style={this.styles.weekday} numberOfLines={1}>{day}</Text>
            ))}
          </View>
          <TouchableOpacity onPress={this.toggleCalendar} style={styles.expandingBar}>
            <View style={styles.expandingButton}></View>
          </TouchableOpacity>
        </View>
        <View style={styles.content}>
          {this.props.renderItem()}
        </View>
      </View>
    )
  }

  toggleCalendar() {
    const { showCalendarOptions } = this.state
    LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut);
    let calendarHeight = 0

    if (showCalendarOptions) {
      this.setState({
        showCalendarOptions: false,
        calendarHeight: 120
      })
    } else {
      this.setState({
        showCalendarOptions: true,
        calendarHeight: 320
      })
    }

    // const animate = Animated.timing;
    // const duration = 200
    // animate(this.state.calendarHeight, {
    //   toValue: calendarHeight,
    //   duration
    // })

  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1
  },
  calendarContainer: {
    position: 'absolute',
    zIndex: 99
  },
  calendar: {
    backgroundColor: 'yellow'
  },
  content: {
    flex: 1,
    paddingTop: 135,
    // position: 'relative',
    backgroundColor: 'white'
  },
  expandingBar: {
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: 'white',
    height: 15,
  },
  expandingButton: {
    width: '50%',
    height: 10,
    backgroundColor: 'gray',
    borderRadius: 4
  },
})
