import Fonts from './Fonts'
import Metrics from './Metrics'
import Colors from './Colors'

// This file is for a reusable grouping of Theme items.
// Similar to an XML fragment layout in Android

const Gradients = {
  AquaSplash: ['#80d0c7', '#13547a'],
  Purple: [Colors.purple, Colors.darkPurple],
  EternalConstance: ['#537895', '#09203f'],
}

export interface ComponentStylesType {
  gradient
  navBar
  tabBar
  statusBar
  card
}

const ComponentStyles: ComponentStylesType = {
  gradient: {
    colors: Gradients.EternalConstance
  },
  navBar: {
    colors: ['#537895', '#521655', '#09203f'] // ['#46114E', '#521655', '#571757']
  },
  tabBar: {
    backgroundColor: '#09203f',
  },
  statusBar: {},
  card: {
    backgroundColor: '#09203f' // Colors.darkPurple
  }
}

export default ComponentStyles
