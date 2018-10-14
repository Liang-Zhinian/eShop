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
    Dimensions
} from 'react-native'
import PropTypes from 'prop-types'
import { format } from 'date-fns'

import { Colors, Fonts, Images, } from '../../Themes/'
import styles from './Styles'
import { TimePickerDialog } from './TimePicker';
import { TimeFormat } from '../../Constants/date'

const { width: windowWidth, height: windowHeight } = Dimensions.get('window')

export default class TimeRangePicker extends React.Component {
    static propTypes = {
        // value: PropTypes.string,
        onValueChanged: PropTypes.func.isRequired,
        initStartDate: PropTypes.instanceOf(Date),
        initEndDate: PropTypes.instanceOf(Date),
        width: PropTypes.number,
        height: PropTypes.number,
    }

    static defaultProps = {
        // value: '08:00 to 20:00',
        onValueChanged: (value) => { },
        initStartDate: new Date(),
        initEndDate: new Date(),
        width: 0.8 * windowWidth,
        height: 0.5 * windowHeight,
    }

    constructor(props) {
        super(props)
        this.state = {
            showModal: false,
            text: this.formatText([props.initStartDate, props.initEndDate]),
            showFromTimeOptions: false,
            showToTimeOptions: false,
            scrollY: new Animated.Value(0),
            markedDates: [null, null],
            fromTimeSaved: false,
            animation: {
                fromTimeDialogWidth: new Animated.Value(props.width),
                fromTimeDialogHeight: new Animated.Value(props.height),
                toTimeDialogWidth: new Animated.Value(0),
                toTimeDialogHeight: new Animated.Value(0),
            },
            initStartDate: props.initStartDate,
            initEndDate: props.initEndDate,
        }
    }

    toggleModal = () => {
        this.setState({ showModal: !this.state.showModal })
    }

    render() {
        const {
            text,
            fromTimeSaved,
            initStartDate,
            initEndDate,
            animation
        } = this.state

        return (
            <View
                style={{
                    backgroundColor: Colors.transparent,
                }}>
                <TouchableOpacity onPress={this.showModal}>
                    <View style={styles.getRide}>
                        <Text style={styles.getRideLabel}>
                            {text}
                        </Text>
                        <Image
                            style={[styles.getRideIcon]}
                            source={Images.chevronRight}
                            resizeMode='cover'
                        />
                    </View>
                </TouchableOpacity>
                <Modal
                    animationType="slide"
                    transparent={true}
                    visible={this.state.showModal}
                    onRequestClose={this.toggleModal}>
                    <View
                        style={{
                            backgroundColor: '#000000aa',
                            justifyContent: 'center',
                            flex: 1,
                            alignContent: 'center',
                            // flexDirection: 'row',
                            alignItems: 'center'
                        }}
                    >
                        <Animated.View style={{
                            display: (fromTimeSaved ? 'none' : 'flex'),
                            width: animation.fromTimeDialogWidth,
                            height: animation.fromTimeDialogHeight
                        }}>
                            <TimePickerDialog
                                initDate={initStartDate}
                                onCancel={this.hideModal.bind(this)}
                                onOk={(time) => {
                                    this.pressNextButton(time)
                                }}
                                okText='Next' />
                        </Animated.View>
                        <Animated.View style={{
                            display: (!fromTimeSaved ? 'none' : 'flex'),
                            width: animation.toTimeDialogWidth,
                            height: animation.toTimeDialogHeight
                        }}>
                            <TimePickerDialog
                                initDate={initEndDate}
                                onCancel={() => {
                                    this.pressBackButton()
                                }}
                                onOk={(time) => {
                                    this.pressOkButton(time)
                                }}
                                okText='Okay'
                                cancelText='Back' />
                        </Animated.View>
                    </View>
                </Modal>
            </View >
        )
    }

    showModal = () => {
        this.setState({
            showModal: true,
            fromTimeSaved: false,
            animation: {
                fromTimeDialogWidth: new Animated.Value(this.props.width),
                fromTimeDialogHeight: new Animated.Value(this.props.height),
                toTimeDialogWidth: new Animated.Value(0),
                toTimeDialogHeight: new Animated.Value(0),
            },
        })
    }

    hideModal = () => {
        this.setState({ showModal: false })
        this.setState({ fromTimeSaved: false })
    }

    pressNextButton = (time) => {
        let markedDates = [time, '']
        this.setState({ fromTimeSaved: true, markedDates })

        const animate = Animated.timing;
        const duration = 200
        Animated.parallel([
            animate(this.state.animation.fromTimeDialogWidth, {
                toValue: 0,
                duration
            }),
            animate(this.state.animation.fromTimeDialogHeight, {
                toValue: 0,
                duration
            }),
            animate(this.state.animation.toTimeDialogWidth, {
                toValue: this.props.width,
                duration
            }),
            animate(this.state.animation.toTimeDialogHeight, {
                toValue: this.props.height,
                duration
            }),

        ]).start()
    }

    pressBackButton = () => {
        let markedDates = this.state.markedDates
        markedDates[1] = null
        this.setState({ fromTimeSaved: false, markedDates })

        const timing = Animated.timing;
        const duration = 200
        Animated.parallel([
            timing(this.state.animation.fromTimeDialogWidth, {
                toValue: this.props.width,
                duration
            }),
            timing(this.state.animation.fromTimeDialogHeight, {
                toValue: this.props.height,
                duration
            }),
            timing(this.state.animation.toTimeDialogWidth, {
                toValue: 0,
                duration
            }),
            timing(this.state.animation.toTimeDialogHeight, {
                toValue: 0,
                duration
            }),

        ]).start()
    }

    pressOkButton = (time) => {
        let markedDates = this.state.markedDates
        markedDates[1] = time
        if (!this.validate(markedDates)) return

        this.setState({
            markedDates,
            text: this.formatText(markedDates)
        })
        this.hideModal()
        this.props.onValueChanged(markedDates)
    }

    formatText(markedDates, formatString = TimeFormat) {
        return `${format(markedDates[0], formatString)} to ${format(markedDates[1], formatString)}`
    }

    validate(markedDates) {
        if (markedDates[0] > markedDates[1]) {
            alert('start time must be less than end time')
            return false
        }
        return true
    }
}

export const TimePicker = TimePicker
