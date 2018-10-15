import React, { Component } from 'react'
import { TouchableOpacity, Text, ScrollView, View, Image, FlatList, ActivityIndicator, RefreshControl as RNRefreshControl } from 'react-native'
import PropTypes from 'prop-types'
import {
  sum
} from 'ramda'

import styles from './Styles/ListStyle'
import ListItem from './ListItem'
import Loading from '../Loading'
import Error from '../Error'
import { Colors } from '../../Themes'

export const RefreshControl = ({ refreshing, onRefresh }) => (
  <RNRefreshControl
    refreshing={refreshing}
    onRefresh={onRefresh}
  />
)

export const ListFooterComponent = ({ isLoading, onPress }) => (
  <View style={styles.footerStyle}>
    {
      isLoading
        ? <ActivityIndicator color={Colors.lightText} />
        : <TouchableOpacity
          activeOpacity={0.7}
          style={styles.TouchableOpacity_style}
          onPress={onPress}
        >
          <Text style={styles.TouchableOpacity_Inside_Text}>Click to load more</Text>

        </TouchableOpacity>
    }
  </View>
)

const getItemLayout = (data, index) => {
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

const renderItem = ({ item }) => {
  return (
    <ListItem
      name={'Missing name'}
      title={'Missing title'}
      onPress={() => {
        window.alert('Item clicked!')
      }} />
  )
}

class List extends Component {
  static propTypes = {
    error: PropTypes.string,
    loading: PropTypes.bool.isRequired,
    headerTitle: PropTypes.string,
    navigation: PropTypes.shape({}),
    extraData: PropTypes.shape({}),
    renderItem: PropTypes.func,
    keyExtractor: PropTypes.func,
    showsVerticalScrollIndicator: PropTypes.bool,
    data: PropTypes.arrayOf(PropTypes.shape()),
    refresh: PropTypes.func,
    loadMore: PropTypes.func,
    refreshing: PropTypes.bool,
    loadingMore: PropTypes.bool,
    getItemLayout: PropTypes.func
  }

  static defaultProps = {
    error: null,
    success: null,
    headerTitle: '',
    navigation: null,
    extraData: null,
    renderItem: renderItem,
    getItemLayout: getItemLayout,
    keyExtractor: (item, idx) => idx,
    showsVerticalScrollIndicator: false,
    data: [],
    pageSize: 20,
    pageIndex: 0,
    refresh: _ => null,
    loadMore: _ => null,
    refreshing: false,
    loadingMore: false
  }

  constructor (props) {
    super(props)
  }

  render = () => {
    const { loading, error, refresh, refreshing, loadingMore, loadMore } = this.props

    // Loading
    if (loading) return <Loading />

    // Error
    if (error) return <Error content={error} />

    return (
      <FlatList
        data={this.props.data}
        extraData={this.props.extraData}
        renderItem={this.props.renderItem}
        keyExtractor={this.props.keyExtractor}
        contentContainerStyle={[styles.listContent]}
        getItemLayout={this.props.getItemLayout}
        showsVerticalScrollIndicator={this.props.showsVerticalScrollIndicator}
        refreshControl={(
          <RefreshControl
            refreshing={refreshing}
            onRefresh={refresh}
          />
        )}
        ListFooterComponent={() => {
          return (
            <ListFooterComponent
              isLoading={loadingMore}
              onPress={loadMore} />
          )
        }}
      />
    )
  }
}

export default List
