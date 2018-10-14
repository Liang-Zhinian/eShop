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

import Messages from '../../Components/Messages'
import Loading from '../../Components/Loading'
import Header from '../../Components/Header'
import Spacer from '../../Components/Spacer'
import Avatar from '../../Components/Avatar';
import RoundedButton from '../../Components/RoundedButton';
import TextInput from '../../Components/TextInput';
import FontAwesome from 'react-native-vector-icons/FontAwesome';
import styles from './Styles/StaffScheduleStyle';
import DateRangePicker from '../../Components/DateRangePicker'
import TimeRangePicker from '../../Components/TimeRangePicker'

class StaffSchedule extends React.Component {
    static navigationOptions = {
        title: 'Schedule'.toUpperCase(),
    };

    static defaultProps = {
        error: null,
        success: null
    }

    schedule = {
        Id: '4',
        IsAvailability: true,
        StartDateTime: new Date('2018-10-13 08:30'),
        EndDateTime: new Date('2018-10-30 22:30'),
        StaffId: 'xxxxxx',
        ServiceItemId: 'xxxx',
        LocationId: 'xxxx',
        SiteId: 'vj4ueTKK',
        BookableEndDateTime: 'vj4ueTKK',
        Sunday: true,
        Monday: true,
        Tuesday: true,
        Wednesday: false,
        Thursday: true,
        Friday: true,
        Saturday: true,
    };

    constructor(props) {
        super(props)

        this.schedule = props.navigation.getParam('schedule') || this.schedule;

        this.state = {
            IsAvailability: this.schedule.IsAvailability,
            StartDateTime: this.schedule.StartDateTime,
            EndDateTime: this.schedule.EndDateTime,
            StaffId: this.schedule.StaffId,
            ServiceItemId: this.schedule.ServiceItemId,
            LocationId: this.schedule.LocationId,
            BookableEndDateTime: this.schedule.BookableEndDateTime,
            Sunday: this.schedule.Sunday,
            Monday: this.schedule.Monday,
            Tuesday: this.schedule.Tuesday,
            Wednesday: this.schedule.Wednesday,
            Thursday: this.schedule.Thursday,
            Friday: this.schedule.Friday,
            Saturday: this.schedule.Saturday,

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
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,

        } = this.state

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
                    <RoundedButton style={styles.button} text='SAVE' />
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
            .then(() => console.log('Location Updated'))
            .catch(e => console.log(`Error: ${e}`))
    }
}

export default StaffSchedule;