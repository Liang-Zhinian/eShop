import React, { Component } from 'react'
import { TouchableOpacity, Text, ScrollView, View, Image } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Icon } from 'native-base'

import { Images } from '../../Themes'
import site from '../../Constants/site'
import { getServiceItems, setError } from '../../Actions/serviceItems'
import styles from './Styles/AppointmentsScreenStyle'
import List from '../../Components/List'
import ListItem from '../../Components/ListItem'

class ServiceItemListing extends Component {
  static propTypes = {
    serviceItems: PropTypes.shape({
      loading: PropTypes.bool.isRequired,
      error: PropTypes.string,
      serviceItems: PropTypes.arrayOf(PropTypes.shape()).isRequired
    }).isRequired,
    match: PropTypes.shape({
      params: PropTypes.shape({})
    }),
    fetchServiceItems: PropTypes.func.isRequired,
    showError: PropTypes.func.isRequired
  }

  static defaultProps = {
    match: null
  }

  static navigationOptions = {
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
    title: 'Appointments'
  }

  constructor (props) {
    super(props)
  }

  static renderRightButton = (props) => {
    return (
      <TouchableOpacity
        onPress={() => { Actions.appointment_category({ match: { params: { action: 'ADD' } } }) }}
        style={{ marginRight: 10 }}>
        <Icon name='add' />
      </TouchableOpacity>
    )
  }

  componentDidMount = () => this.fetchServiceItems();

    /**
      * Fetch Data from API, saving to Redux
      */
  fetchServiceItems = () => {
    const { fetchServiceItems, showError, match } = this.props
    const serviceCategoryId = (match && match.params && match.params.id) ? match.params.id : null
    return fetchServiceItems(site.siteId, serviceCategoryId, 10, 0)
            //   .then(() => fetchMeals())
            .catch((err) => {
              console.log(`Error: ${err}`)
              return showError(err)
            })
  }

  render = () => {
    const { serviceItems, match } = this.props
    const id = (match && match.params && match.params.id) ? match.params.id : null

    let listViewData = []
    if (serviceItems.serviceItems && serviceItems.serviceItems.Data) {
      listViewData = serviceItems.serviceItems.Data.map(item => ({
        id: item.Id,
        title: item.Name,
        content: item.Description
      }))
    }

    return (

      <List
        headerTitle='Appointments'
        navigation={this.props.navigation}
        data={listViewData}
        renderItem={this._renderRow.bind(this)}
        keyExtractor={(item, idx) => item.id}
        contentContainerStyle={styles.listContent}
        showsVerticalScrollIndicator={false}
        reFetch={() => this.fetchServiceItems()}
        serviceItemId={id}
        error={serviceItems.error}
        loading={serviceItems.loading}
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
                    //   navigation.navigate('AppointmentListing', { id: item.id })
        }}
        onPressEdit={() => {
          const { navigation } = this.props
                //   navigation.navigate('AppointmentListing', { id: item.id })
        }}
        onPressRemove={() => {
          const { navigation } = this.props
                //   navigation.navigate('AppointmentListing', { id: item.id })
        }} />
    )
  }
}

const mapStateToProps = (state, props) => ({
  serviceItems: state.serviceItems || {},
  match: props.navigation.state
})

const mapDispatchToProps = {
  fetchServiceItems: getServiceItems,
  showError: setError
}

export default connect(mapStateToProps, mapDispatchToProps)(ServiceItemListing)
