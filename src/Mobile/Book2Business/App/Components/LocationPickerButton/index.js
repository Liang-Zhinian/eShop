import React from 'react'
import { View, Modal } from 'react-native'
import RoundedButton from '../RoundedButton'
import LocationPickerScreen from './LocationPicker'
import { Colors, Fonts } from '../../Themes'

export default class LocationPickerButton extends React.Component {
  constructor (props) {
    super(props)
    this.state = {
      showModal: false
    }
  }

  toggleModal = () => {
    this.setState({ showModal: !this.state.showModal })
  }

  render () {
    return (
      <View>
        <RoundedButton style={{
          backgroundColor: Colors.purple,
          borderColor: Colors.purple
          // color: Colors.purple,
        }} onPress={this.toggleModal}>
          Pick a location
          </RoundedButton>
        <Modal
          style={{backgroundColor: Colors.transparent}}
          visible={this.state.showModal}
          onRequestClose={this.toggleModal}>
          <LocationPickerScreen screenProps={{
            toggle: this.toggleModal
          }} {...this.props} />

        </Modal>
      </View>
    )
  }
}
