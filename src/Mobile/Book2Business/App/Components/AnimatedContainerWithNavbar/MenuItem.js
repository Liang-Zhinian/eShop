import React from  'react'
import {TouchableOpacity, Text} from 'react-native'
import styles from './Styles'


export default (props) => (
    <TouchableOpacity style={{}} onPress={props.onPress} >
        <Text style={[styles.textFooter, styles.menutext]}>{props.text}</Text>
    </TouchableOpacity >
)