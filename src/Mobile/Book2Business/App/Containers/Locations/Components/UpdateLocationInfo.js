import React from 'react'
import PropTypes from 'prop-types'
import {
    Container, Content, Text, Body, ListItem, Form, Item, Label, Input, CheckBox, Button, View
} from 'native-base'
import Messages from '../../../Components/Messages'
import Loading from '../../../Components/Loading'
import Header from '../../../Components/Header'
import Spacer from '../../../Components/Spacer'
import { Colors, Fonts, Metrics } from '../../../Themes/'

class UpdateLocationInfo extends React.Component {
  static propTypes = {
    error: PropTypes.string,
    success: PropTypes.string,
    loading: PropTypes.bool,
        // onFormSubmit: PropTypes.func.isRequired,
    location: PropTypes.shape({
      name: PropTypes.string,
      description: PropTypes.string
    }).isRequired
  }

  static defaultProps = {
    error: null,
    success: null
  }

  constructor (props) {
    super(props)
    this.state = {
      name: props.location.Name,
      description: props.location.Description
    }

    this.handleChange = this.handleChange.bind(this)
    this.handleSubmit = this.handleSubmit.bind(this)

        // this.init();
  }

  init = () => {

    this.props.getLocation('', '')
        .then(res => {
        })
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
            name,
            description
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
                                Name
                </Label>
              <Input
                value={name}
                onChangeText={v => this.handleChange('name', v)}
                            />
            </Item>

            <Item stackedLabel>
              <Label>
                                Description
            </Label>
              <Input
                value={description}
                onChangeText={v => this.handleChange('description', v)}
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

export default UpdateLocationInfo
