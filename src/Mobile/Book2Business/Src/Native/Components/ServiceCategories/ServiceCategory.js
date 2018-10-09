import React from 'react'
import PropTypes from 'prop-types'
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
  Switch,
  Button,
  View,
  Left,
  Right
} from 'native-base'
import Messages from '../Messages'
import Loading from '../Loading'
import Header from '../Header'
import Spacer from '../Spacer'

class ServiceCategory extends React.Component {
  static propTypes = {
    error: PropTypes.string,
    success: PropTypes.string,
    loading: PropTypes.bool.isRequired,
    onFormSubmit: PropTypes.func.isRequired,
    member: PropTypes.shape({
      name: PropTypes.string,
      description: PropTypes.string,
      allowOnlineScheduling: PropTypes.bool
    }).isRequired
  }

  static defaultProps = {
    error: null,
    success: null
  }

  constructor (props) {
    super(props)
    this.state = {
      name: 0,
      longitude: '',
      description: '',
      allowOnlineScheduling: true
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
      name,
      description,
      allowOnlineScheduling
    } = this.state

    // Loading
    if (loading) return <Loading />

    return (
      <Container>
        <Content padder>
          <Header
            title='Service category'
            content='Thanks for keeping your service category up to date!'
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

            <Item stackedLabel noIndent>
              <Left>
                <Label>
                Allow online scheduling
              </Label></Left>
              <Right>
                <Switch value={allowOnlineScheduling} />
              </Right>
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

export default ServiceCategory
