import React from 'react'
import PropTypes from 'prop-types'
import { View, TouchableOpacity, Image } from 'react-native'
import { Text, H1 } from 'native-base'
import Spacer from './Spacer'
import LinearGradient from 'react-native-linear-gradient'
import styles from './Styles/HeaderStyle'
import { Images } from '../Themes'

const Header = ({ title, content, goBack }) => (
  <View style={{ flex: 1 }}>

    <TouchableOpacity style={styles.backButton} onPress={() => {
      goBack()
    }}>
      <Image style={styles.backButtonIcon} source={Images.arrowIcon} />
      <Text style={styles.backButtonText}>Back</Text>
    </TouchableOpacity>
    <View style={{
      flex: 1,
      flexDirection: 'column',
      alignItems: 'center',
      justifyContent: 'space-evenly'
    }}>
      <Spacer size={25} />
      <H1>
        {title}
      </H1>
      {!!content && (
        <View style={{
          flex: 1
        }}>
          <Spacer size={10} />
          <Text>
            {content}
          </Text>
        </View>
      )}
      <Spacer size={25} />
    </View>
  </View>
)

Header.propTypes = {
  title: PropTypes.string,
  content: PropTypes.string,
  goBack: PropTypes.func
}

Header.defaultProps = {
  title: 'Missing title',
  content: '',
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
      <View style={styles.dayToggle}>
        {children}
      </View>
    </LinearGradient>
  )
}

export default GradientHeader
