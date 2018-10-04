import React from 'react'
import PropTypes from 'prop-types'
import {
    Container, Content, Text, Body, ListItem, Form, Item, Label, Input, CheckBox, Button, View
} from 'native-base'
import Messages from '../Messages'
import Loading from '../Loading'
import Header from '../Header'
import Spacer from '../Spacer'
import { Colors, Fonts, Metrics } from '../../Themes/'

class UpdateLocation extends React.Component {
  static propTypes = {
    error: PropTypes.string,
    success: PropTypes.string,
    loading: PropTypes.bool.isRequired,
    onFormSubmit: PropTypes.func.isRequired,
    member: PropTypes.shape({
      latitude: PropTypes.number,
      longitude: PropTypes.number,
      streetAddress: PropTypes.string,
      city: PropTypes.string,
      stateProvince: PropTypes.string,
      postalCode: PropTypes.string,
      country: PropTypes.string,
      primaryTelephone: PropTypes.string,
      secondaryTelephone: PropTypes.string,
      contactName: PropTypes.string
    }).isRequired
  }

  static defaultProps = {
    error: null,
    success: null
  }

  constructor (props) {
    super(props)
    this.state = {
      latitude: 0,
      longitude: 0,
      streetAddress: '',
      city: '',
      stateProvince: '',
      postalCode: '',
      country: '',
      primaryTelephone: '',
      secondaryTelephone: '',
      contactName: ''
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
    onFormSubmit(this.state)
            .then(() => console.log('Location Updated'))
            .catch(e => console.log(`Error: ${e}`))
  }

  render () {
    const { loading, error, success } = this.props
    const {
            latitude,
            longitude,
            streetAddress,
            city,
            stateProvince,
            postalCode,
            country,
            primaryTelephone,
            secondaryTelephone,
            contactName
        } = this.state

        // Loading
    if (loading) return <Loading />

    return (
      <Container style={{backgroundColor: Colors.snow}}>
        <Content padder>
          <Header
            title='Business location'
            content='Thanks for keeping your business location up to date!'
                    />

          {error && <Messages message={error} />}
          {success && <Messages message={success} type='success' />}

          <Form>
            <Item stackedLabel>
              <Label>
                                Latitude
                </Label>
              <Input
                value={latitude}
                onChangeText={v => this.handleChange('latitude', v)}
                            />
            </Item>

            <Item stackedLabel>
              <Label>
                                Longitude
            </Label>
              <Input
                value={longitude}
                onChangeText={v => this.handleChange('longitude', v)}
                            />
            </Item>

            <Item stackedLabel>
              <Label>
                                Street
              </Label>
              <Input
                value={streetAddress}
                onChangeText={v => this.handleChange('streetAddress', v)}
                            />
            </Item>

            <Item stackedLabel>
              <Label>
                                City
              </Label>
              <Input
                value={city}
                onChangeText={v => this.handleChange('city', v)}
                            />
            </Item>

            <Item stackedLabel>
              <Label>
                                State/Province
              </Label>
              <Input
                value={stateProvince}
                onChangeText={v => this.handleChange('stateProvince', v)}
                            />
            </Item>

            <Item stackedLabel>
              <Label>
                                Postal code
              </Label>
              <Input
                value={postalCode}
                onChangeText={v => this.handleChange('postalCode', v)}
                            />
            </Item>

            <Item stackedLabel>
              <Label>
                                Country
              </Label>
              <Input
                value={country}
                onChangeText={v => this.handleChange('country', v)}
                            />
            </Item>

            <Item stackedLabel>
              <Label>
                                Primary telephone
              </Label>
              <Input
                value={primaryTelephone}
                onChangeText={v => this.handleChange('primaryTelephone', v)}
                            />
            </Item>

            <Item stackedLabel>
              <Label>
                                Secondary telephone
              </Label>
              <Input
                value={secondaryTelephone}
                onChangeText={v => this.handleChange('secondaryTelephone', v)}
                            />
            </Item>

            <Item stackedLabel>
              <Label>
                                Contact name
              </Label>
              <Input
                value={contactName}
                onChangeText={v => this.handleChange('contactName', v)}
                            />
            </Item>

            <Spacer size={20} />

            <Button block onPress={this.handleSubmit}>
              <Text>
                                Update Location
              </Text>
            </Button>
          </Form>
        </Content>
      </Container>
    )
  }
}

export default UpdateLocation
