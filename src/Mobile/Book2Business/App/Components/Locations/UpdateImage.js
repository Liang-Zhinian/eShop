import React from 'react'
import {
  // AppState,
  ScrollView,
  // TouchableOpacity,
  View,
  // Image,
  // Text,
  // Linking,
  Animated
  // PanResponder,
  // LayoutAnimation
} from 'react-native'
import PropTypes from 'prop-types'
import {
  Container, Content, Body, Form, Item, Label, Input, Button
} from 'native-base'
// import Secrets from 'react-native-config'

import PurpleGradient from '../PurpleGradient'
// import RoundedButton from '../RoundedButton'
import Messages from '../Messages'
import Loading from '../Loading'
import Header from '../Header'
import Spacer from '../Spacer'
import { Images, Colors, Fonts, Metrics } from '../../Themes/'
import styles from './Styles/LocationInfoStyle'
import ImageCropperButton from './ImageCropperButton'
import MainContainer from '../../Containers/MainContainer'

class UpdateImage extends React.Component {
  static propTypes = {
    error: PropTypes.string,
    success: PropTypes.string,
    loading: PropTypes.bool,
    // onFormSubmit: PropTypes.func.isRequired,
    location: PropTypes.shape({
    }).isRequired
  }

  static defaultProps = {
    error: null,
    success: null
  }

  constructor (props) {
    super(props)
    this.state = {
      scrollY: new Animated.Value(0),
      shouldComponentUpdate: false,
      backgroundImage: Images.theArmory
    }

    this.handleChange = this.handleChange.bind(this)
    this.handleSubmit = this.handleSubmit.bind(this)
  }

  handleChange = (name, val) => {
    this.setState({
      [name]: val
    })
  }

  handleSubmit = () => {
    const { onFormSubmit } = this.props
    onFormSubmit(this.state)
      .then(() => console.log('Location Updated'))
      .catch(e => console.log(`Error: ${e}`))
  }

  renderBackground = () => {
    const height = Metrics.locationBackgroundHeight
    const { scrollY } = this.state
    return (
      <Animated.Image
        style={[styles.venue, { position: 'absolute' }, {
          width: '100%',
          height: height,
          transform: [{
            translateY: scrollY.interpolate({
              inputRange: [-height, 0, height],
              outputRange: [height, 0, 0]
            })
          }, {
            scale: scrollY.interpolate({
              inputRange: [-height, 0, height],
              outputRange: [0.9, 1, 1.5]
            })
          }]
        }]}
        source={this.state.backgroundImage}
        resizeMode='cover'
      />
    )
  }

  renderHeader = () => {
    const height = Metrics.locationBackgroundHeight - 24
    const { scrollY } = this.state
    return (
      <Animated.View style={{
        position: 'relative',
        height: height,
        padding: 0,
        opacity: scrollY.interpolate({
          inputRange: [-height, 0, height * 0.4, height * 0.9],
          outputRange: [1, 1, 1, 0]
        }),
        transform: [{
          translateY: scrollY.interpolate({
            inputRange: [-height, 0, height * 0.45, height],
            outputRange: [0, 0, height * 0.45, height * 0.4]
          })
        }]
      }}>
        {/* <View style={styles.headingContainer}>
          <Text style={styles.mainHeading}>The Armory</Text>
          <Text style={styles.address}>
            128 NW Eleventh Ave{'\n'}
            Portland, OR 97209
          </Text>
        </View>
        <View style={styles.headingContainer}>
          <RoundedButton
            text='Change background image'
            onPress={() => null}
            style={styles.slackButton}
          />
        </View> */}
      </Animated.View>
    )
  }

  render () {
    const { loading, error, success } = this.props

    const {
      name,
      description
    } = this.state

    // Loading
    if (loading) return <Loading />

    const { event } = Animated

    return (
      <PurpleGradient style={[styles.linearGradient, { flex: 1 }]}>
        <ScrollView
          ref='scrolly'
          onScroll={event([{ nativeEvent: { contentOffset: { y: this.state.scrollY } } }])}
          scrollEventThrottle={10}
          scrollEnabled={!this.state.mapViewMode}>
          <View style={styles.container}>
            {this.renderBackground()}
            {this.renderHeader()}
            <View>
              <Header
                title='Business location'
                content='Thanks for keeping your business location up to date!'
              />

              {error && <Messages message={error} />}
              {success && <Messages message={success} type='success' />}

              {/* <Form> */}

              <Spacer size={20} />

              <ImageCropperButton
                handePickButton={(image) => {
                  console.log(image)
                  this.setState({ backgroundImage: image })
                }} />

              {/* </Form> */}
            </View>
          </View>
        </ScrollView>
      </PurpleGradient>
    )
  }
}

export default UpdateImage
