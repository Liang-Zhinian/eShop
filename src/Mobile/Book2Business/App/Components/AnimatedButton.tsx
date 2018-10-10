import React from 'react'
import { View, Text, Image, TouchableWithoutFeedback, LayoutAnimation, Animated } from 'react-native'
import styles from './Styles/AnimatedButtonStyle'
import FadeIn from 'react-native-fade-in-image'
import { Images } from '../Themes'

interface ButtonProps {
  onPress (): void
}

interface ButtonState {
  animatedSize: Animated.Value
}

export default class AnimatedButton extends React.Component<ButtonProps, ButtonState> {
  constructor (props) {
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

  render () {
    const {children} = this.props

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
        >
          <Animated.View style={containerStyles}>
            <View style={styles.info}>
              {children}
            </View>
          </Animated.View>
        </TouchableWithoutFeedback>
      </View>
    )
  }
}
