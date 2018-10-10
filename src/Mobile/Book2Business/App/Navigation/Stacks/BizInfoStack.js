import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import CreateStackNavigator from '../CreateStackNavigator'
import LocationScreen from '../../Containers/Locations/LocationScreen'
import UpdateInfoScreen from '../../Containers/Locations/UpdateLocationInfo'
import UpdateAddressScreen from '../../Containers/Locations/UpdateAddress'
import UpdateGeolocationScreen from '../../Containers/Locations/UpdateGeolocation'
import UpdateContactScreen from '../../Containers/Locations/UpdateContact'
import AdditionalImagesScreen from '../../Containers/Locations/UpdateImage'
import UpdateBackgroundScreen from '../../Containers/Locations/UpdateImage'

export default CreateStackNavigator({
  Location: { screen: LocationScreen },
  UpdateInfoScreen: { screen: UpdateInfoScreen },
  UpdateAddressScreen: { screen: UpdateAddressScreen },
  UpdateGeolocationScreen: { screen: UpdateGeolocationScreen },
  UpdateContactScreen: { screen: UpdateContactScreen },
  AdditionalImagesScreen: { screen: AdditionalImagesScreen },
  UpdateBackgroundScreen: { screen: UpdateBackgroundScreen }
}, 'screen')
