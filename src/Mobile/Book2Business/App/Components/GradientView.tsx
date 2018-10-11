import React from 'react'
import LinearGradient from 'react-native-linear-gradient'
import { ComponentStyles } from '../Themes'

export default (props) => {
  const gradient = ComponentStyles.gradient.colors
  return (
    <LinearGradient colors={gradient} style={props.style}>
      {props.children}
    </LinearGradient>
  )
}