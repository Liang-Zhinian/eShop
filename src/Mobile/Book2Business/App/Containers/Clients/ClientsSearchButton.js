import React from 'react'
import { Text, View, Modal } from 'react-native'
import AnimatedTouchable from '../../Components/AnimatedTouchable'
import ClientsSearchScreen from './ClientsSearchScreen'
import { Colors, Fonts } from '../../Themes'

export default class ClientsSearchButton extends React.Component {
    constructor(props) {
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

    render() {
        return (
            <View>
                <AnimatedTouchable style={{
                    backgroundColor: Colors.purple,
                    borderColor: Colors.purple
                }} onPress={this.showModal}>
                    <Text>Seach</Text>
                </AnimatedTouchable>
                <Modal
                    style={{ backgroundColor: Colors.transparent }}
                    visible={this.state.showModal}
                    onRequestClose={this.hideModal}>
                    <ClientsSearchScreen screenProps={{
                        hideModal: this.hideModal
                    }} {...this.props} />

                </Modal>
            </View>
        )
    }
}
