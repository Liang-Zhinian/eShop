import React, { Component } from 'react'
import PropTypes from 'prop-types'

import styles from '../Styles/AppointmentsScreenStyle'
import Accordion from '../../../Components/Accordion'

const AppointmentTypeListing = ({
  error,
  loading,
  data,
  titleExtractor,
  renderRow,
  loadAppointmentTypes,
  navigation,
  refresh,
  loadNextPage,
  reFetchingStatus,
  fetchingNextPageStatus,
}) => {

  const keyExtractor = item => item.Id;

  return (
    <Accordion
      data={data}
      titleExtractor={titleExtractor}
      renderItem={renderRow}
      activeIndex={-1}
      onPress={(item)=>loadAppointmentTypes(item)}
      icon="add"
      expandedIcon="remove" />
  )
}

AppointmentTypeListing.propTypes = {
  error: PropTypes.string,
  loading: PropTypes.bool.isRequired,
  data: PropTypes.arrayOf(PropTypes.shape()).isRequired,
  renderRow: PropTypes.func,
  navigation: PropTypes.shape(),
  onRefresh: PropTypes.func,
  onNextPage: PropTypes.func,
  reFetchingStatus: PropTypes.bool,
  fetchingNextPageStatus: PropTypes.bool,
  loadAppointmentTypes: PropTypes.func,
};

AppointmentTypeListing.defaultProps = {
  error: null,
  reFetch: null,
};

export default AppointmentTypeListing;
