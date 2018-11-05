// @flow

import React from 'react'
import {
  View, TouchableOpacity, Text, Dimensions
} from 'react-native'
import {
  Left, Right, Body, Button, Title
} from 'native-base'
import { Images, Colors, Metrics } from '../../Themes'
import Icon from 'react-native-vector-icons/FontAwesome'
import { connect } from 'react-redux'

import List from '../../Components/List'

import Client from './Components/Client_Lite'
import AnimatedTouchable from '../../Components/AnimatedTouchable'
import GradientView from '../../Components/GradientView'
import GradientHeader from '../../Components/GradientHeader'
import BackButton from '../../Components/BackButton'
import SearchBox from '../../Components/SearchBox'
import styles from './Styles/ClientsSearchScreenStyle'
import { searchClients, setError } from '../../Actions/clients'

const SCREEN_HEIGHT = Dimensions.get('window').height


interface ClientsSearchScreenProps {
  screenProps: { hideModal(): void }
  style: StyleSheet
  showError(err): object
  searchClients(searchKeywords, pageSize, pageIndex): object
  onValueChanged (item): void
}

interface ClientsSearchScreenState {
  isLoading: boolean
  error: String
  data: object[]
  selectedItem: object
  pageSize: number
  pageIndex: number
  reFetchingStatus: boolean
  fetchingNextPageStatus: boolean
  searchKeywords: String
}

class ClientsSearchScreen extends React.Component<ClientsSearchScreenProps, ClientsSearchScreenState> {

  constructor(props) {
    super(props)

    this.state = {
      isLoading: false,
      selectedItem: null,
      data: null,
      pageSize: 20,
      pageIndex: 1,
      reFetchingStatus: false,
      fetchingNextPageStatus: false,
      searchKeywords: '',
      error: null
    }

  }

  componentWillReceiveProps(newProps) {

  }

  componentDidMount() {
  }

  onRefresh = () => {
    this.setState({ reFetchingStatus: true })
    this.searchClients(this.state.searchKeywords, this.state.pageSize, 0)
      .then(clients => {
        this.setState({
          data: clients.data,
          pageIndex: 1,
          isLoading: false,
          reFetchingStatus: false
        })
      })
      .catch(error => console.log(error))
  }

  onNextPage = async () => {
    this.setState({ fetchingNextPageStatus: true })
    const { pageSize, pageIndex, data } = this.state
    const clients = await this.searchClients(pageSize, pageIndex + 1)
      .catch(error => console.log(error))
    this.setState({
      data: [...data, ...clients.data],
      pageIndex: pageIndex + 1,
      fetchingNextPageStatus: false
    })
  }

  renderClientList = () => {
    return (
      <View style={{ height: (SCREEN_HEIGHT - 85 - 180) }}>
        <List
          data={this.state.data}
          renderItem={this.renderRow.bind(this)}
          keyExtractor={(item, idx) => item.Id}
          error={this.state.error}
          loading={this.state.isLoading}
          refresh={this.onRefresh.bind(this)}
          loadMore={this.onNextPage.bind(this)}
          refreshing={this.state.reFetchingStatus}
          loadingMore={this.state.fetchingNextPageStatus} />
      </View>
    )
  }

  onSelectRow(item) {
    this.setState({
      selectedItem: item
    })
    this.props.onValueChanged && this.props.onValueChanged({key: item.Id, value:item })
  }

  renderRow({ item }) {
    const { address, title } = item
    return (
      <Client
        name={item.Name + ' ' + item.LastName}
        avatarURL={item.AvatarImageUri || ''}
        title={item.Email}
        onPress={() => { this.onSelectRow(item) }}
      />
    )
  }

  renderSearchBox() {
    return <SearchBox
      onBeforeSearch={(searchText) => { }}
      onSearch={this.searchClients.bind(this)}
      onAfterSearch={(searchText) => {
        this.setState({ searchKeywords: searchText })
      }}
    />
  }

  searchClients(searchText, pageSize = 10, pageIndex = 0) {
    const { searchClients } = this.props
    return searchClients(searchText, pageSize, pageIndex)
      .then(res => {
        this.setState({
          data: res.data.Data,
        })
      })
  }

  render() {
    return (
      <GradientView style={[styles.linearGradient]}>
        {/* {this.renderHeader()} */}
        {this.renderSearchBox()}
        {this.renderClientList()}
      </GradientView>

    )
  }
}

const mapStateToProps = (state) => {
  return {
    // isLoading: state.status.loading || false,
    clients: state.clients
  }
}

const mapDispatchToProps = {
  searchClients: searchClients,
  showError: setError
}

export default connect(mapStateToProps, mapDispatchToProps)(ClientsSearchScreen)
