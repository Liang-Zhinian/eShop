import React from 'react'
import {
    Text,
    UIManager,
    Platform
} from 'react-native'
import PropTypes from 'prop-types'

import Accordion from '../../../Components/Accordion'
import Picker from '../../../Components/Picker'


const dataArray = [
    { title: "First Element", content: "Lorem ipsum dolor sit amet" },
    { title: "Second Element", content: "Lorem ipsum dolor sit amet" },
    { title: "Third Element", content: "Lorem ipsum dolor sit amet" }
];

export default class AppointmentTypePicker extends React.Component {
    static propTypes = {
        onValueChanged: PropTypes.func.isRequired,
    }

    static defaultProps = {
        onValueChanged: (value) => { },
    }

    constructor(props) {
        super(props)
        this.state = {
            value: props.value || 'Appointment Type',
            data: dataArray
        }

        if (Platform.OS === 'android') {
            UIManager.setLayoutAnimationEnabledExperimental(true);
        }
        this._bootStrapAsync()
    }

    async _bootStrapAsync() {
    }

    render() {
        const {
            value,
            data
        } = this.state

        return (
            <Picker
                ref={ref => this.picker = ref}
                onShow={this.onShow.bind(this)}
                onDismiss={this.onDismiss.bind(this)}
                value={value}
                title='Pick an Appointment Type'
            >
                <Accordion
                    data={data}
                    // renderTitle={({item, index})=>{}}
                    renderItem={({ item, index }) => {
                        return (
                            <Text>{item.content}</Text>
                        )
                    }}
                    icon="add"
                    expandedIcon="remove" />
            </Picker>
        )
    }

    onShow = () => {
    }

    onDismiss = () => {
    }

    dismissPicker = () => {
        this.picker.hideModal()
    }

    onPress(value) {
        this.setState({
            value
        });
        this.dismissPicker();
        this.onValueReturned();
    }

    onValueReturned = () => {
        setTimeout(() => { this.props.onValueChanged && this.props.onValueChanged(this.state.value) }, 500)
    }
}
