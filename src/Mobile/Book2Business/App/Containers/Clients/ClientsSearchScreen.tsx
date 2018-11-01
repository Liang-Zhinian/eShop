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
    this.searchClients(this.state.pageSize, 1)
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

  searchClients = async (pageSize = 20, pageIndex = 1) => {
    const { searchClients, showError } = this.props
    return searchClients(this.state.searchKeywords, pageSize, pageIndex)
      .then(json => json.data)
      .catch((err) => {
        console.log(`Error: ${err}`)
        return showError(err)
      })
  }

  renderClientList = () => {
    return (
      <View style={{ height: (SCREEN_HEIGHT - 85 - 180) }}>
        <List
          data={this.state.data}
          renderItem={this.renderRow.bind(this)}
          keyExtractor={(item, idx) => item.id}
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
  }

  renderRow({ item }) {
    const { address, title } = item
    return (
      <AnimatedTouchable
        onPress={() => {
          this.onSelectRow(item)
        }}
        style={{
          borderRadius: Metrics.cardRadius,
          marginVertical: Metrics.baseMargin,
          marginHorizontal: Metrics.doubleBaseMargin
        }}
      >

      </AnimatedTouchable>
    )
  }

  renderHeader() {
    return (

      <GradientHeader>
        <View style={[styles.header]}>
          <Left>
            <BackButton onPress={this.props.screenProps.hideModal} />
          </Left>
          <Body>
            <Title style={{ color: Colors.snow }}>Pick a location</Title>
          </Body>
          <Right>
          </Right>
        </View>
      </GradientHeader>
    )
  }

  renderSearchBox() {
    return <SearchBox
      onBeforeSearch={() => { }}
      onSearch={() => { }}
      onAfterSearch={() => { }}
    />
  }

  render() {
    return (
      <GradientView style={[styles.linearGradient]}>
        {/* {this.renderHeader()} */}
        {/* {this.renderSearchBox()} */}
        {this.renderClientList()}
      </GradientView>

    )
  }
}

const mapStateToProps = (state) => {
  return {
    // isLoading: state.status.loading || false,
    nearbyData: state.nearby
  }
}

const mapDispatchToProps = {
  searchClients: searchClients,
  showError: setError
}

export default connect(mapStateToProps, mapDispatchToProps)(ClientsSearchScreen)
