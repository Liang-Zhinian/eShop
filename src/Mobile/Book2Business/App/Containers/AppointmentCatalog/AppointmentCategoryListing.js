import React, { Component } from 'react'
import { Image } from 'react-native'
import { connect } from 'react-redux'
import PropTypes from 'prop-types'

import { Images } from '../../Themes'
import styles from './Styles/AppointmentsScreenStyle'
import List from '../../Components/List/List'
import ListItem from '../../Components/List/ListItem'
import BackButton from '../../Components/BackButton'

import { setSelectedAppointmentCategory, getAppointmentCategories, setError } from '../../Actions/appointmentCategories'

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
    setSelectedAppointmentCategory: PropTypes.func.isRequired,
    showError: PropTypes.func.isRequired,
  }

  static defaultProps = {
    error: null,
    reFetch: null,
  }

  static navigationOptions = ({ navigation }) => ({
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
    title: 'Appointment Categories',
    headerMode: 'screen',
    headerBackTitleVisible: true,
    headerLeft: (
      <BackButton navigation={navigation} />
    )
  })

  constructor(props) {
    super(props)
    this.state = {
    }
  }

  componentDidMount = () => this.fetchAppointmentCategories();

  render = () => {
    const { appointmentCategories, navigation } = this.props;
    
    const keyExtractor = item => item.Id;

    return (
      <List
        headerTitle='Appointment categories'
        data={appointmentCategories.appointmentCategories.Data}
        renderItem={this._renderRow.bind(this)}
        keyExtractor={keyExtractor}
        contentContainerStyle={styles.listContent}
        showsVerticalScrollIndicator={false}
        reFetch={this.fetchAppointmentCategories}
        error={appointmentCategories.error}
        loading={appointmentCategories.loading}
      />
    )
  }

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

  _renderRow({ item }) {
    return (
      <ListItem
        name={item.Name}
        title={item.Description}
        onPress={() => {
          const { navigation, setSelectedAppointmentCategory } = this.props
          setSelectedAppointmentCategory(item)
          navigation.navigate('AppointmentTypeListing', { id: item.Id })
        }}
        onPressEdit={() => {
          const { navigation, setSelectedAppointmentCategory } = this.props
          setSelectedAppointmentCategory(item)
          navigation.navigate('AppointmentTypeListing', { id: item.Id })
        }}
        onPressRemove={() => {
          const { navigation } = this.props
          navigation.navigate('AppointmentTypeListing', { id: item.Id })
        }} />
    )
  }
}

const mapStateToProps = state => ({
  member: state.member || {},
  appointmentCategories: state.appointmentCategories || {}
})

const mapDispatchToProps = {
  fetchAppointmentCategories: getAppointmentCategories,
  showError: setError,
  setSelectedAppointmentCategory: setSelectedAppointmentCategory
}

export default connect(mapStateToProps, mapDispatchToProps)(AppointmentCategoryListing);

