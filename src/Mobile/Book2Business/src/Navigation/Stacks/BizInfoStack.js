import { StackNavigator, TabNavigator, TabBarBottom } from 'react-navigation'

import CreateStackNavigator from '../CreateStackNavigator'
import LocationScreen from '../../modules/booking/Containers/Locations/LocationScreen'
import UpdateInfoScreen from '../../modules/booking/Containers/Locations/UpdateLocationInfo'
import UpdateAddressScreen from '../../modules/booking/Containers/Locations/UpdateAddress'
import UpdateGeolocationScreen from '../../modules/booking/Containers/Locations/UpdateGeolocation'
import UpdateContactScreen from '../../modules/booking/Containers/Locations/UpdateContact'
import AdditionalImagesScreen from '../../modules/booking/Containers/Locations/UpdateImage'
import UpdateBackgroundScreen from '../../modules/booking/Containers/Locations/UpdateImage'

export default CreateStackNavigator({
  Location: { screen: LocationScreen },
  UpdateInfoScreen: { screen: UpdateInfoScreen },
  UpdateAddressScreen: { screen: UpdateAddressScreen },
  UpdateGeolocationScreen: { screen: UpdateGeolocationScreen },
  UpdateContactScreen: { screen: UpdateContactScreen },
  AdditionalImagesScreen: { screen: AdditionalImagesScreen },
  UpdateBackgroundScreen: { screen: UpdateBackgroundScreen }
}, 'screen')
