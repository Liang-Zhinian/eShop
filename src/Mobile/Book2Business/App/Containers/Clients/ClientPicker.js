import React from 'react'
import { Text, View, Modal } from 'react-native'
import AnimatedTouchable from '../../Components/AnimatedTouchable'
import ClientsSearchScreen from './ClientsSearchScreen'
import { Colors, Fonts } from '../../Themes'
import Picker from '../../Components/Picker'

export default class ClientPicker extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            value: props.value || 'Pick a Client',
        }
    }

    render() {
        const {
            value,
        } = this.state

        return (
            <View>
                <Picker
                    ref={ref => this.picker = ref}
                    onShow={this.onShow.bind(this)}
                    onDismiss={this.onDismiss.bind(this)}
                    value={value}
                    title='Pick a Client'
                    // style={{ flex: 1 }}
                    scrollEnabled={false}
                >
                    <ClientsSearchScreen screenProps={{
                        hideModal: this.hideModal
                    }} {...this.props} />
                </Picker>
            </View>
        )
    }

    onShow = () => {
    }

    onDismiss = () => {
    }

    dismissPicker = () => {
        this.picker.hideModal()
    }
}
