import React from 'react'
import { Image, TouchableOpacity } from 'react-native'
import FontAwesome from 'react-native-vector-icons/FontAwesome'
import styles from './Styles/SocialMediaButtonStyles'
import ExamplesRegistry from '../Services/ExamplesRegistry'
import { Images } from '../Themes'

// Example
ExamplesRegistry.addComponentExample('EditButton', () =>
  <EditButton
    network='twitter'
    onPress={() => window.alert('Lets Get Social AF')}
  />
)

interface EditButtonProps {
  style?: StyleSheet
  network: 'twitter' | 'github'
  spacing?: 'left' | 'right'
  onPress (): void
}

const EditButton = (props: EditButtonProps) => {
  const { network, style, spacing, onPress } = props
  const imageSource = network === 'twitter' ? Images.twitterIcon : Images.githubIcon
  const spacingShim = spacing === 'right' ? 'right' : 'left'

  return (
    <TouchableOpacity
      style={[styles[spacingShim], style]}
      onPress={onPress}
      hitSlop={{ top: 10, left: 10, bottom: 10, right: 10 }}
    >
      {/* <Image source={imageSource} /> */}
      <FontAwesome name='edit' size={25} />
    </TouchableOpacity>
  )
}

export default EditButton
