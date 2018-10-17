import React from 'react'
import { TouchableOpacity, Text } from 'react-native'

import styles from './AddButtonStyle'

export default ({ onPress }) => (
  <TouchableOpacity style={{ marginRight: 20 }} onPress={onPress} >
    <Text style={styles.addButtonText}>Add</Text>
  </TouchableOpacity >
)
