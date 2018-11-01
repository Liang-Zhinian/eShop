import React from 'react'
import {
  View,
  TouchableWithoutFeedback,
  Animated,
  StyleProp,
  ViewStyle
} from 'react-native'
import styles from './Styles'

interface ButtonProps {
  onPress(): void,
  style?: StyleProp<ViewStyle>
}

interface ButtonState {
  animatedSize: Animated.Value
}

export default class AnimatedTouchable extends React.Component<ButtonProps, ButtonState> {
  constructor(props) {
    super(props)

    this.state = {
      animatedSize: new Animated.Value(1)
    }
  }

  handlePressIn = () => {
    Animated.spring(this.state.animatedSize, {
      toValue: 1.05,
      useNativeDriver: true
    }).start()
  }

  handlePressOut = () => {
    Animated.spring(this.state.animatedSize, {
      toValue: 1,
      friction: 5,
      useNativeDriver: true
    }).start()
  }

  render() {
    const { children, style } = this.props

    const animatedStyle = {
      transform: [{ scale: this.state.animatedSize }]
    }

    const containerStyles = [
      styles.container,
      animatedStyle
    ]

    return (
      <View>
        <TouchableWithoutFeedback
          onPressIn={this.handlePressIn}
          onPressOut={this.handlePressOut}
          onPress={this.props.onPress}
          style={{ flex: 1, }}
        >
          <Animated.View style={[containerStyles, {  }]}>
            <View style={[styles.info, style]}>
              {children}
            </View>
          </Animated.View>
        </TouchableWithoutFeedback>
      </View>
    )
  }
}
