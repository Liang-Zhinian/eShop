import React from 'react'
import {
  Text,
  TextInput,
  TouchableOpacity,
  ViewPropTypes,
  StyleSheet
} from 'react-native'
import PropTypes from 'prop-types'

import styles from './Styles'

/*
 * @property {string} propTypes - Types for component stylization
 * By default `TextInput` supports following types: `bordered`, `rounded`, `form`, `topLabel`
 * @property {TextInput.props} props - Regular `TextInput` props
 * will be passed to internal `TextInput` component
 * @property {style} style - Style for TouchableOpacity wrapping input and label
 * @property {string || function} label - Label displayed with input.
 * When label is clicked input gets focus. function should return React component
 * @property {style} labelStyle - Style applied to label
 * @property {style} inputStyle - Style applied to text input
 */
export default class xTextInput extends React.Component {
  static propTypes = {
    editable: PropTypes.bool,
    label: PropTypes.oneOfType([
      PropTypes.string,
      PropTypes.element
    ]),
    style: ViewPropTypes.style,
    inputStyle: ViewPropTypes.style
  };
  static defaultProps = {
    editable: true,
    label: null,
    style: null,
    inputStyle: null
  };
  componentName = 'TextInput';
  typeMapping = {
    container: {
      underlineWidth: 'borderBottomWidth',
      underlineColor: 'borderBottomColor'
    },
    input: {
      color: 'color',
      inputBackgroundColor: 'backgroundColor',
      placeholderTextColor: 'placeholderTextColor'
    },
    label: {
      labelColor: 'color',
      labelFontSize: 'fontSize'
    }
  };

  focusInput = () => {
    if (this.props.editable) {
      this.inputRef.focus()
    }
  };

  renderLabel(label, labelStyle) {
    if (typeof label === 'string') {
      return (
        <Text style={labelStyle} onPress={this.focusInput}>{label}</Text>
      )
    }
    return React.cloneElement(label, {
      onPress: (e) => {
        this.inputRef.focus()
        if (label.props.onPress) {
          label.props.onPress(e)
        }
      },
      style: [labelStyle, label.props.style]
    })
  }

  render() {
    const {
      style,
      label,
      inputStyle,
      ...inputProps
    } = this.props
    const { container: boxStyle, input, label: labelS } = styles
    // const placeholderColor = this.extractNonStyleValue(input, 'placeholderTextColor');
    inputProps.labelStyle = [labelS, inputProps.labelStyle]

    inputProps.style = [input, inputStyle, {textAlign: 'right'}]
    // inputProps.placeholderTextColor = placeholderColor;
    // boxStyle.push(style);
    return (
      <TouchableOpacity activeOpacity={1} onPress={this.focusInput} style={[boxStyle]}>
        {label && this.renderLabel(label, inputProps.labelStyle)}
        <TextInput
          underlineColorAndroid='transparent'
          ref={(inputValue) => {
            this.inputRef = inputValue
          }}
          {...inputProps}
        />
      </TouchableOpacity>
    )
  }
}
