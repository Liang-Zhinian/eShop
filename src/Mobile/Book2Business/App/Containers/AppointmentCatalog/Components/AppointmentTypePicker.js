import React from 'react'
import {
    Text,
    UIManager,
    Platform,
    View,
    StyleSheet
} from 'react-native'
import PropTypes from 'prop-types'

import { Colors, Metrics } from '../../../Themes/'
import AnimatedTouchable from '../../../Components/AnimatedTouchable'
import Picker from '../../../Components/Picker'
import Container from '../AppointmentTypeListingContainer'
import Layout from './AppointmentTypeListing'
import List from '../../../Components/List'

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
            id: null,
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
                // style={{ flex: 1 }}
            >
                <Container Layout={this.layout} />
            </Picker>
        )
    }

    onShow = () => {
    }

    onDismiss = () => {
    }

    dismissPicker = () => {
        this.picker.dismiss()
    }

    layout = ({ loading, error, data, refresh, loadChildren }) => {
        return (
            <Layout
                loading={loading}
                error={error}
                data={data}
                refresh={refresh}
                titleExtractor={(item) => item.Name}
                renderRow={this.renderRow}
                loadAppointmentTypes={loadChildren}
            />
        )
    }

    renderTitle = ({ item }) => {
        return <Text>{item.Name}</Text>
    }

    renderRow = ({ item }) => {
        // console.log(item)
        if (!item.AppointmentTypes) return <View><Text>{item.Description}</Text></View>

        return (
            <List
                headerTitle='Appointment types'
                data={item.AppointmentTypes}
                renderItem={({ item, index }) => (
                    <AnimatedTouchable onPress={() => this.setSelectedAppointmentType(item)}
                        style={{
                            flex: 1,
                            // flexDirection: 'row',
                            //height: 100,
                            // justifyContent: 'center',
                            // alignItems: 'center',
                            borderBottomWidth: StyleSheet.hairlineWidth,
                            borderBottomColor: 'gray'
                        }}>
                        <View style={[styles.infoText, { marginHorizontal: 5, paddingVertical: 10 }]}>
                            <Text style={styles.name}>{item.Name}</Text>
                            <Text style={styles.title}>{item.Description}</Text>
                        </View>
                    </AnimatedTouchable>
                )}
                keyExtractor={(item, index) => item.Id}
                contentContainerStyle={styles.listContent}
                showsVerticalScrollIndicator={false}
            // refresh={this.onRefresh}
            // loadMore={this.onNextPage.bind(this)}
            // refreshing={this.state.reFetchingStatus}
            // loadingMore={this.state.fetchingNextPageStatus}
            // error={appointmentCategories.error}
            // loading={appointmentCategories.loading}
            />
        );
    }
    
    setSelectedAppointmentType = (item) => {
        this.setState({ id: item.Id, value: item.Name })
        this.props.onValueChanged && this.props.onValueChanged({ value: item })
        this.dismissPicker()
    }
}

const styles = StyleSheet.create({

    listContent: {
        paddingTop: 0, //Metrics.baseMargin,
        paddingBottom: 0, //Metrics.baseMargin * 8
    },
    infoText: {
        flex: 1,
        paddingRight: Metrics.doubleBaseMargin
    },
    title: {
        fontFamily: 'Montserrat-SemiBold',
        fontSize: 17,
        color: Colors.darkPurple,
        letterSpacing: 0
    },
    name: {
        fontFamily: 'Montserrat-Light',
        fontSize: 13,
        color: Colors.lightText,
        letterSpacing: 0,
        lineHeight: 18
    },
})
