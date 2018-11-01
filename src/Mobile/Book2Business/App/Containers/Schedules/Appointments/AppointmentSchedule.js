import React from 'react'
import {
    TextInput,
    StyleSheet
} from 'react-native'
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
    Switch,
    View
} from 'native-base'
import {
    format,
    addMinutes
} from 'date-fns'

import { TimeFormat } from '../../../Constants/date'
import Messages from '../../../Components/Messages'
import Loading from '../../../Components/Loading'
import Spacer from '../../../Components/Spacer'

import StaffPicker from '../../Staffs/StaffPicker';
import ClientPicker from '../../Clients/ClientPicker';
import AppointmentTypePicker from '../../AppointmentCatalog/Components/AppointmentTypePicker'
import { DateTimePickerButton } from '../../../Components/DateTimePicker'
import Styles from '../../../Components/AnimatedContainerWithNavbar/Styles';

function formatTime(date) {
    return `${format(date, TimeFormat)}`
}


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

export default class AppointmentSchedule extends React.Component {
    static propTypes = {
        error: PropTypes.string,
        success: PropTypes.string,
        loading: PropTypes.bool.isRequired,
        onFormSubmit: PropTypes.func.isRequired,
        appointmentSchedule: PropTypes.shape({
            Id: PropTypes.string,
            OrderDate: PropTypes.instanceOf(Date),
            StartDateTime: PropTypes.instanceOf(Date),
            EndDateTime: PropTypes.instanceOf(Date),
            StaffId: PropTypes.string,
            LocationId: PropTypes.string,
            SiteId: PropTypes.string,
            GenderPreference: PropTypes.string,
            Duration: PropTypes.number,
            AppointmentStatusId: PropTypes.number,
            Notes: PropTypes.string,
            StaffRequested: PropTypes.bool,
            ClientId: PropTypes.string,
            FirstAppointment: PropTypes.bool,
            AppointmentServiceItems: PropTypes.arrayOf(PropTypes.shape({})),
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
            AppointmentServiceItems: props.appointmentSchedule.AppointmentServiceItems || [],
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

    getDuration = (selectedAppointmentTypes) => {
        if (selectedAppointmentTypes && selectedAppointmentTypes.length > 0) {
            return selectedAppointmentTypes[0].DefaultTimeLength
        }
        return 0
    }

    setBookingDateTime = (date) => {
        const {
            AppointmentServiceItems,
            StartDateTime,
        } = this.state

        if (!date) date = StartDateTime
        let datetime = new Date(date)
        const duration = this.getDuration(AppointmentServiceItems)
        datetime = addMinutes(datetime, duration)

        this.setState({ StartDateTime: date, EndDateTime: datetime })
    }

    getEndTime = () => {
        const {
            EndDateTime,
        } = this.state

        return formatTime(EndDateTime)
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
            AppointmentServiceItems
        } = this.state

        console.log(this.state)
        // Loading
        if (loading) return <Loading />

        return (
            <Container style={{ backgroundColor: 'white' }}>
                <Content padder>

                    {error && <Messages message={error} />}
                    {success && <Messages message={success} type='success' />}

                    <Form>
                        <Item fixedLabel>
                            <Label>
                                Client
                            </Label>
                            <ClientPicker />
                        </Item>
                        <Item fixedLabel>
                            <Label>
                                Appointment Type
                            </Label>
                            <AppointmentTypePicker onValueChanged={({ value }) => {
                                this.setState({ AppointmentServiceItems: [value] }, () => {
                                    this.setBookingDateTime(StartDateTime)
                                })
                            }} />
                        </Item>
                        <Item fixedLabel>
                            <Label>
                                Staff
                            </Label>
                            <StaffPicker onValueChanged={({ key, value }) => {
                                this.setState({ StaffId: key })
                            }} />
                        </Item>
                        <Item fixedLabel>
                            <Label>
                                Date
                            </Label>
                            <DateTimePickerButton
                                mode='date'
                                initialDate={new Date()}
                                onValueChanged={({ value }) => {
                                    this.setBookingDateTime(value)
                                }} />
                        </Item>
                        <Item fixedLabel>
                            <Label>
                                Start Time
                            </Label>
                            <DateTimePickerButton
                                mode='time'
                                minuteInterval={15}
                                initialDate={new Date()}
                                onValueChanged={({ value }) => {
                                    this.setBookingDateTime(value)
                                }} />
                        </Item>
                        <Item fixedLabel>
                            <Label>
                                End Time
                            </Label>
                            <View>
                                <Text style={{ paddingVertical: 16, }}>
                                    {this.getEndTime()}
                                </Text>
                            </View>
                        </Item>
                        <Item fixedLabel>
                            <Label>
                                Duration
                            </Label>
                            <View>
                                <Text style={{ paddingVertical: 16, }}>
                                    {this.getDuration(AppointmentServiceItems) + ' mins'}
                                </Text>
                            </View>
                        </Item>

                        <Item fixedLabel>
                            <Label>
                                Resource
                            </Label>
                            <View>
                                <Text style={{ paddingVertical: 16, }}>

                                </Text>
                            </View>
                        </Item>

                        <Item>
                            <TextInput
                                multiline={true}
                                numberOfLines={20}
                                placeholder='Appointment notes'
                                style={styles.notes}
                                onChangeText={value => {
                                    this.setState({ Notes: value })
                                }} />
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

const styles = StyleSheet.create({
    notes: {
        flex: 1, 
        padding: 6, 
        marginVertical: 16, 
        height: 150, 
        borderWidth: 1, 
        borderColor: 'gray'
    }
})