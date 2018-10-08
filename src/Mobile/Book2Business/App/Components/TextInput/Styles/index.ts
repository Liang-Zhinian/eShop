import { StyleSheet } from 'react-native'
import { Colors, Metrics } from '../../../Themes/'

export default StyleSheet.create({
  input: {
    flex: 1,
    alignSelf: 'center',
    marginVertical: 18,
    marginLeft: 16
  },
  container: {
    flexDirection: 'row',
    borderBottomWidth: 0.5,
    borderBottomColor: Colors.cloud,
    marginVertical: 9,
    // borderRadius: 100,
    // borderWidth: 0.5,
    // borderColor: 'gray'
    // underlineColor: theme.colors.border.solid,
  },
  label: {
    // fontSize: theme.fonts.sizes.base,
    alignSelf: 'center'
  },
  basic: {
    backgroundColor: 'transparent'
    // color: theme.colors.input.text,
    // labelColor: theme.colors.input.label,
    // placeholderTextColor: theme.colors.input.placeholder,
  },

  bordered: {
    borderRadius: 5,
    borderWidth: 0.5
    // borderColor: theme.colors.border.solid,
    // underlineColor: theme.colors.border.solid,
  }
})
