import React, { Component } from 'react'
import { TouchableOpacity, Text, ScrollView, View } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Icon } from 'native-base'

import PurpleGradient from '../../Components/PurpleGradient'
import site from '../../Constants/site'
import { getServiceItems, setError } from '../../Actions/serviceItems'
import Layout from '../../Components/Appointments/ServiceItems'
import styles from './Styles/AppointmentsScreenStyle'

class ServiceItemListing extends Component {
  static propTypes = {
        // Layout: PropTypes.func.isRequired,
    serviceItems: PropTypes.shape({
      loading: PropTypes.bool.isRequired,
      error: PropTypes.string,
      serviceItems: PropTypes.arrayOf(PropTypes.shape()).isRequired
    }).isRequired,
    match: PropTypes.shape({
      params: PropTypes.shape({})
    }),
    fetchServiceItems: PropTypes.func.isRequired,
        // fetchMeals: PropTypes.func.isRequired,
    showError: PropTypes.func.isRequired
  }

  static defaultProps = {
    match: null
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
      <PurpleGradient style={styles.linearGradient}>
        <ScrollView>
          <View style={{ marginTop: 20 }}>
            <Layout
              serviceItemId={id}
              error={serviceItems.error}
              loading={serviceItems.loading}
              serviceItems={listViewData}
              reFetch={() => this.fetchServiceItems()}
                        />
          </View>
        </ScrollView>
      </PurpleGradient>
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
