import React, { Component } from 'react'
import { TouchableOpacity, Text, ScrollView, View, Image, Button } from 'react-native'

import { Images } from '../Themes'
import styles from './Styles/BackButtonStyle'

export default ({navigation}) => (
  <TouchableOpacity style={styles.backButton} onPress={() => navigation.goBack(null)}>
    <Image style={styles.backButtonIcon} source={Images.arrowIcon} />
  </TouchableOpacity>
)
