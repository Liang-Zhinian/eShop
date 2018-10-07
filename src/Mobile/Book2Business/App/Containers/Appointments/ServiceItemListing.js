import React, { Component } from 'react'
import { TouchableOpacity, Text, ScrollView, View, Image } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Icon } from 'native-base'
// import Icon from 'react-native-vector-icons/FontAwesome'

import { Images } from '../../Themes'
import { getServiceItems, setError } from '../../Actions/serviceItems'
import styles from './Styles/AppointmentsScreenStyle'
import List from '../../Components/List'
import ListItem from '../../Components/ListItem'

class ServiceItemListing extends Component {
  static propTypes = {
    serviceItems: PropTypes.shape({
      loading: PropTypes.bool.isRequired,
      error: PropTypes.string,
      serviceItems: PropTypes.shape({}).isRequired
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

  componentDidMount = () => {
    this.fetchServiceItems();
    
    this.props.navigation.setParams({
      handleAddButton: this.handleAddButton.bind(this)
    })
  }

    /**
      * Fetch Data from API, saving to Redux
      */
  fetchServiceItems = () => {
    const { member, fetchServiceItems, showError, match } = this.props
    const serviceCategoryId = (match && match.params && match.params.id) ? match.params.id : null
    return fetchServiceItems(member.currentLocation.SiteId, serviceCategoryId, 10, 0)
            //   .then(() => fetchMeals())
            .catch((err) => {
              console.log(`Error: ${err}`)
              return showError(err)
            })
  }

  render = () => {
    const { serviceItems, match } = this.props
    
    const id = (match && match.params && match.params.id) ? match.params.id : null

    let listViewData = serviceItems.serviceItems.Data

    return (

      <List
        headerTitle='Appointments'
        navigation={this.props.navigation}
        data={listViewData}
        renderItem={this._renderRow.bind(this)}
        keyExtractor={(item, idx) => item.Id}
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
        name={item.Name}
        title={item.Description}
        onPress={() => {
          const { navigation } = this.props
          navigation.navigate('AppointmentType', { AppointmentType: item })
        }}
        onPressEdit={() => {
          const { navigation } = this.props
          navigation.navigate('AppointmentType', { AppointmentType: item })
        }}
        onPressRemove={() => {
          const { navigation } = this.props
                //   navigation.navigate('AppointmentListing', { id: item.id })
        }} />
    )
  }

  handleAddButton(){
    const { member, fetchServiceItems, showError, match, navigation } = this.props
    const serviceCategoryId = (match && match.params && match.params.id) ? match.params.id : null

    navigation.navigate('AppointmentType')
  }
}

const mapStateToProps = (state, props) => ({
  member: state.member || {},
  serviceItems: state.serviceItems || {},
  match: props.navigation.state
})

const mapDispatchToProps = {
  fetchServiceItems: getServiceItems,
  showError: setError
}

export default connect(mapStateToProps, mapDispatchToProps)(ServiceItemListing)
