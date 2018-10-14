import React, { Component } from 'react'
import { TouchableOpacity, Text, Image, View } from 'react-native'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Icon } from 'native-base'
// import { Actions } from 'react-native-router-flux';

import { Images } from '../../Themes'
import Layout from './Components/UpdateContact'

class AddStaff extends Component {
  static propTypes = {
    // Layout: PropTypes.func.isRequired,
    locations: PropTypes.shape({}).isRequired,
    onFormSubmit: PropTypes.func.isRequired,
    isLoading: PropTypes.bool.isRequired
  }

  static defaultProps = {
    match: null
  }

  static navigationOptions = {
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
    headerTitle: 'Update Contact'
  }

  componentWillMount = () => {
    // this.setState({ errorMessage: this.props.locations.error })
  }

  state = {
    errorMessage: null,
    successMessage: null
  }

  onFormSubmit = (data) => {
    // const { onFormSubmit } = this.props;
    // return onFormSubmit(data)
    //   .then(mes => this.setState({ successMessage: mes, errorMessage: null }))
    //   .catch((err) => { this.setState({ errorMessage: err, successMessage: null }); throw err; });
  }

  render = () => {
    const {
      locations,
      // Layout,
      isLoading
    } = this.props

    console.log('UpdateLocationInfo', this)

    const { successMessage, errorMessage } = this.state

    // return (<View />)

    return (
      <Layout
        location={locations.currentLocation}
        loading={isLoading}
        error={errorMessage}
        success={successMessage}
        onFormSubmit={this.onFormSubmit}
      />
    )
  }
}

const mapStateToProps = state => ({
  locations: null || state.locations || {},
  isLoading: false || state.status.loading || false
})

const mapDispatchToProps = {
  onFormSubmit: function () { }
  // showError: setError,
}

export default connect(mapStateToProps, mapDispatchToProps)(AddStaff)
