import React from 'react'
import { TouchableOpacity, Text, StyleSheet } from 'react-native'
import LinearGradient from 'react-native-linear-gradient'
import { scaleVertical } from '../Util/Scale'

const styles = StyleSheet.create({
  button: {
    alignItems: 'stretch',
    paddingVertical: 0,
    paddingHorizontal: 0,
    height: scaleVertical(40),
    borderRadius: 20
  },
  gradient: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    borderRadius: 20
        // colors: ['#46114E', '#521655', '#571757'],
  },
  text: {
    backgroundColor: 'transparent',
    color: 'white'
  }
})

export default class GradientButton extends React.Component {
  componentName = 'GradientButton';
  typeMapping = {
    button: {},
    gradient: {},
    text: {}
  };

  renderContent = (textStyle) => {
    const hasText = this.props.text === undefined
    return hasText ? this.props.children : this.renderText(textStyle)
  };

  renderText = (textStyle) => (
    <Text style={textStyle}>{this.props.text}</Text>
    )

  extractNonStyleValue = (style, attr) => {
    return styles[style][attr]
  }

  render () {
    const { button, gradient, text: textStyle } = styles
    const { style, ...restProps } = this.props
    const colors = ['#46114E', '#521655', '#571757'] // this.props.colors || this.extractNonStyleValue(gradient, 'colors');
    return (
      <TouchableOpacity
        style={[button, style]}
        {...restProps}>
        <LinearGradient
          colors={colors}
          start={{ x: 0.0, y: 0.5 }}
          end={{ x: 1, y: 0.5 }}
          style={[gradient]}>
          {this.renderContent(textStyle)}
        </LinearGradient>
      </TouchableOpacity>
    )
  }
}
