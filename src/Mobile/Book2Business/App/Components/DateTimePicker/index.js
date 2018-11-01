import { Platform } from "react-native";
import DateTimePickerAndroid from "./datetime-picker.android";
import DateTimePickerIOS from "./datetime-picker.iOS";
import DateTimePickerButton from './datetime-picker-button'

const IS_ANDROID = Platform.OS === "android";

export default (IS_ANDROID ? DateTimePickerAndroid : DateTimePickerIOS);

export { DateTimePickerButton }