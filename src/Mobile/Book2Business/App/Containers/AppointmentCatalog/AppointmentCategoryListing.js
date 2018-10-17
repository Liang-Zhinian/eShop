import React, { Component } from 'react'
import { Image } from 'react-native'
import { connect } from 'react-redux'
import PropTypes from 'prop-types'

import { Images } from '../../Themes'
import styles from './Styles/AppointmentsScreenStyle'
import List from '../../Components/List/List'
import ListItem from '../../Components/List/ListItem'
import AddButton from '../../Components/AddButton'

import { setSelectedAppointmentCategory, getAppointmentCategories, setError } from '../../Actions/appointmentCategories'

class AppointmentCategoryListing extends Component {
  static propTypes = {
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

  static navigationOptions = ({ navigation }) => {

    const { handleAddButton } = navigation.state.params || {
      handleAddButton: () => null,
    }

    return {
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
      headerRight: (<AddButton onPress={handleAddButton} />),
    }
  }

  constructor(props) {
    super(props)
    this.state = {
    }
  }

  componentDidMount = () => {
    this.fetchAppointmentCategories();

    this.props.navigation.setParams({
      handleAddButton: this.handleAddButton.bind(this)
    })
  }

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
    console.log(member)

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
          navigation.navigate('AppointmentCategory', {ActionType: 'Update'})
        }}
        onPressRemove={() => {
          const { navigation } = this.props
          // navigation.navigate('AppointmentTypeListing', { id: item.Id })
        }} />
    )
  }

  handleAddButton() {
    this.props.navigation.navigate('AppointmentCategory', {ActionType: 'Add'})
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

