import React from 'react'
import PropTypes from 'prop-types'
import { TouchableOpacity, Modal } from 'react-native';
import {
    Container,
    Content,
    Text,
    Body,
    ListItem,
    Form,
    Item,
    Label,
    Input,
    Icon,
    Button,
    View,
    Footer
} from 'native-base'

import Messages from '../../../Components/Messages'
import Loading from '../../../Components/Loading'
// import Header from '../../../Components/Header'
import Spacer from '../../../Components/Spacer'
import GradientHeader, { Header } from '../../../Components/GradientHeader'
import { Colors } from '../../../Themes'
import AnimatedSubmissionButton from '../../../Components/AnimatedSubmissionButton'

export default class RegistrationInvitation extends React.Component {
    static PropTypes = {
        offerRegistrationInvitation: PropTypes.func
    }

    constructor(props) {
        super(props)
        this.state = {
            invitationDescription: ''
        }

        this.handleChange = this.handleChange.bind(this)
        this.handleSubmit = this.handleSubmit.bind(this)
        this.handleCancel = this.handleCancel.bind(this)
    }

    handleChange = (name, val) => {
        this.setState({
            [name]: val
        })
    }

    handleSubmit = () => {
        const { offerRegistrationInvitation } = this.props
        offerRegistrationInvitation && offerRegistrationInvitation(this.state.invitationDescription)
    }

    handleCancel = () => {
        this.props.cancel()
    }

    render = () => {
        const { invitationDescription } = this.state
        return (
            <Container style={{ backgroundColor: Colors.snow }}>
                <GradientHeader>
                    <Header title='Off Registration Invitation'
                        goBack={this.handleCancel} />
                </GradientHeader>
                <Content padder>
                    <Form>
                        <Item stackedLabel>
                            <Label>
                                Invitation Description
                    </Label>
                            <Input
                                value={invitationDescription}
                                onChangeText={(v) => this.handleChange('invitationDescription', v)}
                            />
                        </Item>

                        <Spacer size={20} />
                    </Form>
                    {/* <Button block onPress={this.handleSubmit}>
                            <Text>
                                Save
                        </Text>
                        </Button> */}
                    <View>
                        <AnimatedSubmissionButton />
                    </View>

                </Content>
            </Container>
        )
    }
}