import React from 'react';
import {
    ScrollView,
    View,
    StyleSheet,
    KeyboardAvoidingView,
    Text,
    //   TextInput,
} from 'react-native';

import Avatar from '../../Components/Avatar';
import RoundedButton from '../../Components/RoundedButton';
import TextInput from '../../Components/TextInput';
import FontAwesome from 'react-native-vector-icons/FontAwesome';
import styles from './Styles/StaffScheduleStyle';

class StaffSchedule extends React.Component {
    static navigationOptions = {
        title: 'Schedule'.toUpperCase(),
    };

    schedule = {
        Id: '4',
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

    state = {
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

    handleChange = (name, val) => {
        this.setState({
            [name]: val
        })
    }


    render = () => (
        <ScrollView style={styles.root}>
            <KeyboardAvoidingView behavior='padding' style={styles.container}>
                <View style={styles.header}>
                    <Avatar img={this.user.photo} size='big' />
                </View>
                <View style={styles.section}>
                    <View style={[styles.row, styles.heading]}>
                        <Text style={styles.primary}>INFO</Text>
                    </View>
                    <View style={styles.row}>
                        <TextInput
                            label='First Name'
                            value={this.state.firstName}
                            type='right clear'
                            onChangeText={this.onFirstNameInputChanged}
                        />
                    </View>
                    <View style={styles.row}>
                        <TextInput
                            label='Last Name'
                            value={this.state.lastName}
                            onChangeText={this.onLastNameInputChanged}
                            type='right clear'
                        />
                    </View>
                    <View style={styles.row}>
                        <TextInput
                            label='Email'
                            value={this.state.email}
                            onChangeText={this.onEmailInputChanged}
                            type='right clear'
                        />
                    </View>
                    <View style={styles.row}>
                        <TextInput
                            label='Country'
                            value={this.state.country}
                            onChangeText={this.onCountryInputChanged}
                            type='right clear'
                        />
                    </View>
                    <View style={styles.row}>
                        <TextInput
                            label='Phone'
                            value={this.state.phone}
                            onChangeText={this.onPhoneInputChanged}
                            type='right clear'
                        />
                    </View>
                </View>
                <View style={styles.section}>
                    <View style={[styles.row, styles.heading]}>
                        <Text style={styles.primary}>CHANGE PASSWORD</Text>
                    </View>
                    <View style={styles.row}>
                        <TextInput
                            label='Old Password'
                            value={this.state.password}
                            type='right clear'
                            secureTextEntry
                            onChangeText={this.onPasswordInputChanged}
                        />
                    </View>
                    <View style={styles.row}>
                        <TextInput
                            label='New Password'
                            value={this.state.newPassword}
                            type='right clear'
                            secureTextEntry
                            onChangeText={this.onNewPasswordInputChanged}
                        />
                    </View>
                    <View style={styles.row}>
                        <TextInput
                            label='Confirm Password'
                            value={this.state.confirmPassword}
                            type='right clear'
                            secureTextEntry
                            onChangeText={this.onConfirmPasswordInputChanged}
                        />
                    </View>
                </View>
                <RoundedButton style={styles.button} text='SAVE' />
            </KeyboardAvoidingView>
        </ScrollView>
    );
}

export default StaffSchedule;