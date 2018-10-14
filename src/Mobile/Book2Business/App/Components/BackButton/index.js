import React, { Component } from 'react'
import { TouchableOpacity, Text, ScrollView, View, Image, Button } from 'react-native'

import { Images } from '../../Themes'
import styles from './BackButtonStyle'

export default ({onPress}) => (
  <TouchableOpacity style={styles.backButton} onPress={onPress}>
    <Image style={styles.backButtonIcon} source={Images.arrowIcon} />
  </TouchableOpacity>
)
