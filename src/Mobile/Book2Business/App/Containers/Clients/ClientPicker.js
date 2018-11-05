import React from 'react'
import { Text, View, Modal } from 'react-native'
import AnimatedTouchable from '../../Components/AnimatedTouchable'
import ClientsSearchScreen from './ClientsSearchScreen'
import { Colors, Fonts } from '../../Themes'
import Picker from '../../Components/Picker'
import Client from './Components/Client_Lite'

export default class ClientPicker extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            value: props.value || 'Pick a Client',
        }

        this.onShow = this.onShow.bind(this)
        this.onDismiss = this.onDismiss.bind(this)
        this.showPicker = this.showPicker.bind(this)
        this.dismissPicker = this.dismissPicker.bind(this)
    }

    render() {
        const {
            value,
        } = this.state

        const {
            onValueChanged,
            isVisible,
            ...otherProps
        } = this.props

        return (
            <View style={{flex: 1}}>
                <Picker
                    ref={ref => this.picker = ref}
                    onShow={this.onShow.bind(this)}
                    onDismiss={this.onDismiss.bind(this)}
                    value={value}
                    title='Pick a Client'
                    isVisible={isVisible}
                    // style={{ flex: 1 }}
                    scrollEnabled={false}
                    touchableComponent={typeof value == 'string' ? null : (
                        <Client name={value.Name + ' ' + value.LastName}
                            avatarURL={value.AvatarImageUri || ''}
                            title={value.Email}
                            onPress={this.showPicker.bind(this)} />
                    )}
                >
                    <ClientsSearchScreen screenProps={{
                        hideModal: this.hideModal
                    }}
                        onValueChanged={({ key, value }) => {
                            this.setState({ value })
                            onValueChanged({ key, value })
                            this.dismissPicker()
                        }}
                        {...otherProps}
                    />
                </Picker>
            </View>
        )
    }

    onShow = () => {
    }

    onDismiss = () => {
    }

    showPicker = () => {
        this.picker.show()
    }

    dismissPicker = () => {
        this.picker.dismiss()
    }
}
