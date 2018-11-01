import React from 'react'
import Picker from '../../Components/Picker'
import Container from './StaffListingContainer'
import Layout from './Components/StaffListing'
import Staff_Lite from './Components/Staff_Lite'

export default class StaffPicker extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            id: null,
            value: props.value || 'Pick a Staff',
        }
    }

    render() {
        const {
            value,
        } = this.state

        return (
            <Picker
                ref={ref => this.picker = ref}
                onShow={this.onShow.bind(this)}
                onDismiss={this.onDismiss.bind(this)}
                value={value}
                title='Pick a Staff'
            // style={{ flex: 1 }}
            >
                <Container Layout={this.layout} />
            </Picker>
        )
    }

    layout = ({ loading, error, data, reFetch }) => {
        console.log('layout', { loading, error, data, reFetch })
        return (
            <Layout
                loading={loading}
                error={error}
                data={data}
                reFetch={reFetch}
                renderRow={this.renderRow.bind(this)}
            />
        )
    }

    renderRow = ({ item }) => {
        return (
            <Staff_Lite
                type={'talk'}
                name={item.FirstName + ' ' + item.LastName}
                avatarURL={item.ImageUri || ''}
                title={item.Bio}
                onPress={() => { this.setSelectedStaff(item) }}
            />
        )
    }

    onShow = () => {
    }

    onDismiss = () => {
    }

    dismissPicker = () => {
        this.picker.dismiss()
    }

    setSelectedStaff = (item) => {
        this.setState({ id: item.Id, value: item.FirstName + ' ' + item.LastName })
        this.props.onValueChanged && this.props.onValueChanged({ key: item.Id, value: item.FirstName + ' ' + item.LastName })
        this.dismissPicker()
    }
}
