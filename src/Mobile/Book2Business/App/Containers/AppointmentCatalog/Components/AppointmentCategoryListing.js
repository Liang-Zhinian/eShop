import React, { Component } from 'react'
import { Image } from 'react-native'
import PropTypes from 'prop-types'

import { Images } from '../../../Themes'
import styles from '../Styles/AppointmentsScreenStyle'
import List from '../../../Components/List'
import ListItem from '../../../Components/ListItem'
import BackButton from '../../../Components/BackButton'

class AppointmentCategoryListing extends Component {
  static propTypes = {
    error: PropTypes.string,
    loading: PropTypes.bool.isRequired,
    appointmentCategories: PropTypes.arrayOf(PropTypes.shape()).isRequired,
    reFetch: PropTypes.func,
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

  render = () => {
    const { error, loading, appointmentCategories, reFetch } = this.props
    const keyExtractor = item => item.Id;

    console.log(appointmentCategories)
    return (
      <List
        headerTitle='Appointment Categories'
        navigation={this.props.navigation}
        data={appointmentCategories}
        renderItem={this._renderRow.bind(this)}
        keyExtractor={keyExtractor}
        contentContainerStyle={styles.listContent}
        showsVerticalScrollIndicator={false}
        reFetch={reFetch}
        error={error}
        loading={loading}
      />
    )
  }

  _renderRow({ item }) {
    return (
      <ListItem
        name={item.Name}
        title={item.Description}
        onPress={() => {
          const { navigation, setSelectedEvent } = this.props
          // setSelectedEvent(item)
          navigation.navigate('AppointmentTypeListing', { id: item.Id })
        }}
        onPressEdit={() => {
          const { navigation, setSelectedEvent } = this.props
          // setSelectedEvent(item)
          navigation.navigate('AppointmentTypeListing', { id: item.Id })
        }}
        onPressRemove={() => {
          const { navigation } = this.props
          navigation.navigate('AppointmentTypeListing', { id: item.Id })
        }} />
    )
  }
}

export default AppointmentCategoryListing
