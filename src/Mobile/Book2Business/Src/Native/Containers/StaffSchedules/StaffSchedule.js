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
    Container, Content, Body, ListItem, Form, Item, Label, Input, CheckBox, Button, Switch
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
        StartDateTime: (new Date()).toString(),
        EndDateTime: (new Date()).toString(),
        StaffId: 'xxxxxx',
        ServiceItemId: 'xxxx',
        LocationId: 'xxxx',
        SiteId: 'vj4ueTKK',
        BookableEndDateTime: 'vj4ueTKK',
        Sunday: true,
        Monday: true,
        Tuesday: true,
        Wednesday: true,
        Thursday: true,
        Friday: true,
        Saturday: true,
    };

    constructor(props) {
        super(props)
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
                            <Label>Availability</Label>
                            <Switch value={IsAvailability} onValueChange={v => this.handleChange('IsAvailability', v)} />
                        </View>
                    </View>
                    <View style={styles.section}>
                        <View style={[styles.row, styles.heading]}>
                            <Text style={styles.primary}>When</Text>
                        </View>
                        <View style={styles.row}>
                            <Label>Date Range</Label>
                            <DateRangePicker />
                        </View>
                        <View style={styles.row}>
                            <Label>Days</Label>

                        </View>
                        <View style={styles.row}>
                            <Label>Time</Label>

                        </View>
                    </View>
                    <RoundedButton style={styles.button} text='SAVE' />
                </KeyboardAvoidingView>
            </ScrollView>
        )
    }
}

export default StaffSchedule;