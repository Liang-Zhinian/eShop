import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'

import { setSelectedAppointmentType, getAppointmentTypes, setError } from '../../Actions/appointmentTypes'
import { setSelectedAppointmentCategory, getAppointmentCategories, setError as setAppointmentCategoryError } from '../../Actions/appointmentCategories'

class AppointmentTypeListingContainer extends Component {
  static propTypes = {
    Layout: PropTypes.func.isRequired,
    appointmentCategories: PropTypes.shape({
      loading: PropTypes.bool.isRequired,
      error: PropTypes.string,
      appointmentCategories: PropTypes.shape({}).isRequired
    }).isRequired,
    match: PropTypes.shape({
      params: PropTypes.shape({})
    }),
    fetchAppointmentTypes: PropTypes.func.isRequired,
    fetchAppointmentCategories: PropTypes.func.isRequired,
    showError: PropTypes.func.isRequired
  }

  static defaultProps = {
    match: null
  }

  constructor(props) {
    super(props)
    this.state = {
      reFetchingStatus: false,
      fetchingNextPageStatus: false,
      pageIndex: 0
    }

    this.fetchAppointmentCategories = this.fetchAppointmentCategories.bind(this)
    this.fetchAppointmentTypes = this.fetchAppointmentTypes.bind(this)
  }

  componentDidMount = () => {
    this.fetchAppointmentCategories();
  }

  render = () => {
    const { Layout, appointmentCategories, appointmentTypes, match, } = this.props

    const id = (match && match.params && match.params.id) ? match.params.id : null

    let apptTypes = appointmentTypes.appointmentTypes ? appointmentTypes.appointmentTypes.Data : null
    let categories = appointmentCategories.appointmentCategories ? appointmentCategories.appointmentCategories.Data : null
    let listViewData = [...categories]
    if (apptTypes && apptTypes.length > 0) {
      for (let i = 0; i < categories.length; i++) {
        // console.log(apptTypes[0], categories[i], apptTypes[0].ServiceCategoryId == categories[i].Id)
        // listViewData.push({ ...categories[i] })
        if (apptTypes[0].ServiceCategoryId == categories[i].Id) {
          listViewData[i].AppointmentTypes = apptTypes
          // console.log('listViewData', listViewData)
        }
      }
    }


    return (
      <Layout
        data={listViewData}
        refresh={this.onRefresh}
        // loadMore={this.onNextPage.bind(this)}
        // refreshing={this.state.reFetchingStatus}
        // loadingMore={this.state.fetchingNextPageStatus}
        error={appointmentCategories.error}
        loading={appointmentCategories.loading}
        loadChildren={this.fetchAppointmentTypes}
      />
    )
  }

  fetchAppointmentCategories = () => {
    const { member, fetchAppointmentCategories, showError } = this.props

    return fetchAppointmentCategories(member.SiteId, 10, this.state.pageIndex)
      .catch((err) => {
        console.log(`Error: ${err}`)
        return showError(err.message)
      })
  }

  /**
    * Fetch Data from API, saving to Redux
    */
  fetchAppointmentTypes = (category) => {
    const { member, fetchAppointmentTypes, showError, match } = this.props
    const serviceCategoryId = category ? category.Id : null

    return fetchAppointmentTypes(member.SiteId, serviceCategoryId, 10, 0)
      .then(res => {
        // console.log('fetchAppointmentTypes', res)
      })
      .catch((err) => {
        console.log(`Error: ${err}`)
        return showError(err)
      })
  }

  onRefresh = () => {
    this.setState({ reFetchingStatus: true, pageIndex: 0 })
    const { member, fetchAppointmentTypes, showError, match } = this.props
    const serviceCategoryId = (match && match.params && match.params.id) ? match.params.id : null

    return fetchAppointmentTypes(member.SiteId, serviceCategoryId, 10, this.state.pageIndex)
      .then(res => {
        this.setState({ reFetchingStatus: false })
      })
      .catch((err) => {
        console.log(`Error: ${err}`)
        return showError(err)
      })
  }

  onNextPage = () => {
    this.setState({ fetchingNextPageStatus: true })
    const { member, fetchAppointmentTypes, showError, match } = this.props
    const serviceCategoryId = (match && match.params && match.params.id) ? match.params.id : null

    return fetchAppointmentTypes(member.SiteId, serviceCategoryId, 10, this.state.pageIndex + 1)
      .then(res => {
        this.setState({
          fetchingNextPageStatus: false,
          pageIndex: this.state.pageIndex + 1
        })
      })
      .catch((err) => {
        console.log(`Error: ${err}`)
        return showError(err)
      })
  }
}

const mapStateToProps = (state, props) => ({
  member: state.member || {},
  appointmentTypes: state.appointmentTypes || {},
  appointmentCategories: state.appointmentCategories || {},
})

const mapDispatchToProps = {
  fetchAppointmentTypes: getAppointmentTypes,
  fetchAppointmentCategories: getAppointmentCategories,
  showError: setError,
  setSelectedAppointmentType: setSelectedAppointmentType
}

export default connect(mapStateToProps, mapDispatchToProps)(AppointmentTypeListingContainer)
