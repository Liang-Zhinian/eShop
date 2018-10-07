import Colors from './Colors'
import Fonts from './Fonts'
import Metrics from './Metrics'
import Images from './Images'
import Videos from './Videos'
import ApplicationStyles, {ScreenStylesType} from './ApplicationStyles'
import ComponentStyles from './ComponentStyles'

export { Colors, Fonts, Images, Videos, Metrics, ApplicationStyles, ScreenStylesType, ComponentStyles }

import * as Lite from './Lite'

const Purple = { Colors, Fonts, Images, Videos, Metrics, ApplicationStyles, ScreenStylesType, ComponentStyles }

export default themes = {
  Lite,
  Purple
}
