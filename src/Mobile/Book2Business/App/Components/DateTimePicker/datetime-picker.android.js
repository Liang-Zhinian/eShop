import React from 'react'
import PropTypes from 'prop-types'
import { DatePickerAndroid } from 'react-native'
import { min } from 'date-fns';
import ReactNativeModal from "react-native-modal";

import TimePickerAndroid from './time-picker.android'
import styles from "./Styles";

export default class DateTimePickerAndroid extends React.Component {
    static propTypes = {
        date: PropTypes.instanceOf(Date),
        mode: PropTypes.oneOf(['date', 'time', 'datetime']),
        onCancel: PropTypes.func.isRequired,
        onConfirm: PropTypes.func.isRequired,
        onDismissAfterConfirm: PropTypes.func.isRequired,
        isVisible: PropTypes.bool,
        datePickerModeAndroid: PropTypes.oneOf(['calendar', 'spinner', 'default'])
    }

    static defaultProps = {
        date: new Date(),
        mode: 'date',
        onDismissAfterConfirm: (date) => { },
        isVisible: false,
        datePickerModeAndroid: 'default'
    }

    state = {
        minuteInterval: 15
    }

    componentDidUpdate = () => {
        if (this.props.isVisible) {
            if (this.props.mode == 'date' || this.props.mode == 'datetime') {
                this._showDatePickerAndroid().catch(console.error)
            } else {
                // this._showTimePickerAndroid().catch(console.error)
            }
        }
    }

    componentDidMount = () => {
        if (this.props.isVisible) {
            if (this.props.mode == 'date' || this.props.mode == 'datetime') {
                this._showDatePickerAndroid().catch(console.error)
            } else {
                // this._showTimePickerAndroid().catch(console.error)
            }
        }
    }

    render() {
        if (this.props.mode == 'time') {
            const {
                onConfirm,
                onDismissAfterConfirm,
                date,
                isVisible,
                minuteInterval,
                mode,
                pickerRefCb,
                ...otherProps
            } = this.props

            return (
                <Modal
                    animationType="slide"
                    visible={isVisible}
                >
                    <TimePickerAndroid
                        ref={pickerRefCb}
                        mode={mode}
                        minuteInterval={this.state.minuteInterval}
                        {...otherProps}
                        date={date}
                        onConfirm={(picked) => {

                            let date
                            if (this.props.date) {
                                const year = this.props.date.getFullYear()
                                const month = this.props.date.getMonth()
                                const day = this.props.date.getDate()
                                const hour = picked.amPm == 'PM' ? (12 + picked.hour) : picked.hour
                                const minute = picked.minute
                                date = new Date(year, month, day, hour, minute)
                            } else {
                                date = new Date()
                                if (picked.amPm == 'PM')
                                    date = date.setHours(12 + picked.hour)
                                date = date.setMinutes(picked.minute)
                            }
                            this.props.onConfirm(date)
                            this.props.onDismissAfterConfirm(date)
                        }}
                    />
                </Modal>
            )
        }

        return null
    }

    _showDatePickerAndroid = async () => {
        let picked;
        try {
            picked = await DatePickerAndroid.open({
                date: this.props.date,
                mode: this.props.datePickerModeAndroid
            })
        } catch ({ message }) {
            console.warn('Cannot open date picker', message)
            return
        }

        const { action, year, month, day } = picked
        if (action !== DatePickerAndroid.dismissedAction) {
            let date
            if (this.props.date && !isNaN(this.props.date.getTime())) {
                let hour = this.props.date.getHours()
                let minute = this.props.date.getMinutes()
                date = new Date(year, month, day, hour, minute)
            } else {
                date = new Date(year, month, day)
            }

            if (this.props.mode === 'datetime') {

            } else {
                this.props.onConfirm(date)
                this.props.onDismissAfterConfirm(date)
            }
        } else {
            this.props.onCancel()
        }
    }

    _showTimePickerAndroid = async () => {
        let picked;
        try {
            picked = await TimePickerAndroid.open({
                date: this.props.date,
                mode: this.props.datePickerModeAndroid
            })
        } catch ({ message }) {
            console.warn('Cannot open date picker', message)
            return
        }

        const { action, year, month, day } = picked
        if (action !== TimePickerAndroid.dismissedAction) {
            let date
            if (this.props.date && !isNaN(this.props.date.getTime())) {
                let hour = this.props.date.getHours()
                let minute = this.props.date.getMinutes()
                date = new Date(year, month, day, hour, minute)
            } else {
                date = new Date(year, month, day)
            }

            if (this.props.mode === 'datetime') {
                return
            } else {
                this.props.onConfirm(date)
                this.props.onDismissAfterConfirm(date)
            }
        } else {
            this.props.onCancel()
        }
    }
}