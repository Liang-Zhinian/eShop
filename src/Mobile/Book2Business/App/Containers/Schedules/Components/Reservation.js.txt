import React from 'react'
import {
    StyleSheet,
    View,
    Text
}
    from 'react-native'

import AnimatedTouchable from '../../../Components/AnimatedTouchable'

export default (props) => (
    <View style={[
        styles.container, props.style
    ]}>
        <Text style={styles.when}>
            {props.when}
        </Text>
        <Text style={styles.who}>
            {props.who}
        </Text>
        <Text style={styles.what}>
            {props.what}
        </Text>
        <Text style={styles.where}>
            {props.where}
        </Text>
    </View>
)

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: 'yellow',
        position: 'absolute',
        borderColor: 'blue',
        borderRadius: 10,
        borderWidth: StyleSheet.hairlineWidth,
        marginHorizontal: 5,
        padding: 5,
    },
    when: {

    },
    who: {
        fontWeight: 'bold'
    },
    what: {

    },
    where: {

    },

})

