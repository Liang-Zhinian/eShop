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
    View
} from 'native-base'
import Messages from '../../../Components/Messages'
import Loading from '../../../Components/Loading'
import Header from '../../../Components/Header'
import Spacer from '../../../Components/Spacer'
import { Colors } from '../../../Themes'
import OfferInvitationDescription from '../RegistrationInvitation';

class AddStaff extends React.Component {
    static propTypes = {
        error: PropTypes.string,
        success: PropTypes.string,
        loading: PropTypes.bool,
        onFormSubmit: PropTypes.func.isRequired,
        startCheckingMemberExistance: PropTypes.func.isRequired,
        memberCheckingStarted: PropTypes.bool.isRequired,
        memberCheckingDone: PropTypes.bool.isRequired,
        memberExistance: PropTypes.bool.isRequired,

        offerRegistrationInvitation: PropTypes.func.isRequired,
        offerRegistrationInvitationLoading: PropTypes.bool,
        registrationInvitation: PropTypes.string,
    }

    static defaultProps = {
        error: null,
        success: null
    }

    constructor(props) {
        super(props)
        this.state = {
            username: '',
            invitationDescription: '',

            showModal: false,
        }

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
        onFormSubmit({username: this.state.username, invitationDescription:this.state.invitationDescription})
            .then(() => console.log('Location Updated'))
            .catch(e => console.log(`Error: ${e}`))
    }

    render() {
        const { loading, error, success } = this.props

        // Loading
        if (loading) return <Loading />

        const {
            username,
            invitationDescription
        } = this.state

        const {
            memberExistance,
            memberCheckingStarted,
            memberCheckingDone,
            offerRegistrationInvitationLoading,
            registrationInvitation,
            offerRegistrationInvitation,

        } = this.props
        let icon = ''
        let iconActive = true
        if (memberCheckingStarted) {
            icon = 'spinner'
        } else if (memberCheckingDone) {
            icon = !memberExistance ? 'close' : 'checkmark'
        } else {
            iconActive = false
        }

        let iconType = memberCheckingStarted ? 'FontAwesome' : 'Ionicons'

        return (
            <Container style={{ backgroundColor: Colors.snow }}>
                <Content padder>
                    <Header
                        title='Contact'
                        content='Thanks for keeping your contact up to date!'
                    />

                    {error && <Messages message={error} />}
                    {success && <Messages message={success} type='success' />}

                    <Form>
                        <Item stackedLabel>
                            <Label>
                                User Name
                            </Label>
                            <Item>
                                <Input
                                    value={username}
                                    onChangeText={(v) => {
                                        this.handleChange('username', v)
                                        this.props.startCheckingMemberExistance(v)
                                    }}
                                />
                                {iconActive && <Icon type={iconType} active name={icon} />}
                            </Item>
                        </Item>

                        <Item stackedLabel>
                            <Label>
                                Invitation Description
                                </Label>
                            <TouchableOpacity onPress={() => { this.showModal() }}>
                                <Text style={{ fontSize: 12, color: 'blue' }}>Don't have an Invitation Description? Click to create one</Text>
                            </TouchableOpacity>
                            <Item>
                                <Input
                                    value={invitationDescription}
                                    onChangeText={(v) => this.handleChange('invitationDescription', v)}
                                />
                            </Item>
                        </Item>

                        <Spacer size={20} />

                        <Button block onPress={this.handleSubmit}>
                            <Text>
                                Save
                            </Text>
                        </Button>
                    </Form>
                    <Modal
                        animationType="slide"
                        visible={this.state.showModal}
                        onRequestClose={this.hideModal.bind(this)}>
                        <OfferInvitationDescription
                            onDismiss={this.hideModal.bind(this)}
                        />
                    </Modal>
                </Content>
            </Container>
        )
    }

    // modal
    showModal = () => {
        this.setState({ showModal: true })
    }

    hideModal = () => {
        this.setState({ showModal: false })
    }
}

export default AddStaff
