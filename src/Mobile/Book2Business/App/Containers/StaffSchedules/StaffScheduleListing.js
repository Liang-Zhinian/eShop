import React, { Component } from 'react'
import { TouchableOpacity, Text, Image, View, StyleSheet, FlatList } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'

import { Images } from '../../Themes'
import { getStaffSchedules, setError } from '../../Actions/staffSchedules'
import List from '../../Components/List/List'
import Availability from './Components/Availability'
import { staffSchedules as testData } from '../../Fixtures/data'
import AddButton from '../../Components/AddButton'

class StaffScheduleListing extends Component {
  static propTypes = {
    member: PropTypes.shape({}).isRequired,
    staffSchedules: PropTypes.shape({
      loading: PropTypes.bool.isRequired,
      error: PropTypes.string,
      staffSchedules: PropTypes.shape({}).isRequired
    }).isRequired,
    match: PropTypes.shape({
      params: PropTypes.shape({})
    }),
    fetchStaffSchedules: PropTypes.func.isRequired,
    showError: PropTypes.func.isRequired
  }

  static defaultProps = {
    match: null
  }

  static navigationOptions = ({ navigation }) => {
    const { handleAddButton } = navigation.state.params || {
      handleAddButton: () => null,
    }
    return {
      title: 'Staff Schedules',
      headerRight: (<AddButton onPress={handleAddButton} />),
      tabBarLabel: 'More',
      tabBarIcon: ({ focused }) => (
        <Image
          source={
            focused
              ? Images.activeInfoIcon
              : Images.inactiveInfoIcon
          }
        />
      )
    }
  };

  componentDidMount = () => {
    const { navigation, } = this.props
    navigation.setParams({
      handleAddButton: this.handleAddButton.bind(this)
    })

    this.fetchStaffSchedules()
  }

  state = {
    errorMessage: null,
    successMessage: null,
    reFetchingStatus: false,
    fetchingNextPageStatus: false,
  }

  animatedContainerWithNavbar = null;

  render = () => {
    const { staffSchedules } = this.props
    console.log(staffSchedules)

    let listViewData = staffSchedules.staffSchedules ? staffSchedules.staffSchedules.Data : testData

    return (
      <List
        headerTitle='Staff schedules'
        navigation={this.props.navigation}
        data={listViewData}
        renderItem={this.renderItem.bind(this)}
        keyExtractor={(item, idx) => item.Id}
        contentContainerStyle={styles.listContent}
        showsVerticalScrollIndicator={false}
        refresh={this.fetchStaffSchedules.bind(this)}
        loadMore={this.onNextPage.bind(this)}
        refreshing={this.state.reFetchingStatus}
        loadingMore={this.state.fetchingNextPageStatus}
        error={staffSchedules.error}
        loading={staffSchedules.loading}
      />
    )
  }

  getItemLayout = (data, index) => {
    const item = data[index]
    const itemLength = (item, index) => {
      if (item.type === 'talk') {
        // use best guess for variable height rows
        return 138 + (1.002936 * item.title.length + 6.77378)
      } else {
        return 145
      }
    }
    const length = itemLength(item)
    const offset = sum(data.slice(0, index).map(itemLength))
    return { length, offset, index }
  }

  // if value exists, create the function calling it, otherwise false
  funcOrFalse = (func, val) => val ? () => func.call(this, val) : false

  renderItem = ({ item }) => {
    return (
      <Availability
        name={`${item.ServiceItemId}`}
        title={`from ${item.StartDateTime} to ${item.EndDateTime}`}
        onPress={() => { this.props.navigation.navigate('StaffSchedule', { schedule: item, ActionType: 'Add' }) }}
        onPressEdit={() => { }}
        onPressRemove={() => { }} />
    )
  }

  handleAddButton() {
    const { navigation } = this.props
    navigation.navigate('StaffSchedule', { ActionType: 'Add' })
  }

  fetchStaffSchedules() {
    const { member, fetchStaffSchedules, selectedAppointmentType, showError } = this.props

    fetchStaffSchedules(member.SiteId, member.currentLocation.Id, member.Id, selectedAppointmentType.Id)
      .catch((err) => {
        console.log(`Error: ${err}`)
        return showError(err)
      })
  }

  onNextPage(pageSize = 10, pageIndex = 0) {
    const { member, fetchStaffSchedules, selectedAppointmentType, showError } = this.props

    fetchStaffSchedules(member.SiteId,
      member.currentLocation.Id,
      member.Id,
      selectedAppointmentType.Id,
      pageSize,
      pageIndex)
      .catch((err) => {
        console.log(`Error: ${err}`)
        return showError(err)
      })
  }
}

const mapStateToProps = state => ({
  member: state.member || {},
  selectedAppointmentType: state.appointmentTypes.selectedAppointmentType || {},
  staffSchedules: state.staffSchedules || {},
  isLoading: state.status.loading || false
})

const mapDispatchToProps = {
  fetchStaffSchedules: getStaffSchedules,
  showError: setError
}

export default connect(mapStateToProps, mapDispatchToProps)(StaffScheduleListing)

const styles = StyleSheet.create({

  textFooter: {
    color: '#fff'
  },
  menutext: {
    fontSize: 20,
    padding: 10
  },
})
