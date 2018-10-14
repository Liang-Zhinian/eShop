
import React from 'react'
import ListItem from '../../../Components/List/ListItem'

export default (props) => (
    <ListItem
        name={props.name}
        title={props.title}
        onPress={props.onPress}
        onPressEdit={props.onPressEdit}
        onPressRemove={props.onPressRemove} />
)