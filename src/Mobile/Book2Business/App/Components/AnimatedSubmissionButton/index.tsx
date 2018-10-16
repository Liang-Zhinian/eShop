import React, { Component } from 'react';
import PropTypes from 'prop-types';
import {
  StyleSheet,
  TouchableOpacity,
  Text,
  Animated,
  Easing,
  Image,
  Alert,
  View,
  Dimensions
} from 'react-native';

import spinner from './Images/loading.gif';

const DEVICE_WIDTH = Dimensions.get('window').width;
const DEVICE_HEIGHT = Dimensions.get('window').height;
const MARGIN = 40;

interface ButtonSubmitProps {
  onPress(callback): void
  onSucceed ():void
  onFailed ():void
}

interface ButtonSubmitState {
  isLoading: boolean
}

export default class ButtonSubmit extends Component<ButtonSubmitProps, ButtonSubmitState> {
  constructor(props) {
    super(props);

    this.state = {
      isLoading: false,
    };

    this.buttonAnimated = new Animated.Value(0);
    this.growAnimated = new Animated.Value(0);
    this._onPress = this._onPress.bind(this);
  }

  buttonAnimated = new Animated.Value(0);
  growAnimated = new Animated.Value(0);

  componentWillReceiveProps(nextProps) {
    // if (nextProps.loading === true) {
    //   this._startAnimation()
    // } else {
    //   // setTimeout(() => {
    //   //   this._onGrow();
    //   // }, 2000);
    //   this._stopAnimation()
    // }
  }

  _startAnimation(callback?: () => void) {
    this.setState({ isLoading: true });
    Animated.timing(this.buttonAnimated, {
      toValue: 1,
      duration: 200,
      easing: Easing.linear,
    }).start(callback);
  }

  _stopAnimation() {
    this.setState({ isLoading: false });
    this.buttonAnimated.setValue(0);
    this.growAnimated.setValue(0);
  }

  _onPress() {
    if (this.state.isLoading) return;

    this._startAnimation(() => {
      this.props.onPress && this.props.onPress(this._onSucceed.bind(this));
    })

    setTimeout(() => {
      if (!this.state.isLoading) return;
      this._stopAnimation()
      this.props.onFailed && this.props.onFailed()
    }, 5000);
  }

  _onSucceed() {
    setTimeout(() => {
      this._onGrow();
    }, 2000);

    this._stopAnimation()
  }

  _onGrow() {
    Animated.timing(this.growAnimated, {
      toValue: 1,
      duration: 200,
      easing: Easing.linear,
    }).start(this.props.onSucceed);
  }

  render() {
    const changeWidth = this.buttonAnimated.interpolate({
      inputRange: [0, 1],
      outputRange: [DEVICE_WIDTH - MARGIN, MARGIN],
    });
    const changeScale = this.growAnimated.interpolate({
      inputRange: [0, 1],
      outputRange: [1, MARGIN],
    });

    return (
      <View style={styles.container}>
        <Animated.View style={{ width: changeWidth }}>
          <TouchableOpacity
            style={styles.button}
            onPress={this._onPress}
            activeOpacity={1}>
            {this.state.isLoading ? (
              <Image source={spinner} style={styles.image} />
            ) : (
                <Text style={styles.text}>SAVE</Text>
              )}
          </TouchableOpacity>
          <Animated.View
            style={[styles.circle, { transform: [{ scale: changeScale }] }]}
          />
        </Animated.View>
      </View>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    // top: -95,
    alignItems: 'center',
    justifyContent: 'flex-start',
  },
  button: {
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#F035E0',
    height: MARGIN,
    borderRadius: 20,
    zIndex: 100,
  },
  circle: {
    height: MARGIN,
    width: MARGIN,
    marginTop: -MARGIN,
    borderWidth: 1,
    borderColor: '#F035E0',
    borderRadius: 100,
    alignSelf: 'center',
    zIndex: 99,
    backgroundColor: '#F035E0',
  },
  text: {
    color: 'white',
    backgroundColor: 'transparent',
  },
  image: {
    width: 24,
    height: 24,
  },
});