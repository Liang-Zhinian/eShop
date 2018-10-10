import React from 'react'
import { StyleSheet } from 'react-native'
import PropTypes from 'prop-types'
import {
    Container, Content, Text, Body, ListItem, Form, Item, Label, Input, CheckBox, Button, View
} from 'native-base'
import { connect } from 'react-redux'
import Messages from '../Messages'
import Loading from '../Loading'
import Header from '../Header'
import Spacer from '../Spacer'
import { Colors, Fonts, Metrics } from '../../Themes/'
import LocationPickerButton from './LocationPickerButton'


class UpdateGeolocation extends React.Component {
  static propTypes = {
    error: PropTypes.string,
    success: PropTypes.string,
    loading: PropTypes.bool,
    onFormSubmit: PropTypes.func.isRequired,
    location: PropTypes.shape({
      Name: PropTypes.string,
      Geolocation: PropTypes.shape({
        Latitude: PropTypes.number,
        Longitude: PropTypes.number
      })
    }).isRequired
  }

  static defaultProps = {
    error: null,
    success: null
  }

  constructor (props) {
    super(props)
    this.state = {
      latitude: '' + props.location.Geolocation.Latitude,
      longitude: '' + props.location.Geolocation.Longitude
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
    const { loading, error, success, location } = this.props

    const {
            latitude,
            longitude
        } = this.state

        // Loading
    if (loading) return <Loading />

    return (
      <Container style={{ backgroundColor: Colors.snow }}>
        <Content padder>
          <Header
            title='Geolocation'
            content='Thanks for keeping your geolocation up to date!'
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
            <Spacer size={20} />

            <LocationPickerButton
              mapViewMode={false}
              scrollEnabled
              style={[styles.map]}
              initialRegion={{ title: location.Name, latitude, longitude, latitudeDelta: 0.05, longitudeDelta: 0.01 }}
              locations={[{ title: location.Name, latitude, longitude }]}
              handePickButton={({location}) => {
                this.setState({
                  latitude: '' + location.latitude,
                  longitude: '' + location.longitude
                })
              }} />

            {/* <View ref='mapContainer' >
                            <LocationPicker
                                mapViewMode={false}
                                scrollEnabled={true}
                                style={[styles.map,]}
                                initialRegion={{ title: location.Name, latitude, longitude, latitudeDelta: 0.05, longitudeDelta: 0.01 }}
                                locations={[{ title: location.Name, latitude, longitude }, { title: 'Chanel@Unkown', "latitude":22.3158808,"longitude":114.1625219 }]} />
                        </View> */}

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

export default UpdateGeolocation

const styles = StyleSheet.create({

  map: {
    width: '100%',
    height: 180,
    zIndex: 2
  }
})
