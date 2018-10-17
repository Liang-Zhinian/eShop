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

const data = {
  Id: "9eeb8643-2e1a-4168-a5c4-fbf17162e3a6",
  Name: "Complimentary",
  Description: "Complimentary",
  AllowOnlineScheduling: true,
  ScheduleTypeId: 4,
  SiteId: "6879a31e-8936-414a-9d5f-abb4ee4a6bb8"
}

export default class AppointmentCategory extends React.Component {
  static propTypes = {
    error: PropTypes.string,
    success: PropTypes.string,
    loading: PropTypes.bool.isRequired,
    onFormSubmit: PropTypes.func.isRequired,
    appointmentCategory: PropTypes.shape({
      Id: PropTypes.string,
      Name: PropTypes.string,
      Description: PropTypes.string,
      AllowOnlineScheduling: PropTypes.bool,
      ScheduleTypeId: PropTypes.number,
      SiteId: PropTypes.string
    }).isRequired
  }

  constructor(props) {
    super(props)

    this.state = {
      Id: props.appointmentCategory.Id || '',
      Name: props.appointmentCategory.Name || '',
      Description: props.appointmentCategory.Description || '',
      AllowOnlineScheduling: props.appointmentCategory.AllowOnlineScheduling || true,
      ScheduleTypeId: props.appointmentCategory.ScheduleTypeId || 4,
      SiteId: props.appointmentCategory.SiteId || '',
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
      Name,
      Description,
      AllowOnlineScheduling,
      ScheduleTypeId,
      SiteId,
    } = this.state

    // Loading
    if (loading) return <Loading />

    return (
      <Container style={{ backgroundColor: 'white' }}>
        <Content padder>
          <Header
            title='Appointment category'
            content='Thanks for keeping your appointment category up to date!'
          />

          {error && <Messages message={error} />}
          {success && <Messages message={success} type='success' />}

          <Form>
            <Item stackedLabel>
              <Label>
                Name
                </Label>
              <Input
                value={Name}
                onChangeText={v => this.handleChange('Name', v)}
              />
            </Item>

            <Item stackedLabel>
              <Label>
                Description
            </Label>
              <Input
                value={Description}
                onChangeText={v => this.handleChange('Description', v)}
              />
            </Item>

            <Item fixedLabel style={{ height: 80 }}>
              <Label>
                AllowOnlineScheduling
              </Label>
              <Switch value={AllowOnlineScheduling} onValueChange={v => this.handleChange('AllowOnlineScheduling', v)} />
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