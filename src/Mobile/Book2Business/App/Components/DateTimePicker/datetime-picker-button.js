import React, { Component } from "react";
import { Text, TouchableOpacity, View } from "react-native";
import {
    format,
} from 'date-fns'
import Icon from 'react-native-vector-icons/FontAwesome'

import DateTimePicker from "./index";
import styles from "./datetime-picker-button.style";
import { TimeFormat } from '../../Constants/date'

function formatTime(date) {
    return `${format(date, TimeFormat)}`
}

export default class DateTimePickerTester extends Component {
    constructor(props) {
        super(props)

        let initialDate = props.initialDate || new Date()
        let selectedDate = props.mode == 'time' ? formatTime(initialDate) : initialDate.toDateString()

        this.state = {
            isDateTimePickerVisible: false,
            initialDate: initialDate,
            selectedDate: selectedDate
        }
    }

    _showDateTimePicker = () => this.setState({ isDateTimePickerVisible: true });

    _hideDateTimePicker = () => this.setState({ isDateTimePickerVisible: false });

    _handleDatePicked = date => {
        let selectedDate = this.props.mode == 'time' ? formatTime(date) : date.toDateString()
        this.setState({ selectedDate });
        this._hideDateTimePicker();
        this.props.onValueChanged && this.props.onValueChanged({ value: date })
    };

    render() {
        const { isDateTimePickerVisible, selectedDate } = this.state;

        return (
            <View style={this.props.style}>
                <TouchableOpacity onPress={this._showDateTimePicker}>
                    <View style={[styles.button]}>
                        <Text style={[styles.label]}>{selectedDate}</Text>
                        <View style={styles.icon}><Icon size={25} color='#000' name={'angle-right'} /></View>
                    </View>
                </TouchableOpacity>

                {/* <Text style={styles.text}>{selectedDate}</Text> */}

                <DateTimePicker
                    mode={this.props.mode}
                    minuteInterval={this.props.minuteInterval}
                    isVisible={isDateTimePickerVisible}
                    onConfirm={this._handleDatePicked}
                    onCancel={this._hideDateTimePicker}
                />
            </View>
        );
    }
}