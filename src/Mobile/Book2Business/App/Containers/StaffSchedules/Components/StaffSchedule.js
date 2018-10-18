import React from 'react';
import {
    ScrollView,
    View,
    StyleSheet,
    KeyboardAvoidingView,
    Text,
    //   TextInput,
} from 'react-native';
import {
    Label, Button, Switch, Segment
} from 'native-base'
import PropTypes from 'prop-types'

import Messages from '../../../Components/Messages'
// import Loading from '../../../Components/Loading'
// import Header from '../../../Components/Header'
// import Spacer from '../../../Components/Spacer'
// import Avatar from '../../../Components/Avatar';
import RoundedButton from '../../../Components/RoundedButton';
import styles from '../Styles/StaffScheduleStyle';
import DateRangePicker from '../../../Components/DateRangePicker'
import TimeRangePicker from '../../../Components/TimeRangePicker'

class StaffSchedule extends React.Component {
    static propTypes = {
        error: PropTypes.string,
        success: PropTypes.string,
        loading: PropTypes.bool.isRequired,
        onFormSubmit: PropTypes.func.isRequired,
        staffSchedule: PropTypes.shape({
            Id: PropTypes.string.isRequired,
            IsAvailability: PropTypes.bool.isRequired,
            StartDateTime: PropTypes.instanceOf(Date).isRequired,
            EndDateTime: PropTypes.instanceOf(Date).isRequired,
            StaffId: PropTypes.string.isRequired,
            ServiceItemId: PropTypes.string.isRequired,
            LocationId: PropTypes.string.isRequired,
            SiteId: PropTypes.string.isRequired,
            BookableEndDateTime: PropTypes.string,
            Sunday: PropTypes.bool.isRequired,
            Monday: PropTypes.bool.isRequired,
            Tuesday: PropTypes.bool.isRequired,
            Wednesday: PropTypes.bool.isRequired,
            Thursday: PropTypes.bool.isRequired,
            Friday: PropTypes.bool.isRequired,
            Saturday: PropTypes.bool.isRequired,
        }).isRequired
    }

    static defaultProps = {
        error: null,
        success: null
    }

    constructor(props) {
        super(props)

        this.state = {
            IsAvailability: this.props.staffSchedule.IsAvailability || true,
            StartDateTime: this.props.staffSchedule.StartDateTime,
            EndDateTime: this.props.staffSchedule.EndDateTime,
            StaffId: this.props.staffSchedule.StaffId || '',
            ServiceItemId: this.props.staffSchedule.ServiceItemId || '',
            LocationId: this.props.staffSchedule.LocationId || '',
            SiteId: this.props.staffSchedule.SiteId || '',
            BookableEndDateTime: this.props.staffSchedule.BookableEndDateTime,
            Sunday: this.props.staffSchedule.Sunday || false,
            Monday: this.props.staffSchedule.Monday || false,
            Tuesday: this.props.staffSchedule.Tuesday || false,
            Wednesday: this.props.staffSchedule.Wednesday || false,
            Thursday: this.props.staffSchedule.Thursday || false,
            Friday: this.props.staffSchedule.Friday || false,
            Saturday: this.props.staffSchedule.Saturday || false,

        };

        this.handleChange = this.handleChange.bind(this)
        this.handleSubmit = this.handleSubmit.bind(this)
    }

    render = () => {
        const { loading, error, success } = this.props

        const {
            IsAvailability,
            StartDateTime,
            EndDateTime,
            StaffId,
            ServiceItemId,
            LocationId,
            BookableEndDateTime,
            SiteId,
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,

        } = this.state

        console.log(this.state)

        return (
            <ScrollView style={styles.root}>
                <KeyboardAvoidingView behavior='padding' style={styles.container}>

                    {error && <Messages message={error} />}
                    {success && <Messages message={success} type='success' />}
                    <View style={styles.section}>
                        <View style={[styles.row, styles.heading]}>
                            <Text style={styles.primary}>What</Text>
                        </View>
                        <View style={styles.row}>
                            <Label>Availability / Unavailability</Label>
                            <View style={styles.inputContainer}>
                                <Switch value={IsAvailability} onValueChange={v => this.handleChange('IsAvailability', v)} />
                            </View>
                        </View>
                    </View>
                    <View style={styles.section}>
                        <View style={[styles.row, styles.heading]}>
                            <Text style={styles.primary}>When</Text>
                        </View>
                        <View style={styles.row}>
                            <Label>Date Range</Label>
                            <View style={styles.inputContainer}>
                                <DateRangePicker
                                    initStartDate={StartDateTime}
                                    initEndDate={EndDateTime}
                                    onValueChanged={(value) => {
                                        let { StartDateTime, EndDateTime } = this.state
                                        StartDateTime.setFullYear(value[0].getFullYear())
                                        StartDateTime.setMonth(value[0].getMonth())
                                        StartDateTime.setDate(value[0].getDate())
                                        EndDateTime.setFullYear(value[1].getFullYear())
                                        EndDateTime.setMonth(value[1].getMonth())
                                        EndDateTime.setDate(value[1].getDate())
                                        this.setState({
                                            StartDateTime,
                                            EndDateTime,
                                        })
                                    }} />
                            </View>
                        </View>
                        <View style={styles.row}>
                            <Label>Days</Label>
                            <View style={styles.inputContainer}>
                                <Segment>
                                    <Button
                                        active={Sunday}
                                        first style={styles.segmentButton}
                                        onPress={() => {
                                            this.toggleDayEnability('Sunday')
                                        }}>
                                        <Text>Sun</Text>
                                    </Button>
                                    <Button active={Monday}
                                        style={styles.segmentButton}
                                        onPress={() => {
                                            this.toggleDayEnability('Monday')
                                        }}>
                                        <Text>Mon</Text>
                                    </Button>
                                    <Button active={Tuesday} style={styles.segmentButton}
                                        onPress={() => {
                                            this.toggleDayEnability('Tuesday')
                                        }}>
                                        <Text>Tue</Text>
                                    </Button>
                                    <Button active={Wednesday} style={styles.segmentButton}
                                        onPress={() => {
                                            this.toggleDayEnability('Wednesday')
                                        }}>
                                        <Text>Wed</Text>
                                    </Button>
                                    <Button active={Thursday} style={styles.segmentButton}
                                        onPress={() => {
                                            this.toggleDayEnability('Thursday')
                                        }}>
                                        <Text>Thu</Text>
                                    </Button>
                                    <Button active={Friday} style={styles.segmentButton}
                                        onPress={() => {
                                            this.toggleDayEnability('Friday')
                                        }}>
                                        <Text>Fri</Text>
                                    </Button>
                                    <Button active={Saturday} last style={styles.segmentButton}
                                        onPress={() => {
                                            this.toggleDayEnability('Saturday')
                                        }}>
                                        <Text>Sat</Text>
                                    </Button>
                                </Segment>
                            </View>
                        </View>
                        <View style={styles.row}>
                            <Label>Time</Label>
                            <View style={[styles.inputContainer]}>
                                <TimeRangePicker
                                    initStartDate={StartDateTime}
                                    initEndDate={EndDateTime}
                                    onValueChanged={(value) => {
                                        this.setState({
                                            StartDateTime: value[0],
                                            EndDateTime: value[1],
                                        })
                                    }} />
                            </View>
                        </View>
                    </View>
                    <RoundedButton 
                    style={styles.button} 
                    text='SAVE'
                    onPress={this.handleSubmit} />
                </KeyboardAvoidingView>
            </ScrollView>
        )
    }

    toggleDayEnability(name) {
        this.handleChange(name, !this.state[name])
    }

    handleChange = (name, val) => {
        this.setState({
            [name]: val
        })
    }

    handleSubmit = () => {
        const { onFormSubmit } = this.props
        onFormSubmit(this.state)
            .then(() => console.log('Staff Schedule Updated'))
            .catch(e => console.log(`Error: ${e}`))
    }
}

export default StaffSchedule;