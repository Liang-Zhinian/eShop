import React from 'react'
import PropTypes from 'prop-types'
import { View, TouchableOpacity, Image, Text, } from 'react-native'
import Spacer from './Spacer'
import LinearGradient from 'react-native-linear-gradient'
import styles from './Styles/HeaderStyle'
import { Images, Colors } from '../Themes'
import BackButton from './BackButton'

export const Header = ({ title, goBack, left, right }) => (
  <View style={{ flex: 1, flexDirection: 'row' }}>
    <View style={{
      flex: 1,
      flexDirection: 'row',
      alignItems: 'center',
      justifyContent: 'center'
    }}>
      {left !== null && <BackButton onPress={goBack} />}
    </View>
    <View style={{
      flex: 4,
      flexDirection: 'row',
      alignItems: 'center',
      justifyContent: 'center'
    }}>
      <Text style={styles.title}>
        {title}
      </Text>
    </View>
    <View style={{
      flex: 1,
      flexDirection: 'row',
      alignItems: 'center',
      justifyContent: 'center'
    }}>

    </View>
  </View>
)

Header.propTypes = {
  title: PropTypes.string,
  goBack: PropTypes.func,
  right: PropTypes.any,
  left: PropTypes.any,
}

Header.defaultProps = {
  title: 'Missing title',
  right: undefined,
  left: undefined,
  goBack: () => { }
}

// export default Header;

const GradientHeader = props => {
  const { title, content, children } = props

  return (
    <LinearGradient
      start={{ x: 0, y: 0 }}
      end={{ x: 1, y: 1 }}
      locations={[0.0, 0.38, 1.0]}
      colors={['#46114E', '#521655', '#571757']}
      style={styles.headerGradient}
    >
      <View style={styles.body}>
        {children}
      </View>
    </LinearGradient>
  )
}

export default GradientHeader
