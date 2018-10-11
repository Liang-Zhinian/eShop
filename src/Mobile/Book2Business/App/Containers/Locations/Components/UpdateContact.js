import React from 'react'
import PropTypes from 'prop-types'
import {
  Container, Content, Text, Body, ListItem, Form, Item, Label, Input, CheckBox, Button, View
} from 'native-base'
import Messages from '../../../Components/Messages'
import Loading from '../../../Components/Loading'
import Header from '../../../Components/Header'
import Spacer from '../../../Components/Spacer'
import { Colors } from '../../../Themes/'

class UpdateContact extends React.Component {
  static propTypes = {
    error: PropTypes.string,
    success: PropTypes.string,
    loading: PropTypes.bool,
    // onFormSubmit: PropTypes.func.isRequired,
    location: PropTypes.shape({
      name: PropTypes.string,
      description: PropTypes.string,
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
      primaryTelephone: props.location.ContactInformation.PrimaryTelephone,
      secondaryTelephone: props.location.ContactInformation.SecondaryTelephone,
      contactName: props.location.ContactInformation.ContactName
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
      primaryTelephone,
      secondaryTelephone,
      contactName
    } = this.state

    // Loading
    if (loading) return <Loading />

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
                Contact name
                            </Label>
              <Input
                value={contactName}
                onChangeText={v => this.handleChange('contactName', v)}
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

export default UpdateContact
