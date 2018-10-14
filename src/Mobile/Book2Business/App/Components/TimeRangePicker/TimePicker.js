import React, { Component } from 'react'
import {
    DatePickerIOS,
    View,
    StyleSheet,
    Platform,
    TouchableHighlight,
    Text
} from 'react-native'
import { TimePicker as TimePickerAndroid } from 'react-native-wheel-picker-android'
import PropTypes from 'prop-types'

const minutesArray = ['00', '15', '30', '45'];

export default class TimePicker extends Component {
    static propTypes = {
        initDate: PropTypes.instanceOf(Date).isRequired,
        onValueChanged: PropTypes.func.isRequired
    }

    static defaultProps = {
        initDate: new Date(),
        onValueChanged: (date) => { }
    }

    constructor(props) {
        super(props);
        this.state = { chosenDate: props.initDate };

        this.setDate = this.setDate.bind(this);

        this._bootStrap()
    }

    _bootStrap() {
        // this.setState({ chosenDate: this.props.initDate })
    }

    setDate(newDate) {
        this.setState({ chosenDate: newDate })
        this.props.onValueChanged(newDate)
    }

    render() {
        return (
            <View style={styles.container}>
                {
                    Platform.OS == 'ios' ?
                        <DatePickerIOS
                            mode='time'
                            minuteInterval={15}
                            date={this.state.chosenDate}
                            onDateChange={this.setDate}
                            style={{ fontSize: 8 }}
                        />
                        :
                        <TimePickerAndroid
                            minutes={minutesArray}
                            onTimeSelected={(date) => this.setDate(date)}
                            initDate={this.state.chosenDate.toISOString()}
                        />
                }
            </View>
        )
    }
}

export class TimePickerDialog extends Component {
    static propTypes = {
        style: PropTypes.any,
        initDate: PropTypes.instanceOf(Date).isRequired,
        onValueChanged: PropTypes.func.isRequired,
        onCancel: PropTypes.func.isRequired,
        cancelText: PropTypes.string.isRequired,
        onOk: PropTypes.func.isRequired,
        okText: PropTypes.string.isRequired,
    }

    static defaultProps = {
        initDate: new Date(),
        onValueChanged: (date) => { },
        style: {},
        onCancel: () => { },
        cancelText: 'Cancel',
        onOk: (date) => { },
        okText: 'Okay'
    }

    constructor(props) {
        super(props)
        this.state = {
            chosenDate: props.initDate
        }
    }

    render() {
        return (
            <View style={[styles.dialogContainer, this.props.style]}>
                <TimePicker
                    onValueChanged={(date) => {
                        this.setState({ chosenDate: date })
                    }}
                    initDate={this.props.initDate}
                />
                <View style={{ flexDirection: 'row' }}>
                    <TouchableHighlight onPress={this.props.onCancel}
                        style={[styles.button, styles.okButton]}
                    >
                        <Text style={styles.text}>{this.props.cancelText}</Text>
                    </TouchableHighlight>
                    <TouchableHighlight onPress={() => { this.props.onOk(this.state.chosenDate) }}
                        style={[styles.button, styles.okButton]}
                    >
                        <Text style={styles.text}>{this.props.okText}</Text>
                    </TouchableHighlight>
                </View>
            </View >
        )
    }
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        backgroundColor: 'transparent'
    },
    dialogContainer: {
        flex: 1,
        width: '100%',
        height: '100%',
        justifyContent: 'center',
        borderRadius: 10,
        backgroundColor: 'white',
        borderWidth: 1,
        borderColor: 'gray',
        alignSelf: 'center'
    },
    text: {
        fontSize: 20,
        textAlign: 'center',
        color: 'blue',
        fontWeight: 'bold'
    },
    button: {
        flex: 1,
        borderColor: 'gray',
        borderWidth: 1,
        justifyContent: 'center',
        alignContent: 'center',
        height: 60
    },
    cancelButton: {
        borderLeftWidth: 0,
        borderBottomWidth: 0
    },
    okButton: {
        borderLeftWidth: 0,
        borderRightWidth: 0,
        borderBottomWidth: 0,
    }
})
