import React from 'react'
import PropTypes from 'prop-types'
import {
    Container,
    Content,
    Text,
    Header,
    Form,
    Item,
    Label,
    Input,
    Button,
    Switch
} from 'native-base'

import Messages from '../../../Components/Messages'
import Loading from '../../../Components/Loading'
import Spacer from '../../../Components/Spacer'
import ClientsSearchButton from '../../Clients/ClientsSearchButton';
import AppointmentTypePicker from '../../AppointmentCatalog/Components/AppointmentTypePicker'

const data = {
    Id: "9eeb8643-2e1a-4168-a5c4-fbf17162e3a6",
    OrderDate: new Date(),
    StartDateTime: new Date(),
    EndDateTime: new Date(),
    StaffId: "6879a31e-8936-414a-9d5f-abb4ee4a6bb8",
    LocationId: "6879a31e-8936-414a-9d5f-abb4ee4a6bb8",
    SiteId: "6879a31e-8936-414a-9d5f-abb4ee4a6bb8",
    GenderPreference: 'Male',
    Duration: 60,
    AppointmentStatusId: 1,
    Notes: '',
    StaffRequested: true,
    ClientId: "6879a31e-8936-414a-9d5f-abb4ee4a6bb8",
    FirstAppointment: false,
}

export default class AppointmentCategory extends React.Component {
    static propTypes = {
        error: PropTypes.string,
        success: PropTypes.string,
        loading: PropTypes.bool.isRequired,
        onFormSubmit: PropTypes.func.isRequired,
        appointmentSchedule: PropTypes.shape({
            Id: String,
            OrderDate: Date,
            StartDateTime: Date,
            EndDateTime: Date,
            StaffId: String,
            LocationId: String,
            SiteId: String,
            GenderPreference: String,
            Duration: Number,
            AppointmentStatusId: Number,
            Notes: String,
            StaffRequested: Boolean,
            ClientId: String,
            FirstAppointment: Boolean,
        }).isRequired
    }

    constructor(props) {
        super(props)

        this.state = {
            Id: props.appointmentSchedule.Id || '',
            OrderDate: props.appointmentSchedule.OrderDate || new Date(),
            StartDateTime: props.appointmentSchedule.StartDateTime || new Date(),
            EndDateTime: props.appointmentSchedule.EndDateTime || new Date(),
            StaffId: props.appointmentSchedule.StaffId || '',
            LocationId: props.appointmentSchedule.LocationId || '',
            SiteId: props.appointmentSchedule.SiteId || '',
            GenderPreference: props.appointmentSchedule.GenderPreference || 'Male',
            Duration: props.appointmentSchedule.Duration || 60,
            AppointmentStatusId: props.appointmentSchedule.AppointmentStatusId || 1,
            Notes: props.appointmentSchedule.Notes || '',
            StaffRequested: props.appointmentSchedule.StaffRequested || true,
            ClientId: props.appointmentSchedule.ClientId || '',
            FirstAppointment: props.appointmentSchedule.FirstAppointment || false,
        }

        this.handleChange = this.handleChange.bind(this)
        this.handleSubmit = this.handleSubmit.bind(this)

        this._bootStrapAsync()
    }

    async _bootStrapAsync() {

    }

    handleChange = (name, val) => {
        this.setState({
            [name]: val
        })
    }

    handleSubmit = () => {
        const { onFormSubmit } = this.props
        onFormSubmit && onFormSubmit(this.state)
            .then(() => console.log('Appointment Type Updated'))
            .catch(e => console.log(`Error: ${e}`))
    }

    render = () => {
        const { loading, error, success } = this.props
        const {
            Id,
            OrderDate,
            StartDateTime,
            EndDateTime,
            StaffId,
            LocationId,
            SiteId,
            GenderPreference,
            Duration,
            AppointmentStatusId,
            Notes,
            StaffRequested,
            ClientId,
            FirstAppointment,
        } = this.state

        // Loading
        if (loading) return <Loading />

        return (
            <Container style={{ backgroundColor: 'white' }}>
                <Content padder>
                    <Header
                        title='Make an Appointment'
                        content='Thanks for keeping your appointment up to date!'
                    />

                    {error && <Messages message={error} />}
                    {success && <Messages message={success} type='success' />}

                    <Form>
                        <Item stackedLabel>
                            <Label>
                                Client
                            </Label>
                            <ClientsSearchButton />
                        </Item>
                        <Item stackedLabel>
                            <Label>
                                Appointment Type
                            </Label>
                            <AppointmentTypePicker />
                        </Item>

                        <Spacer size={20} />

                        <Button block onPress={this.handleSubmit}>
                            <Text>
                                Save
              </Text>
                        </Button>
                    </Form>
                </Content>
            </Container>
        )
    }
}