import React from 'react'
import PropTypes from 'prop-types'
import {
  Container, Content, Text, Body, ListItem, Form, Item, Label, Input, CheckBox, Button, View, Switch
} from 'native-base'
import Messages from '../../../Components/Messages'
import Loading from '../../../Components/Loading'
import Header from '../../../Components/Header'
import Spacer from '../../../Components/Spacer'

class ServiceItem extends React.Component {
  static propTypes = {
    error: PropTypes.string,
    success: PropTypes.string,
    loading: PropTypes.bool.isRequired,
    onFormSubmit: PropTypes.func.isRequired,
    appointmentType: PropTypes.shape({
      Id: PropTypes.string,
      Name: PropTypes.string,
      Description: PropTypes.string,
      DefaultTimeLength: PropTypes.number,
      ServiceCategoryId: PropTypes.string,
      IndustryStandardCategoryName: PropTypes.string,
      IndustryStandardSubcategoryName: PropTypes.string,
      Price: PropTypes.number,
      AllowOnlineScheduling: PropTypes.bool,
      TaxRate: PropTypes.number,
      TaxAmount: PropTypes.number,
      SiteId: PropTypes.string
    }).isRequired
  }

  static defaultProps = {
    error: null,
    success: null
  }

  constructor(props) {
    super(props)
    this.state = {
      Id: props.appointmentType.Id || '',
      Name: props.appointmentType.Name || '',
      Description: props.appointmentType.Description || '',
      DefaultTimeLength: '' + (props.appointmentType.DefaultTimeLength || 0),
      ServiceCategoryId: props.appointmentType.ServiceCategoryId || '',
      IndustryStandardCategoryName: props.appointmentType.IndustryStandardCategoryName || '',
      IndustryStandardSubcategoryName: props.appointmentType.IndustryStandardSubcategoryName || '',
      Price: '' + (props.appointmentType.Price || 0),
      AllowOnlineScheduling: props.appointmentType.AllowOnlineScheduling || true,
      TaxRate: '' + (props.appointmentType.TaxRate || 0),
      TaxAmount: '' + (props.appointmentType.TaxAmount || 0),
      SiteId: props.appointmentType.SiteId || '',
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

  render() {
    const { loading, error, success } = this.props
    const {
      Id,
      Name,
      Description,
      DefaultTimeLength,
      ServiceCategoryId,
      IndustryStandardCategoryName,
      IndustryStandardSubcategoryName,
      Price,
      AllowOnlineScheduling,
      TaxRate,
      TaxAmount,
      SiteId,
    } = this.state

    // Loading
    if (loading) return <Loading />

    return (
      <Container style={{backgroundColor: 'white'}}>
        <Content padder>
          <Header
            title='Appointment type'
            content='Thanks for keeping your appointment type up to date!'
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

            <Item stackedLabel>
              <Label>
                DefaultTimeLength
              </Label>
              <Input
                keyboardType={'numeric'}
                value={DefaultTimeLength}
                onChangeText={v => this.handleChange('DefaultTimeLength', v)}
              />
            </Item>

            <Item stackedLabel>
              <Label>
                IndustryStandardCategoryName
              </Label>
              <Input
                value={IndustryStandardCategoryName}
                onChangeText={v => this.handleChange('IndustryStandardCategoryName', v)}
              />
            </Item>

            <Item stackedLabel>
              <Label>
                IndustryStandardSubcategoryName
              </Label>
              <Input
                value={IndustryStandardSubcategoryName}
                onChangeText={v => this.handleChange('IndustryStandardSubcategoryName', v)}
              />
            </Item>

            <Item fixedLabel style={{height: 80}}>
              <Label>
                AllowOnlineScheduling
              </Label>
              {/*<Input
                value={AllowOnlineScheduling}
                onChangeText={v => this.handleChange('AllowOnlineScheduling', v)}
              />*/}
              <Switch value={AllowOnlineScheduling} onValueChange={v => this.handleChange('AllowOnlineScheduling', v)} />
            </Item>

            <Item stackedLabel>
              <Label>
                Price
              </Label>
              <Input
                keyboardType={'numeric'}
                value={Price}
                onChangeText={v => this.handleChange('Price', v)}
              />
            </Item>

            <Item stackedLabel>
              <Label>
                TaxRate
              </Label>
              <Input
                keyboardType={'numeric'}
                value={TaxRate}
                onChangeText={v => this.handleChange('TaxRate', v)}
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

export default ServiceItem
