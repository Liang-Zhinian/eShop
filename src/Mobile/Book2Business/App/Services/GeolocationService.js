
export const getUserPosition = async () => {
  return new Promise(async (resolve, reject) => {
        // get geolocation
    navigator.geolocation.getCurrentPosition(pos => {
      resolve(pos.coords)
    }, err => {
      let err_msg = `getUserPosition ERROR: (${err.code}) - ${err.message}`

      console.warn(err_msg)
      Alert.alert(err_msg, [{ text: 'OK', onPress: () => console.log('OK Pressed') }], { cancelable: false })
      reject(err)
    }, {
      enableHighAccuracy: true,
      timeout: 20000,
      maximumAge: 0
    })
  })
}

export const watchUserPosition = async () => {
  return new Promise(async (resolve, reject) => {
    let watchID = navigator.geolocation.watchPosition(pos => {
      resolve({
        watchID,
        coords: pos.coords
      })
    }, error => {
      reject(error)
    })
  })
}
