import React, { Component } from 'react'
import { TouchableOpacity, Text, Image, View, StyleSheet, FlatList } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Icon } from 'native-base'

import { Images } from '../../Themes'
import { getStaffSchedules, setError } from '../../Actions/staffSchedules'
import Hamburger from '../../Components/Hamburger'
import AnimatedContainerWithNavbar from '../../Components/AnimatedContainerWithNavbar'
import GradientView from '../../Components/GradientView'

class StaffScheduleListing extends Component {
  static propTypes = {
    isLoading: PropTypes.bool.isRequired
  }

  static defaultProps = {
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

  componentDidMount = () => {
    const { navigation, member, getStaffSchedules, selectedServiceItem } = this.props
    navigation.setParams({
      handleAddButton: this.handleAddButton.bind(this)
    })

    getStaffSchedules(selectedServiceItem.SiteId, member.currentLocation.Id, member.Id, selectedServiceItem.Id)
  }

  state = {
    errorMessage: null,
    successMessage: null
  }

  animatedContainerWithNavbar = null;

  componentWillReceiveProps(newProps) {
    const { staffSchedules } = newProps

    this.setState({ data: staffSchedules.staffSchedules })
  }

  render = () => {
    const { data } = this.state
    const { navigation } = this.props

    return (
      <GradientView style={[styles.linearGradient, { flex: 1 }]}>
        <FlatList
          ref='scheduleList'
          data={data}
          extraData={this.props}
          renderItem={this.renderItem}
          keyExtractor={(item, idx) => item.eventStart}
          contentContainerStyle={styles.listContent}
          getItemLayout={this.getItemLayout}
          showsVerticalScrollIndicator={false}
        />
      </GradientView>
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
      <View />
    )
  }

  handleAddButton() {
    const { navigation } = this.props
    navigation.navigate('StaffSchedule')
  }
}

const mapStateToProps = state => ({
  member: state.member || {},
  selectedServiceItem: state.selectedServiceItem || {},
  staffSchedules: state.staffSchedules || {},
  isLoading: state.status.loading || false
})

const mapDispatchToProps = {
  getStaffSchedules: getStaffSchedules,
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
