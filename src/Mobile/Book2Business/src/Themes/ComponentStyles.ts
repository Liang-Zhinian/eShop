import Fonts from './Fonts'
import Metrics from './Metrics'
import Colors from './Colors'

// This file is for a reusable grouping of Theme items.
// Similar to an XML fragment layout in Android

const Gradients = {
  AquaSplash: ['#80d0c7', '#13547a'],
  Purple: [Colors.purple, Colors.darkPurple],
  EternalConstance: ['#537895', '#09203f'],
  ViciousStance: ['#485563', '#29323c']
}

const defaultGradient = Gradients.ViciousStance

export interface ComponentStylesType {
  gradient
  navBar
  tabBar
  statusBar
  card
}

const ComponentStyles: ComponentStylesType = {
  gradient: {
    colors: defaultGradient
  },
  navBar: {
    colors: [defaultGradient[0], '#521655', defaultGradient[1]]
  },
  tabBar: {
    backgroundColor: defaultGradient[1]
  },
  statusBar: {
    backgroundColor: '#09203f'
  },
  card: {
    backgroundColor: defaultGradient[1]
  }
}

export default ComponentStyles