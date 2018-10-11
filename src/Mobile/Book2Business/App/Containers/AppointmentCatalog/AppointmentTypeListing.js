import React, { Component } from 'react'
import { TouchableOpacity, Text, ScrollView, View, Image } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'

import { Images } from '../../Themes'
import { setSelectedAppointmentType, getAppointmentTypes, setError } from '../../Actions/appointmentTypes'
import styles from './Styles/AppointmentsScreenStyle'
import List from '../../Components/List/List'
import ListItem from '../../Components/List/ListItem'

class AppointmentTypeListing extends Component {
  static propTypes = {
    appointmentTypes: PropTypes.shape({
      loading: PropTypes.bool.isRequired,
      error: PropTypes.string,
      appointmentTypes: PropTypes.shape({}).isRequired
    }).isRequired,
    match: PropTypes.shape({
      params: PropTypes.shape({})
    }),
    fetchAppointmentTypes: PropTypes.func.isRequired,
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
      title: 'Appointment Types',
      headerRight: (
        <TouchableOpacity style={{ marginRight: 20 }} onPress={handleAddButton} >
          <Text>Add</Text>
        </TouchableOpacity >),
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

  constructor(props) {
    super(props)
  }

  componentDidMount = () => {
    this.fetchAppointmentTypes();

    this.props.navigation.setParams({
      handleAddButton: this.handleAddButton.bind(this)
    })
  }

  /**
    * Fetch Data from API, saving to Redux
    */
   fetchAppointmentTypes = () => {
    const { member, fetchAppointmentTypes, showError, match } = this.props
    const serviceCategoryId = (match && match.params && match.params.id) ? match.params.id : null
    
    return fetchAppointmentTypes(member.SiteId, serviceCategoryId, 10, 0)
      .catch((err) => {
        console.log(`Error: ${err}`)
        return showError(err)
      })
  }

  render = () => {
    const { appointmentTypes, match } = this.props

    const id = (match && match.params && match.params.id) ? match.params.id : null
    
    let listViewData = appointmentTypes.appointmentTypes ? appointmentTypes.appointmentTypes.Data : null

    return (

      <List
        headerTitle='Appointment types'
        navigation={this.props.navigation}
        data={listViewData}
        renderItem={this._renderRow.bind(this)}
        keyExtractor={(item, idx) => item.Id}
        contentContainerStyle={styles.listContent}
        showsVerticalScrollIndicator={false}
        reFetch={() => this.fetchAppointmentTypes()}
        error={appointmentTypes.error}
        loading={appointmentTypes.loading}
      />
    )
  }

  _renderRow({ item }) {
    return (
      <ListItem
        name={item.Name}
        title={item.Description}
        onPress={() => {
          const { navigation, setSelectedAppointmentType } = this.props
          setSelectedAppointmentType(item)
          navigation.navigate('AppointmentType', { AppointmentType: item })
        }}
        onPressEdit={() => {
          const { navigation, setSelectedAppointmentType } = this.props
          setSelectedAppointmentType(item)
          navigation.navigate('AppointmentType', { AppointmentType: item })
        }}
        onPressRemove={() => {
          const { navigation } = this.props
          //   navigation.navigate('AppointmentListing', { id: item.id })
        }} />
    )
  }

  handleAddButton() {
    navigation.navigate('AppointmentType')
  }
}

const mapStateToProps = (state, props) => ({
  member: state.member || {},
  appointmentTypes: state.appointmentTypes || {},
  match: props.navigation.state
})

const mapDispatchToProps = {
  fetchAppointmentTypes: getAppointmentTypes,
  showError: setError,
  setSelectedAppointmentType: setSelectedAppointmentType
}

export default connect(mapStateToProps, mapDispatchToProps)(AppointmentTypeListing)
