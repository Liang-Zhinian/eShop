import React, {Component} from 'react'
import {
    Animated,
    Platform,
    Keyboard
} from 'react-native'

export default class AvoidKeyboard extends Component {
  constructor (props) {
    super(props)
    this.state = {
      top: new Animated.Value(0)
    }

    this.onKeyboardWillShow = this._onKeyboardWillShow.bind(this)
    this.onKeyboardWillHide = this._onKeyboardWillHide.bind(this)
  }

    // component life cycle
  componentWillMount () {
    if (Platform.OS === 'ios') {
      this.keyboardWillShowListener = Keyboard.addListener('keyboardWillShow', this.onKeyboardWillShow)
      this.keyboardWillHideListener = Keyboard.addListener('keyboardWillHide', this.onKeyboardWillHide)
    }
  }

  componentWillUnount () {
    if (Platform.OS === 'ios') {
      this.keyboardWillShowListener.remove()
      this.keyboardWillHideListener.remove()
    }
  }

  render () {
    let {
            style,
            children,
            ...props
        } = this.props

        // const { container } = styles;

    return (
      <Animated.View style={[{position: 'relative', top: this.state.top}, style]} {...props}>
        {children}
      </Animated.View>
    )
  }

    // keyboard event handlers
  _onKeyboardWillShow (e) {
    Animated.timing(this.state.top, {
      toValue: -(e.startCoordinates.height),
      duration: e.duration
    }).start()
  }

  _onKeyboardWillHide (e) {
    Animated.timing(this.state.top, {
      toValue: 0,
      duration: e.duration
    }).start()
  }
}
