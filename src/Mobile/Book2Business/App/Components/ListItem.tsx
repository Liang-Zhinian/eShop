import React from 'react'
import { View, Text, Image, TouchableWithoutFeedback, LayoutAnimation, Animated } from 'react-native'
import styles from './Styles/ListItemStyle'
import FadeIn from 'react-native-fade-in-image'
import { Images } from '../Themes'
import ListItemInfo from './ListItemInfo'

interface ListItemProps {
  name: string
  title: string
  // avatarURL: any
  onPress (): void
  onPressEdit (): void
  onPressRemove (): void
}

interface ListItemState {
  animatedSize: Animated.Value
}

export default class Link extends React.Component<ListItemProps, ListItemState> {
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
    const {
      name,
      title
      // avatarURL,
    } = this.props

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
              <View style={styles.infoText}>
                <Text style={styles.name}>{name}</Text>
                <Text style={styles.title}>{title}</Text>
              </View>
              {/* <FadeIn>
                <Image style={styles.rightAvatar} source={Images.chevronRight} />
              </FadeIn> */}
            </View>
            <ListItemInfo
              onPressEdit={this.props.onPressEdit}
              onPressRemove={this.props.onPressRemove}
            />
          </Animated.View>
        </TouchableWithoutFeedback>
      </View>
    )
  }
}
