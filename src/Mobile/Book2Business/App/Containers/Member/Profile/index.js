import React from 'react';
import {
    ScrollView,
    View,
    StyleSheet,
    KeyboardAvoidingView,
    Text,
    //   TextInput,
} from 'react-native';
import { connect } from 'react-redux'

import Avatar from '../../../Components/Avatar';
import RoundedButton from '../../../Components/RoundedButton';
import TextInput from '../../../Components/TextInput';
import styles from './Styles';
import { logout, setError } from '../../../Actions/member'

class ProfileSettings extends React.Component {
    static navigationOptions = {
        title: 'Profile Settings'.toUpperCase(),
    };

    user = {
        id: 4,
        firstName: 'Nancy',
        lastName: 'O\'Crevan',
        email: 'nocrevan3@zimbio.com',
        country: 'China',
        password: 'W0NxvHo2C',
        newPassword: 'vj4ueTKK',
        confirmPassword: 'vj4ueTKK',
        postCount: 78,
        phone: '86-(499)721-5796',
        followingCount: 86,
        followersCount: 3303,
        images: [],
        photo: require('../../../Images/img-barista.jpg'),
    };

    state = {
        firstName: this.user.firstName,
        lastName: this.user.lastName,
        email: this.user.email,
        country: this.user.country,
        phone: this.user.phone,
        password: this.user.password,
        newPassword: this.user.newPassword,
        confirmPassword: this.user.confirmPassword,
    };

    onFirstNameInputChanged = (text) => {
        this.setState({ firstName: text });
    };

    onLastNameInputChanged = (text) => {
        this.setState({ lastName: text });
    };

    onEmailInputChanged = (text) => {
        this.setState({ email: text });
    };

    onCountryInputChanged = (text) => {
        this.setState({ country: text });
    };

    onPhoneInputChanged = (text) => {
        this.setState({ phone: text });
    };

    onPasswordInputChanged = (text) => {
        this.setState({ password: text });
    };

    onNewPasswordInputChanged = (text) => {
        this.setState({ newPassword: text });
    };

    onConfirmPasswordInputChanged = (text) => {
        this.setState({ confirmPassword: text });
    };

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

                <RoundedButton onPress={this.props.logout} style={styles.button} text='SIGN OUT' />
            </KeyboardAvoidingView>
        </ScrollView>
    );
}

const mapStateToProps = (state) => ({})

const mapDispatchToProps = {
    logout: logout,
    showError: setError
}

export default connect(mapStateToProps, mapDispatchToProps)(ProfileSettings);