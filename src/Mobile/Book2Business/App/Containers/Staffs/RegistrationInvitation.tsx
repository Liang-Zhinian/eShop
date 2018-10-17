import React from 'react'
import PropTypes from 'prop-types'
import { ScrollView, View, Text, TextInput } from 'react-native';
import { connect } from 'react-redux'

import Messages from '../../Components/Messages'
import Loading from '../../Components/Loading'
// import Header from '../../../Components/Header'
import Spacer from '../../Components/Spacer'
import GradientHeader, { Header } from '../../Components/GradientHeader'
import { Colors } from '../../Themes'
import AnimatedSubmissionButton from '../../Components/AnimatedSubmissionButton'
import { offerRegistrationInvitation } from '../../Actions/identityAccess'

interface Props {
    onDismiss(): void
    onFormSubmit(v): Promise<{}>
    member: {
        error: string
        loading: boolean
    }
    identityAccess: {
        error: string
        loading: boolean
    }
}

interface State {
    invitationDescription: string
}

class RegistrationInvitation extends React.Component<Props, State> {
    static propTypes = {
        onFormSubmit: PropTypes.func.isRequired,
        onDismiss: PropTypes.func.isRequired,
        member: PropTypes.shape({}).isRequired,
        identityAccess: PropTypes.shape({}),
    }

    constructor(props) {
        super(props)

        this.handleChange = this.handleChange.bind(this)
        this.handleSubmit = this.handleSubmit.bind(this)
        this.handleDismiss = this.handleDismiss.bind(this)
    }

    state = {
        invitationDescription: ''
    }

    handleChange = (name, val) => {
        this.setState({
            [name]: val
        })
    }

    handleSubmit = (onSucceed: () => void) => {
        const { onFormSubmit } = this.props
        onFormSubmit && onFormSubmit(this.state.invitationDescription)
            .then((res) => {
                //if (res === this.state.invitationDescription) {
                    onSucceed();
                    // this.props.onDismiss();
                // }
            })
    }

    handleDismiss = () => {
        this.props.onDismiss()
    }

    render = () => {
        const { invitationDescription } = this.state
        return (
            <View style={{ flex: 1, backgroundColor: Colors.snow }}>
                <GradientHeader>
                    <Header title='Offer Registration Invitation'
                        goBack={this.handleCancel} />
                </GradientHeader>
                <ScrollView style={{ padding: 10 }}>
                    <View style={{ marginBottom: 20 }}>
                        <Text style={{ fontSize: 20 }}>
                            Invitation Description
                        </Text>
                        <TextInput
                            style={{
                                marginTop: 10,
                                fontSize: 20,
                                lineHeight: 26,
                                padding: 3,
                                borderBottomWidth: 1
                            }}
                            value={invitationDescription}
                            onChangeText={(v) => this.handleChange('invitationDescription', v)}
                        />
                    </View>

                    <Spacer size={20} />

                    <View style={{ marginBottom: 20 }}>
                        <AnimatedSubmissionButton
                            onPress={this.handleSubmit}
                            onSucceed={this.handleDismiss}
                            onFailed={()=>{}} />
                    </View>

                </ScrollView>
            </View>
        )
    }
}

const mapStateToProps = state => ({
    member: state.member || {},
    identityAccess: state.identityAccess || {},
})

const mapDispatchToProps = {
    onFormSubmit: offerRegistrationInvitation
}

export default connect(mapStateToProps, mapDispatchToProps)(RegistrationInvitation)