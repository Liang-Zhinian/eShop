import React from 'react'
import {
    View,
    Text,
    ListView,
    Animated,
    StyleSheet,
    Dimensions,
    TouchableOpacity,
} from 'react-native'
import PropTypes from 'prop-types'

var moment = require('moment');
var SCREEN_WIDTH = Dimensions.get('window').width;
var SCREEN_HEIGHT = Dimensions.get('window').height;
var HOURS = [
    '1', '2', '3', '4', '5', '6', '7',
    '8', '9', '10', '11', '12'
];
var MINUTES = ['00', '15', '30', '45'];
var AmPm = ['AM', 'PM']

class ItemsPicker extends React.Component {
    constructor(props) {
        super(props)
    }

    _setItem = (rowdata) => {
        this.props.setItem(rowdata);
    }

    _renderHeader = () => {
        return <View style={{ height: SCREEN_HEIGHT / 4 - 50 }}></View>;
    }

    _renderFooter = () => {
        return <View style={{ height: SCREEN_HEIGHT / 4 - 50 }}></View>;
    }

    _renderRow = (rowdata) => {
        var rowStyle = this.props.currentItem === rowdata ? { color: 'black', transform: [{ scale: 1.3 }] } : { color: 'gray' };
        return (
            <TouchableOpacity onPress={this._setItem.bind(this, rowdata)}>
                <View style={styles.rowItem}>
                    <Text style={[rowStyle, { fontSize: 20 }]}>{rowdata}</Text>
                </View>
            </TouchableOpacity>
        );
    }

    render = () => {
        var ds = new ListView.DataSource({
            rowHasChanged: (row1, row2) => row1 !== row2,
        }).cloneWithRows(this.props.dataSource);
        return (
            <ListView
                ref="listView"
                initialListSize={1}
                dataSource={ds}
                renderRow={this._renderRow}
                renderHeader={this._renderHeader}
                renderFooter={this._renderFooter}
                showsVerticalScrollIndicator={false} />
        );
    }
}

class TimePicker extends React.Component {
    static propTypes = {
        onCancel: PropTypes.func,
        onConfirm: PropTypes.func,
    }

    constructor(props) {
        super(props)

        var now = new Date();
        var localeTimeString = now.toLocaleTimeString('en-US')
        var regex1 = RegExp('(\d+):(\d+):(\d+)\s(AM|PM)', 'g');
        var timeSections = regex1.exec(localeTimeString)

        this.state = {
            hour: timeSections[1],
            minute: timeSections[2],
            amPm: timeSections[4]
        }
    }

    _cancel = () => {
        if (this.props.onCancel) {
            this.props.onCancel();
        }
    }

    _confirm = () => {
        console.log('Time=' + this.state.hour + ':' + this.state.minute + ' ' + this.state.amPm);
        var birthday = {
            hour: this.state.hour,
            minute: this.state.minute,
            amPm: this.state.amPm,
        };
        this.props.onConfirm(birthday);
        this._cancel();
    }

    _setHour = (hour) => {
        this.setState({
            hour
        });
    }

    _setMinute = (minute) => {
        this.setState({
            minute,
        });
    }

    _setAmPm = (amPm) => {
        this.setState({ amPm });
    }

    render = () => {
        return (
            <View style={styles.container}>
                <View style={{ height: SCREEN_HEIGHT * 0.5 - 40 }}></View>
                <View style={{ flex: 1 }}>
                    <View style={styles.selectContainer}>
                        <TouchableOpacity onPress={this._cancel}>
                            <View style={[styles.buttonContainer, { alignItems: 'flex-start' }]}>
                                <Text style={[styles.buttonText, { marginLeft: 20 }]}>Cancel</Text>
                            </View>
                        </TouchableOpacity>
                        <TouchableOpacity onPress={this._confirm}>
                            <View style={[styles.buttonContainer, { alignItems: 'flex-end' }]}>
                                <Text style={[styles.buttonText, { marginRight: 20 }]}>Ok</Text>
                            </View>
                        </TouchableOpacity>
                    </View>
                    <View style={styles.datePicker} >
                        <ItemsPicker
                            setItem={this._setHour}
                            currentItem={this.state.hour}
                            dataSource={HOURS} />
                        <ItemsPicker
                            setItem={this._setMinute}
                            currentItem={this.state.minute}
                            dataSource={MINUTES} />
                        <ItemsPicker
                            currentItem={this.state.amPm}
                            dataSource={AmPm}
                            setItem={this._setAmPm} />
                    </View>
                </View>
            </View>
        );
    }
}


var styles = StyleSheet.create({
    container: {
        position: 'absolute',
        top: 0,
        width: SCREEN_WIDTH,
        height: SCREEN_HEIGHT,
        backgroundColor: 'rgba(0,0,0,0.5)',
        justifyContent: 'center',
        alignItems: 'center',
    },
    selectContainer: {
        flex: 1,
        flexDirection: 'row',
        backgroundColor: 'white',
        borderBottomColor: 'grey',
        borderTopColor: 'transparent',
        borderWidth: 1,
        height: 40,
    },
    buttonContainer: {
        flex: 1,
        justifyContent: 'center',
        width: SCREEN_WIDTH / 2,
    },
    buttonText: {
        fontSize: 25,
        color: 'grey',
    },
    datePicker: {
        width: SCREEN_WIDTH,
        height: SCREEN_HEIGHT / 2,
        flexDirection: 'row',
        backgroundColor: 'white',
    },
    rowItem: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        margin: 5,
    },
});

export default TimePicker;