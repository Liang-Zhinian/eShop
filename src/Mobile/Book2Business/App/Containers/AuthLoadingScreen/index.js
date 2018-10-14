import React from 'react'
import {
  StatusBar,
  StyleSheet,
  View,
  Text
} from 'react-native'
import { connect } from 'react-redux'
import Wallpaper from './components/Wallpaper'
import PropTypes from 'prop-types'

class AuthLoadingScreen extends React.Component {
  static propTypes = {
    member: PropTypes.shape({}).isRequired,
  }

  printProps(title = 'auth loading ...') {
    const { member } = this.props
    console.log('printProps ' + title, member)
  }

  componentWillReceiveProps(nextProps) {
    const { member } = nextProps
    this.printProps()
    if (member) {
      if (!member.token) {
        this.props.navigation.navigate('Auth')
      }
      else { this.props.navigation.navigate('App') }
    }
  }

  // Render any loading content that you like here
  render() {
    return (
      <Wallpaper>
        <StatusBar barStyle='default' />
        <View style={styles.container}>
          <Text style={styles.logo}>Book2</Text></View>
      </Wallpaper>
    )
  }
}

const mapStateToProps = (state, props) => ({
  member: state.member || {},
})

const mapDispatchToProps = (dispatch) => ({
})

export default connect(mapStateToProps, mapDispatchToProps)(AuthLoadingScreen)

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center'
  },
  logo: {
    fontSize: 40
  }
})
