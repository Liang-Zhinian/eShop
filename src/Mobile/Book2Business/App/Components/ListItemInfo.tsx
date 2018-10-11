import React from 'react'
import { View, Text } from 'react-native'
import SocialMediaButton from './SocialMediaButton'
import styles from './Styles/ListItemInfoStyle'
import EditButton from './EditButton'
import RemoveButton from './RemoveButton'

interface ListItemInfoProps {
  onPressEdit (): void
  onPressRemove (): void
}

const ListItemInfo = (props: ListItemInfoProps) => {

  return (
    <View style={styles.container}>
      <View style={styles.details}>
        <View style={styles.detail}>
          <Text style={styles.detailLabel}>
            Start
          </Text>
          <Text style={styles.detailText}>

          </Text>
        </View>
        <View style={styles.detail}>
          <Text style={styles.detailLabel}>
            Duration
          </Text>
          <Text style={styles.detailText}>

          </Text>
        </View>
      </View>
      <View style={styles.socialButtons}>
        {props.onPressEdit && <EditButton network='twitter' onPress={props.onPressEdit} />}
        {props.onPressRemove && <RemoveButton network='github' onPress={props.onPressRemove} />}
      </View>
    </View>
  )
}
export default ListItemInfo
