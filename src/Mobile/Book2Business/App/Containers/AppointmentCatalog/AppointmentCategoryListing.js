import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'

import { setSelectedCategory, getServiceCategories, setError } from '../../Actions/serviceCategories'
import Layout from './Components/AppointmentCategoryListing'


class AppointmentCategoryListing extends Component {
  static propTypes = {
    // Layout: PropTypes.func.isRequired,
    appointmentCategories: PropTypes.shape({
      loading: PropTypes.bool.isRequired,
      error: PropTypes.string,
      appointmentCategories: PropTypes.shape({
        Count: PropTypes.number.isRequired,
        Data: PropTypes.arrayOf(PropTypes.shape()).isRequired,
        PageIndex: PropTypes.number.isRequired,
        PageSize: PropTypes.number.isRequired,
      }).isRequired,
    }).isRequired,
    fetchAppointmentCategories: PropTypes.func.isRequired,
    showError: PropTypes.func.isRequired,
  }

  componentDidMount = () => this.fetchAppointmentCategories();

  /**
    * Fetch Data from API, saving to Redux
    */
  fetchAppointmentCategories = () => {
    const { member, fetchAppointmentCategories, showError } = this.props

    return fetchAppointmentCategories(member.SiteId, 10, 0)
      .catch((err) => {
        console.log(`Error: ${err}`)
        return showError(err)
      })
  }

  render = () => {
    const { appointmentCategories, navigation } = this.props;

    return (
      <Layout
        error={appointmentCategories.error}
        loading={appointmentCategories.loading}
        appointmentCategories={appointmentCategories.appointmentCategories.Data}
        reFetch={() => this.fetchServiceCategories()}
        navigation={navigation}
      />
    );
  }
}

const mapStateToProps = state => ({
  member: state.member || {},
  appointmentCategories: state.appointmentCategories || {}
})

const mapDispatchToProps = {
  fetchAppointmentCategories: getServiceCategories,
  showError: setError,
  setSelectedEvent: setSelectedCategory
}

export default connect(mapStateToProps, mapDispatchToProps)(AppointmentCategoryListing);

