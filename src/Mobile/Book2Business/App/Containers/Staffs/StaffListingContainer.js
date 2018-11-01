import React, { Component } from 'react'
import { connect } from 'react-redux'
import PropTypes from 'prop-types'

import * as StaffActions from '../../Actions/staffs'

class StaffListingContainer extends Component {
  static propTypes = {
    Layout: PropTypes.func.isRequired,
    member: PropTypes.shape({}).isRequired,
    staffs: PropTypes.shape({
      loading: PropTypes.bool.isRequired,
      error: PropTypes.string,
      staffs: PropTypes.arrayOf(PropTypes.shape({})),
    }).isRequired,
    match: PropTypes.shape({
      params: PropTypes.shape({})
    }),
    fetchStaffs: PropTypes.func.isRequired,
    showError: PropTypes.func.isRequired
  }

  static defaultProps = {
    match: null
  }

  componentDidMount() {
    this.fetchStaffs()
  }

  fetchStaffs() {
    const { member, fetchStaffs, showError } = this.props

    fetchStaffs(member.SiteId)
      .catch((err) => {
        console.log(`Error: ${err}`)
        return showError(err)
      })
  }

  render() {
    const { navigation, staffs, member, Layout, setSelectedStaff } = this.props
    console.log('staffs', staffs)

    let listViewData = staffs && staffs.staffs ? staffs.staffs : null

    return (
      <Layout
        navigation={navigation}
        data={listViewData}
        error={staffs.error}
        loading={staffs.loading}
        reFetch={this.fetchStaffs}
        setSelectedStaff={setSelectedStaff}
      />
    )
  }
}

const mapStateToProps = (state) => {
  return {
    staffs: state.staffs || {},
    member: state.member || {}
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    setSelectedStaff: (staff) => dispatch(StaffActions.setSelectedStaff(staff)),
    fetchStaffs: (siteId) => dispatch(StaffActions.getStaffs(siteId)),
    showError: (err) => dispatch(StaffActions.setError(err))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(StaffListingContainer)
