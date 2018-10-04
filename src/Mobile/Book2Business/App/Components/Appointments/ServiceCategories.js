import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { FlatList, View, ListView, TouchableOpacity, TouchableWithoutFeedback, LayoutAnimation, Animated } from 'react-native'
import { Container, Header, Content, Left, Body, Right, Button, Icon, List, Text } from 'native-base'

import {
  merge,
  groupWith,
  contains,
  assoc,
  map,
  sum,
  findIndex
} from 'ramda'
import ListItem from '../ListItem'
import styles from '../Styles/ListStyle'

const dataArray = [
  { title: 'Single', content: 'Lorem ipsum dolor sit amet' },
  { title: 'Complimentary', content: 'Lorem ipsum dolor sit amet' },
  { title: 'Redeemable', content: 'Lorem ipsum dolor sit amet' },
  { title: 'Package', content: 'Lorem ipsum dolor sit amet' },
  { title: 'Personal Training', content: 'Lorem ipsum dolor sit amet' }
]

export default class ServiceCategories extends Component {
  constructor (props) {
    super(props)
    this.ds = new ListView.DataSource({ rowHasChanged: (r1, r2) => r1 !== r2 })
    this.state = {
      basic: true,
      listViewData: dataArray
    }
  }

  componentDidMount () {
    this.setState({ listViewData: this.props.serviceCategories })
  }

  render () {
    const ds = new ListView.DataSource({ rowHasChanged: (r1, r2) => r1 !== r2 })

    return (
      <View style={{flex: 1, marginTop: 20}}>
        <FlatList
          data={this.state.listViewData}
          extraData={this.props}
          renderItem={this._renderRow.bind(this)}
          keyExtractor={(item, idx) => item.id}
          contentContainerStyle={styles.listContent}
          getItemLayout={this.getItemLayout}
          showsVerticalScrollIndicator={false}
            />
      </View>

    )
  }

  getItemLayout = (data, index) => {
    const item = data[index]
    const itemLength = (item, index) => {
      if (item.type === 'talk') {
        // use best guess for variable height rows
        return 138 + (1.002936 * item.title.length + 6.77378)
      } else {
        return 145
      }
    }
    const length = itemLength(item)
    const offset = sum(data.slice(0, index).map(itemLength))
    return { length, offset, index }
  }

  _renderRow ({ item }) {
    return (
      <ListItem
        name={item.title}
        title={item.content}
        onPress={() => {
          const { navigation } = this.props

          navigation.navigate('AppointmentListing', { id: item.id })
        }} />
    )
  }

  _deleteRow (secId, rowId, rowMap) {
    rowMap[`${secId}${rowId}`].props.closeRow()
    const newData = [...this.state.listViewData]
    newData.splice(rowId, 1)
    this.setState({ listViewData: newData })
  }
}
