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

  showModal = () => {
    this.setState({ showModal: true })
  }

  hideModal = () => {
    this.setState({ showModal: false })
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
        }} onPress={this.showModal}>
          Pick a location
          </RoundedButton>
        <Modal
          style={{backgroundColor: Colors.transparent}}
          visible={this.state.showModal}
          onRequestClose={this.hideModal}>
          <LocationPickerScreen screenProps={{
            hideModal: this.hideModal
          }} {...this.props} />

        </Modal>
      </View>
    )
  }
}
