import { Text, YellowBox } from 'react-native'
import DebugConfig from './DebugConfig'
import AppConfig from './AppConfig'
import './PushConfig'
import './CodePushConfig'

// Allow/disallow font-scaling in app
Text.defaultProps.allowFontScaling = AppConfig.allowTextFontScaling

if (__DEV__) {
  // If ReactNative's yellow box warnings are too much, it is possible to turn
  // it off, but the healthier approach is to fix the warnings.  =)
  console.disableYellowBox = !DebugConfig.yellowBox
}

YellowBox.ignoreWarnings(['Warning:'])
console.disableYellowBox = true