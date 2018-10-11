import React from 'react'
import {
    View,
    Modal,
    TouchableOpacity,
    ScrollView,
    Image,
    Text,
    Animated,
    LayoutAnimation
} from 'react-native'
import { Colors, Fonts, Images, } from '../../Themes/'
import styles from './Styles'
import HorizontalCalendarList from '../HorizontalCalendarList'

export default class ImageCropperButton extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            showModal: false,
            text: 'Ongoing',
            showSingleDateOptions: false,
            showDateRangeOptions: false,
            scrollY: new Animated.Value(0),
            markedDates: ['', '']
        }
        this.startDaySaved = false
        this.value = props.value || 'Ongoing'
    }

    render() {
        const { showSingleDateOptions, showDateRangeOptions, text } = this.state
        const { event } = Animated
        return (
            <View>
                <TouchableOpacity onPress={this.showModal}>
                    <View style={styles.getRide}>
                        <Text style={styles.getRideLabel}>
                            {text}
                        </Text>
                        <Image
                            style={[styles.getRideIcon, showSingleDateOptions && styles.flip]}
                            source={Images.chevronIcon}
                        />
                    </View>
                </TouchableOpacity>

                <Modal
                    style={{ backgroundColor: Colors.transparent }}
                    visible={this.state.showModal}
                    onRequestClose={this.hideModal}>
                    <ScrollView
                        ref='scrolly'
                        onScroll={event([{ nativeEvent: { contentOffset: { y: this.state.scrollY } } }])}
                        scrollEventThrottle={10}
                        scrollEnabled={true}>
                        <View style={styles.container}>
                            <TouchableOpacity onPress={this.onPressOngoing.bind(this)}>
                                <Text style={styles.getRideLabel}>
                                    Ongoing
                            </Text>
                            </TouchableOpacity>
                            <TouchableOpacity onPress={() => this.toggleSingleDateCalendar()}>
                                <View style={styles.getRide}>
                                    <Text style={styles.getRideLabel}>
                                        Single date
                                </Text>
                                    <Image
                                        style={[styles.getRideIcon, showSingleDateOptions && styles.flip]}
                                        source={Images.chevronIcon}
                                    />
                                </View>
                            </TouchableOpacity>
                            <View style={[styles.singleDateOptions, showSingleDateOptions && { height: 370 }]}>
                                <View style={styles.singleDatePicker}>
                                    <HorizontalCalendarList
                                        onDayPress={this.onSingleDayPress.bind(this)}
                                        markedDates={this.state.markedDates} />
                                </View>
                            </View>
                            <TouchableOpacity onPress={() => this.toggleDateRangeCalendar()}>
                                <View style={styles.getRide}>
                                    <Text style={styles.getRideLabel}>
                                        From & To
                                </Text>
                                    <Image
                                        style={[styles.getRideIcon, showDateRangeOptions && styles.flip]}
                                        source={Images.chevronIcon}
                                    />
                                </View>
                            </TouchableOpacity>
                            <View style={[styles.dateRangeOptions, showDateRangeOptions && { height: 270 }]}>
                                <View style={styles.dateRangePicker}>
                                    <HorizontalCalendarList
                                        onDayPress={this.onDayRangePress.bind(this)}
                                        markedDates={this.state.markedDates} />
                                </View>
                            </View>
                            <TouchableOpacity onPress={this.hideModal}>
                                <View style={styles.getRide}>
                                    <Text style={styles.getRideLabel}>
                                        Cancel
                                    </Text>
                                </View>
                            </TouchableOpacity>
                        </View>
                    </ScrollView>
                </Modal>
            </View>
        )
    }

    showModal = () => {
        this.setState({ showModal: true })
        this.startDaySaved = false
    }

    hideModal = () => {
        this.setState({ showModal: false })
        this.startDaySaved = false
    }

    toggleSingleDateCalendar = () => {
        const { showSingleDateOptions, scrollY } = this.state
        // if (!showSingleDateOptions && scrollY._value < 200) {
        //     this.refs.scrolly.scrollTo({ x: 0, y: 200, animated: true })
        // }
        this.setState({
            showDateRangeOptions: false,
            showSingleDateOptions: !this.state.showSingleDateOptions,
        })
    }

    toggleDateRangeCalendar = () => {
        const { showDateRangeOptions, scrollY } = this.state
        // if (!showSingleDateOptions && scrollY._value < 200) {
        //     this.refs.scrolly.scrollTo({ x: 0, y: 200, animated: true })
        // }
        this.setState({
            showDateRangeOptions: !this.state.showDateRangeOptions,
            showSingleDateOptions: false,
        })
    }

    onPressOngoing() {
        this.setState({
            text: 'Ongoing',
            markedDates: null,
        });
        this.value = 'Ongoing'
        this.hideModal();
        this.onValueReturned();
    }

    onSingleDayPress(day) {
        this.setState({
            text: day.dateString,
            markedDates: [day.dateString],
        });
        this.value = day.dateString
        this.hideModal();
        this.onValueReturned();
    }

    onDayRangePress(day) {
        if (!this.startDaySaved) {
            this.setState({
                markedDates: [day.dateString, '']
            });
            this.startDaySaved = true
        } else {
            let markedDates = this.state.markedDates
            markedDates[1] = day.dateString
            let date1 = new Date(markedDates[0])
            let date2 = new Date(markedDates[1])
            if (date1 > date2) {
                let d = markedDates[0]
                markedDates[0] = markedDates[1]
                markedDates[1] = d
            }
            this.setState({
                markedDates: markedDates,
                text: `${markedDates[0]} to ${markedDates[1]}`
            });
            this.value = markedDates
            this.hideModal();
            this.onValueReturned();
        }
    }

    onValueReturned = () => {
        setTimeout(() => { this.props.onValueChanged && this.props.onValueChanged(this.value) }, 500)
    }
}
