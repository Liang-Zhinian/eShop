import React, { Component } from 'react'
import { TouchableOpacity, Text, ScrollView, View, Image, Button } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Container, Content, Icon, Header } from 'native-base'
import { Header as RCTHeader } from 'react-navigation'

import { Colors, Fonts, Metrics } from '../../Themes/'
import Messages from '../../Components/Messages'
import Loading from '../../Components/Loading'
import Error from '../../Components/Error'
import { Images } from '../../Themes'
import site from '../../Constants/site'
import { getServiceCategories, setError } from '../../Actions/serviceCategories'
import styles from './Styles/AppointmentsScreenStyle'
import List from '../../Components/List'
import ListItem from '../../Components/ListItem'
import BackButton from '../../Components/BackButton'

const dataArray = [
  { id: 0, title: 'Single', content: 'Lorem ipsum dolor sit amet' },
  { id: 1, title: 'Complimentary', content: 'Lorem ipsum dolor sit amet' },
  { id: 2, title: 'Redeemable', content: 'Lorem ipsum dolor sit amet' },
  { id: 3, title: 'Package', content: 'Lorem ipsum dolor sit amet' },
  { id: 4, title: 'Personal Training', content: 'Lorem ipsum dolor sit amet' }
]

class ServiceCategoryListing extends Component {
  static propTypes = {
    // Layout: PropTypes.func.isRequired,
    serviceCategories: PropTypes.shape({
      loading: PropTypes.bool.isRequired,
      error: PropTypes.string,
      serviceCategories: PropTypes.shape({
        PageIndex: PropTypes.number,
        PageSize: PropTypes.number,
        Count: PropTypes.number,
        Data: PropTypes.arrayOf(PropTypes.shape()).isRequired
      })
    }).isRequired,
    match: PropTypes.shape({
      params: PropTypes.shape({})
    }),
    fetchServiceCategories: PropTypes.func.isRequired,
    // fetchMeals: PropTypes.func.isRequired,
    showError: PropTypes.func.isRequired
  }

  static defaultProps = {
    match: null
  }

  static navigationOptions = ({ navigation }) => ({
    tabBarLabel: 'More',
    tabBarIcon: ({ focused }) => (
      <Image
        source={
          focused
            ? Images.activeInfoIcon
            : Images.inactiveInfoIcon
        }
      />
    ),
    title: 'Appointment Categories',
    headerMode: 'screen',
    headerBackTitleVisible: true,
    headerLeft: (
      <BackButton navigation={navigation} />
    )
  })

  constructor (props) {
    super(props)
    this.state = {
      basic: true,
      listViewData: dataArray
    }
  }

  componentDidMount () {
    // this.setState({ listViewData: this.props.serviceCategories });
    this.fetchServiceCategories()
  }

  static renderRightButton = (props) => {
    return (
      <TouchableOpacity
        onPress={() => { Actions.appointment_category({ match: { params: { action: 'ADD' } } }) }}
        style={{ marginRight: 10 }}>
        {/* <Icon name='add' /> */}
      </TouchableOpacity>
    )
  }

  render = () => {
    const { serviceCategories, match } = this.props
    const id = (match && match.params && match.params.id) ? match.params.id : null

    let listViewData = []
    if (serviceCategories.serviceCategories && serviceCategories.serviceCategories.Data) {
      listViewData = serviceCategories.serviceCategories.Data.map(item => ({
        id: item.Id,
        title: item.Name,
        content: item.Description
      }))
    }

    const {
      latitude,
      longitude
    } = this.state

    return (
      <List
        headerTitle='Appointment Categories'
        navigation={this.props.navigation}
        data={listViewData}
        renderItem={this._renderRow.bind(this)}
        keyExtractor={(item, idx) => item.id}
        contentContainerStyle={styles.listContent}
        showsVerticalScrollIndicator={false}
        reFetch={() => this.fetchServiceCategories()}
        serviceCategoryId={id}
        error={serviceCategories.error}
        loading={serviceCategories.loading}
      />
    )
  }

  _renderRow ({ item }) {
    return (
      <ListItem
        name={item.title}
        title={item.content}
        onPress={() => {
          const { navigation } = this.props
          navigation.navigate('AppointmentListing', { id: item.id })
        }}
        onPressEdit={() => {
          const { navigation } = this.props
          navigation.navigate('AppointmentListing', { id: item.id })
        }}
        onPressRemove={() => {
          const { navigation } = this.props
          navigation.navigate('AppointmentListing', { id: item.id })
        }} />
    )
  }

  /**
    * Fetch Data from API, saving to Redux
    */
  fetchServiceCategories = () => {
    const { fetchServiceCategories, showError, dispatch } = this.props

    return fetchServiceCategories(site.siteId, 10, 0)
      .catch((err) => {
        console.log(`Error: ${err}`)
        return showError(err)
      })
  }
}

const mapStateToProps = state => ({
  serviceCategories: state.serviceCategories || {}
})

const mapDispatchToProps = {
  fetchServiceCategories: getServiceCategories,
  showError: setError,
  setSelectedEvent: (data) => dispatch({
    type: 'SERVICE_CATEGORY_SELECTED',
    data: data
  })
}

export default connect(mapStateToProps, mapDispatchToProps)(ServiceCategoryListing)
