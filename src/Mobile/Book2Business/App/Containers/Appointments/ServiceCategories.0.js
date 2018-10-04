import React, { Component } from 'react'
import { TouchableOpacity, Text, ScrollView, View, Image } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Icon } from 'native-base'

import PurpleGradient from '../../Components/PurpleGradient'
import site from '../../Constants/site'
import { getServiceCategories, setError } from '../../Actions/serviceCategories'
import Layout from '../../Components/Appointments/ServiceCategories'
import styles from './Styles/AppointmentsScreenStyle'
import Header from '../../Components/Header'

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

  static renderRightButton = (props) => {
    return (
      <TouchableOpacity
        onPress={() => { Actions.appointment_category({ match: { params: { action: 'ADD' } } }) }}
        style={{ marginRight: 10 }}>
        <Icon name='add' />
      </TouchableOpacity>
    )
  }

  componentDidMount = () => this.fetchServiceCategories();

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

    return (
      <PurpleGradient style={styles.linearGradient}>
        <Header
          title={'title'}
          navigation={this.props.navigation}
        />
        <ScrollView>
          <Layout
            serviceCategoryId={id}
            error={serviceCategories.error}
            loading={serviceCategories.loading}
            serviceCategories={listViewData}
            reFetch={() => this.fetchServiceCategories()}
            navigation={this.props.navigation}
            />
        </ScrollView>
      </PurpleGradient>
    )
  }

  /**
    * Fetch Data from API, saving to Redux
    */
  fetchServiceCategories = () => {
    const { fetchServiceCategories, showError, dispatch } = this.props

    return dispatch(fetchServiceCategories(site.siteId, 10, 0))
  }
}

const mapStateToProps = state => ({
  serviceCategories: state.serviceCategories || {}
})

const mapDispatchToProps = (dispatch) => ({
  dispatch: dispatch,
  fetchServiceCategories: getServiceCategories,
  showError: setError,
  setSelectedEvent: (data) => dispatch({
    type: 'SERVICE_CATEGORY_SELECTED',
    data: data
  })
})

export default connect(mapStateToProps, mapDispatchToProps)(ServiceCategoryListing)
