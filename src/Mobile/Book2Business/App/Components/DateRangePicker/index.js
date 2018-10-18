import React from 'react'
import {
    View,
    Modal,
    TouchableOpacity,
    ScrollView,
    Image,
    Text,
    Animated,
    LayoutAnimation,
    UIManager,
    Platform
} from 'react-native'
import { Container, Body } from 'native-base'
import PropTypes from 'prop-types'
import { format } from 'date-fns'
import { Colors, Fonts, Images, } from '../../Themes/'
import styles from './Styles'
import HorizontalCalendarList from '../HorizontalCalendarList'
import GradientHeader, { Header } from '../GradientHeader'
import { MinDate, DateFormat } from '../../Constants/date'

function equalDates(d1, d2) {
    return d1.toDateString() == d2.toDateString()
}

function formatText(markedDates, formatString = DateFormat) {
    if (equalDates(markedDates[0], markedDates[1])) {
        if (equalDates(markedDates[0], MinDate)) {
            return 'Ongoing'
        }
        else {
            return format(markedDates[0], formatString)
        }
    }

    return `${format(markedDates[0], formatString)} to ${format(markedDates[1], formatString)}`
}

export default class DateRangePicker extends React.Component {
    static propTypes = {
        onValueChanged: PropTypes.func.isRequired,
        initStartDate: PropTypes.instanceOf(Date).isRequired,
        initEndDate: PropTypes.instanceOf(Date).isRequired,
    }

    static defaultProps = {
        onValueChanged: (value) => { },
        initStartDate: new Date(),
        initEndDate: new Date(),
    }

    constructor(props) {
        super(props)
        this.state = {
            showModal: false,
            text: formatText([props.initStartDate, props.initEndDate]),
            showSingleDateOptions: false,
            showDateRangeOptions: false,
            scrollY: new Animated.Value(0),
            markedDates: [props.initStartDate, props.initEndDate],
            singleDateCalendarHeight: 0,
            dateRangeCalendarHeight: 0

        }
        this.startDaySaved = false

        if (Platform.OS === 'android') {
            UIManager.setLayoutAnimationEnabledExperimental(true);
        }
        this._bootStrap()
    }

    _bootStrap() {
    }

    render() {
        const {
            showSingleDateOptions,
            showDateRangeOptions,
            text,
            singleDateCalendarHeight,
            dateRangeCalendarHeight,

        } = this.state
        let markedDates = this.state.markedDates.map(d => (format(d, DateFormat)))

        const { event } = Animated
        return (
            <View>
                <TouchableOpacity onPress={this.showModal}>
                    <View style={styles.getRide}>
                        <Text style={styles.getRideLabel}>
                            {text}
                        </Text>
                        <Image
                            style={[styles.getRideIcon]}
                            source={Images.chevronRight}
                            esizeMode='cover'
                        />
                    </View>
                </TouchableOpacity>

                <Modal
                    animationType="slide"
                    visible={this.state.showModal}
                    onRequestClose={this.hideModal}>
                    <Container>
                        <GradientHeader>
                            <Header title='Select Date (Range)'
                                goBack={this.hideModal} />
                        </GradientHeader>
                        <Body>
                            <ScrollView
                                ref='scrolly'
                                onScroll={event([{ nativeEvent: { contentOffset: { y: this.state.scrollY } } }])}
                                scrollEventThrottle={10}
                                scrollEnabled={true}>
                                <View style={styles.container}>
                                    <TouchableOpacity activeOpacity={0.7}
                                        onPress={this.onPressOngoing.bind(this)}
                                        style={styles.TouchableOpacityStyle}>
                                        <View style={styles.getRide}>
                                            <Text style={[styles.getRideLabel, styles.TouchableOpacityTitleText]}>
                                                Ongoing
                            </Text>
                                        </View>
                                    </TouchableOpacity>
                                    <TouchableOpacity activeOpacity={0.7}
                                        onPress={() => this.toggleSingleDateCalendar()}
                                        style={styles.TouchableOpacityStyle}>
                                        <View style={styles.getRide}>
                                            <Text style={[styles.getRideLabel, styles.TouchableOpacityTitleText]}>
                                                Single date
                                </Text>
                                            <Image
                                                style={[styles.getRideIcon, showSingleDateOptions && styles.flip]}
                                                source={Images.chevronIcon}
                                            />
                                        </View>
                                    </TouchableOpacity>
                                    <View style={[styles.singleDateOptions, showSingleDateOptions && { height: singleDateCalendarHeight }]}>
                                        <View style={styles.singleDatePicker}>
                                            <HorizontalCalendarList
                                                onDayPress={this.onSingleDayPress.bind(this)}
                                                markedDates={[markedDates[0]]} />
                                        </View>
                                    </View>
                                    <TouchableOpacity activeOpacity={0.7}
                                        onPress={() => this.toggleDateRangeCalendar()}
                                        style={styles.TouchableOpacityStyle}>
                                        <View style={styles.getRide}>
                                            <Text style={[styles.getRideLabel, styles.TouchableOpacityTitleText]}>
                                                From & To
                                </Text>
                                            <Image
                                                style={[styles.getRideIcon, showDateRangeOptions && styles.flip]}
                                                source={Images.chevronIcon}
                                            />
                                        </View>
                                    </TouchableOpacity>
                                    <View style={[styles.dateRangeOptions, showDateRangeOptions && { height: dateRangeCalendarHeight }]}>
                                        <View style={styles.dateRangePicker}>
                                            <HorizontalCalendarList
                                                onDayPress={this.onDayRangePress.bind(this)}
                                                markedDates={markedDates} />
                                        </View>
                                    </View>

                                    <TouchableOpacity activeOpacity={0.7}
                                        onPress={this.hideModal}
                                        style={styles.TouchableOpacityStyle}>
                                        <View style={styles.getRide}>
                                            <Text style={[styles.getRideLabel, styles.TouchableOpacityTitleText]}>
                                                Cancel
                                            </Text>
                                        </View>
                                    </TouchableOpacity>
                                </View>
                            </ScrollView>
                        </Body>
                    </Container>
                </Modal>
            </View >
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

        LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut);

        this.setState({
            singleDateCalendarHeight: 370,
        });
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

        LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut);

        this.setState({
            dateRangeCalendarHeight: 370,
        });
    }

    onPressOngoing() {
        let markedDates = [MinDate, MinDate]
        this.setState({
            text: formatText(markedDates),
            markedDates,
        });
        this.hideModal();
        this.onValueReturned();
    }

    onSingleDayPress(day) {
        let markedDates = [new Date(day.dateString), new Date(day.dateString)]
        this.setState({
            text: formatText(markedDates),
            markedDates,
        });
        this.hideModal();
        this.onValueReturned();
    }

    onDayRangePress(day) {
        if (!this.startDaySaved) {
            this.setState({
                markedDates: [new Date(day.dateString), undefined]
            });
            this.startDaySaved = true
        } else {
            let markedDates = this.state.markedDates
            markedDates[1] = new Date(day.dateString)
            let date1 = markedDates[0]
            let date2 = markedDates[1]
            if (date1 > date2) {
                let d = markedDates[0]
                markedDates[0] = markedDates[1]
                markedDates[1] = d
            }
            this.setState({
                markedDates: markedDates,
                text: formatText(markedDates)
            });
            this.hideModal();
            this.onValueReturned();
        }
    }

    onValueReturned = () => {
        setTimeout(() => { this.props.onValueChanged && this.props.onValueChanged(this.state.markedDates) }, 500)
    }
}
