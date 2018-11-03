import React from 'react'
import { View, Text, Image, StyleSheet } from 'react-native'
import styles from './Styles/ClientStyle'
import FadeIn from 'react-native-fade-in-image'
import AnimatedTouchable from '../../../Components/AnimatedTouchable';
import Spacer from '../../../Components/Spacer';

interface ClientProps {
  title: string
  name: string
  avatarURL: string
  onPress(): void
}

interface ClientState {
}

export default class Client_Lite extends React.Component<ClientProps, ClientState> {
  constructor(props) {
    super(props)

    this.state = {
    }
  }

  render() {
    const {
      name,
      title,
      avatarURL,
      onPress
    } = this.props

    return (
      <AnimatedTouchable onPress={onPress}
        style={{
          flexDirection: 'row',
          height: 100,
          justifyContent: 'center',
          alignItems: 'center',
          borderBottomWidth: StyleSheet.hairlineWidth,
          borderBottomColor: 'gray'
        }}>
        <FadeIn>
          <Image style={[styles.avatar, {marginHorizontal: 5}]} source={{ uri: avatarURL }} />
        </FadeIn>
        <View style={[styles.infoText, {marginHorizontal: 5}]}>
          <Text style={styles.name}>{name}</Text>
          <Text style={styles.title}>{title}</Text>
        </View>
      </AnimatedTouchable>
    )
  }
}
