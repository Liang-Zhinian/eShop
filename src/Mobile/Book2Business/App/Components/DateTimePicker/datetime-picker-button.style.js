import { StyleSheet } from "react-native";
import { ApplicationStyles, Metrics, Colors, Fonts } from '../../Themes/'

export default StyleSheet.create({
  container: {
    // flex: 1,
    // justifyContent: "center",
    // alignItems: "center"
  },
  button: {
    flex: 1,
    flexDirection: 'row',
    justifyContent: "center",
    alignItems: 'center',
    paddingVertical: 16,
    // margin: 16,
  },
  label: {
    fontFamily: 'Montserrat-Medium',
    fontSize: 15,
    lineHeight: 23,
    letterSpacing: 0.5,
    color: Colors.darkPurple,
    marginRight: 10,
  },
  text: {
    marginVertical: 10
  },
  icon: {
    // marginHorizontal: 10,
    // tintColor: 'black',
  },
});