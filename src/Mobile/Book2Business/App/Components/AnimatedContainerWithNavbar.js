
import React, { Component } from 'react'
import {
    AppRegistry,
    StyleSheet,
    Text,
    View,
    Animated,
    ListView,
    Image,
    Dimensions,
    AlertIOS,
    TouchableOpacity,
    TouchableHighlight,
    TouchableWithoutFeedback
} from 'react-native'

const { width, height } = Dimensions.get('window')

import recipes from './data'
import NavBar from './Navbar'
import Icon from 'react-native-vector-icons/FontAwesome'

export default class AnimatedContainerWithNavbar extends Component {
  constructor () {
    super()
    const ds = new ListView.DataSource({ rowHasChanged: (r1, r2) => r1 !== r2 })

    this.state = {
      like: false,
      loaded: false,
      anim: new Animated.Value(0),
      anim_rotateY: new Animated.Value(0),
      anim_translateX: new Animated.Value(width),
      isMenuOpen: false,
      menuAnimate: new Animated.Value(0),
      dataSource: ds.cloneWithRows(recipes)
            // icon: 'bars'
    }
  }

  componentWillReceiveProps (nextProps) {
        // this.setState({ isMenuOpen: nextProps || false });
  }

  componentDidMount () {
    this.setState({
      loaded: true
    })

        // this.props.navigation.setParams({
        //     icon: 'bars',
        //     pressMenu: this.toggleMenu.bind(this)
        // })
  }

  toggleMenu () {
        // let icon = 'times';
        // let pressMenu = this.showMenu.bind(this);
    if (this.state.isMenuOpen) {
            // icon = 'bars';
      this.closeMenu()
    } else {
            // icon = 'times';
      this.showMenu()
    }

        // this.props.navigation.setParams({
        //     icon
        // })
  }

  showMenu () {
    if (this.state.isMenuOpen) {
      this.setState({ isMenuOpen: false })
      Animated.parallel([
        Animated.timing(
                    this.state.anim_translateX, {
                      toValue: width
                    }),
        Animated.timing(
                    this.state.anim_rotateY, {
                      toValue: 0
                    })
      ]).start()
    } else {
      this.setState({ isMenuOpen: true })
      Animated.parallel([
        Animated.timing(
                    this.state.anim_translateX, {
                      toValue: width * 0.60
                    }),
        Animated.timing(
                    this.state.anim_rotateY, {
                      toValue: 1
                    }),
        Animated.timing(
                    this.state.menuAnimate, {
                      toValue: 1,
                      duration: 800
                    })
      ]).start()
    }
  }
  closeMenu () {
    this.setState({ isMenuOpen: false })
    Animated.parallel([
      Animated.timing(
                this.state.anim_translateX, {
                  toValue: width
                }),
      Animated.timing(
                this.state.anim_rotateY, {
                  toValue: 0
                }),
      Animated.timing(
                this.state.menuAnimate, {
                  toValue: 0,
                  duration: 300
                })
    ]).start()
  }
  render () {
    const menuPosition = this.props.menuPosition || 'left'

    let anim_translateX = {
      inputRange: [0, width],
      outputRange: [-width * 1.5, 0]
    }
    let anim_rotateY = {
      inputRange: [0, 1],
      outputRange: ['0deg', '10deg']
    }

    if (this.props.menuPosition == 'left') {
      anim_translateX = {
        inputRange: [0, width],
        outputRange: [width, 0]
      }

      anim_rotateY = {
        inputRange: [0, 1],
        outputRange: ['0deg', '-10deg']
      }
    }

    return (
      <View style={styles.container}>
        <Animated.View style={[styles.content, {
          width: width,
          backgroundColor: 'gray',
          flex: 1,
          transform: [{ perspective: 450 },
            {
              translateX: this.state.anim_translateX.interpolate(anim_translateX)
            },
            {
              rotateY: this.state.anim_rotateY.interpolate(anim_rotateY)
            }
          ]
        }]}>
          {/* this.state.isMenuOpen ? <NavBar icon="times" pressMenu={this.closeMenu.bind(this)} />
                        : <NavBar icon="bars" pressMenu={this.showMenu.bind(this)} />
            */}

          {this.state.isMenuOpen ? this.props.content
                        : (<TouchableWithoutFeedback>
                          {this.props.content}
                        </TouchableWithoutFeedback>)}
        </Animated.View>
        <Animated.View style={{ opacity: this.state.menuAnimate, position: 'absolute', width: 200, right: 0, top: 120, backgroundColor: 'transparent' }}>
          {/* <Text style={[styles.textFooter, styles.menutext]}>Home</Text>
                    <Text style={[styles.textFooter, styles.menutext]}>New Recipe</Text>
                    <Text style={[styles.textFooter, styles.menutext]}>Recipes</Text>
                    <Text style={[styles.textFooter, styles.menutext]}>Profile</Text>
                    <Text style={[styles.textFooter, styles.menutext]}>Settings</Text> */}
          {this.props.menu}
        </Animated.View>
      </View>
    )
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#555566'
        // paddingTop: 20
  },
  content: {
    zIndex: 3
  },
  containerRow: {
    marginBottom: 10
  },
  listView: {
    marginHorizontal: 10
  },
  imageFooter: {
    backgroundColor: '#555566',
    flexDirection: 'row',
    paddingHorizontal: 7,
    paddingVertical: 7
  },
  textFooter: {
    color: '#fff'
  },
  textFooterBold: {
    fontWeight: 'bold'
  },
  imageAvatar: {
    width: 50,
    height: 50,
    borderRadius: 25,
    marginRight: 7
  },
  textFooterLight: {
    fontWeight: '100'
  },
  textFooterLittle: {
    fontSize: 11
  },
  menutext: {
    fontSize: 20,
    padding: 10
  },
  menu: {
    width: width,
    height: height,
    flex: 1,
    position: 'absolute',
    left: 0,
    top: 0,
    backgroundColor: '#ff00ff'
  },
  menulist: {
    width: 200,
    position: 'absolute',
    right: 0,
    top: 100
  }
})
