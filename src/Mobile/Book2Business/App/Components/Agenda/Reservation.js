import React, { Component } from 'react'
import {
    StyleSheet,
    View,
    Text
} from 'react-native'

import { xdateToData } from './interface';
import XDate from 'xdate';
import dateutils from './dateutils';
import styleConstructor from './Styles/styleConstructor';
import AnimatedTouchable from '../AnimatedTouchable'


class ReservationListItem extends Component {
    constructor(props) {
        super(props);
        this.styles = styleConstructor(props.theme);
    }

    render() {
        const { reservation, date } = this.props.item;
        let content;
        if (reservation) {
            const firstItem = date ? true : false;
            content = this.props.renderItem(reservation, firstItem);
        } else {
            content = this.props.renderEmptyDate(date);
        }
        return (
            <AnimatedTouchable
                // key={this.props.key}
                style={[
                    this.styles.container,
                    styles.container,
                    this.props.style
                ]}>
                {content}

            </AnimatedTouchable>
        );
    }
}

export default ReservationListItem;

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#cbcbcb',
        position: 'absolute',
        borderColor: '#eff2ff',
        borderRadius: 10,
        borderWidth: StyleSheet.hairlineWidth,
        marginHorizontal: 5,
        padding: 0,
    },

})

