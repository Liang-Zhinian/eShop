import React, { Component } from 'react'
import { TouchableOpacity, Text, Image, View } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'

import { Images } from '../../Themes'
import Layout from './Components/AddStaff'
import { checkMemberExistance } from '../../Actions/staffs'


class AddStaff extends Component {
    static propTypes = {
        staffs: PropTypes.shape({}).isRequired,
        onFormSubmit: PropTypes.func.isRequired,
        isLoading: PropTypes.bool.isRequired
    }

    static defaultProps = {
        match: null
    }

    static navigationOptions = {
        tabBarLabel: 'More',
        tabBarIcon: ({ focused }) => (
            <Image
                source={
                    focused
                        ? Images.activeInfoIcon
                        : Images.inactiveInfoIcon
                }
            />
        ),
        headerTitle: 'Add Staff'
    }

    state = {
        errorMessage: null,
        successMessage: null,
        memberCheckingStarted: false
    }

    onFormSubmit = (data) => {
        // const { onFormSubmit } = this.props;
        // return onFormSubmit(data)
        //   .then(mes => this.setState({ successMessage: mes, errorMessage: null }))
        //   .catch((err) => { this.setState({ errorMessage: err, successMessage: null }); throw err; });
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.staffs
            && typeof nextProps.staffs.memberExistance != undefined
            && nextProps.staffs.memberExistance === false) {
            this.setState({ errorMessage: 'User doesn\'t exist' })
        } else {
            this.setState({ errorMessage: null })
        }
    }

    render = () => {
        const {
            staffs,
            // Layout,
            isLoading,
            checkMemberExistance
        } = this.props
        console.log(staffs)

        const { successMessage, errorMessage } = this.state

        return (
            <Layout
                loading={isLoading}
                error={errorMessage}
                success={successMessage}
                onFormSubmit={this.onFormSubmit}
                // memberChecking={staffs.memberChecking || false}
                memberCheckingStarted={staffs.memberCheckingStarted || false}
                memberCheckingDone={staffs.memberCheckingDone || false}
                memberExistance={staffs.memberExistance || false}
                // cancelCheckingMemberExistance={this.cancelCheckingMemberExistance}
                startCheckingMemberExistance={this.startCheckingMemberExistance.bind(this)}
            />
        )
    }

    cancelCheckingMemberExistance() {
        clearTimeout(this.timeout)
    }

    startCheckingMemberExistance(username) {
        if (!username) return
        this.cancelCheckingMemberExistance()
        this.timeout = setTimeout(() => {
            this.setState({ memberCheckingStarted: true })
            this.checkMemberExistance(username)
        }, 2000)
    }

    checkMemberExistance(username) {
        const { checkMemberExistance } = this.props
        checkMemberExistance(username)
    }
}

const mapStateToProps = state => ({
    member: state.member || {},
    staffs: state.staffs || {},
    isLoading: state.status.loading || false,
})

const mapDispatchToProps = {
    onFormSubmit: function () { },
    // showError: setError,
    checkMemberExistance: checkMemberExistance,

}

export default connect(mapStateToProps, mapDispatchToProps)(AddStaff)
