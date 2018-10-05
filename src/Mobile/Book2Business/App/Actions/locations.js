import ErrorMessages from '../Constants/errors'
import statusMessage from './status'
// import { Firebase, FirebaseRef } from '../lib/firebase';
import site from '../Constants/site'

/**
  * Set an Error Message
  */
export function setError(message) {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'LOCATIONS_ERROR',
    data: message
  })))
}

export function getLocation(siteId, locationId) {
  const { api } = site
  
  return dispatch => new Promise(async (resolve, reject) => {
    await statusMessage(dispatch, 'loading', true)

    // Go to 
    const url = `${api}business-information/sites/${siteId}/locations/${locationId}`
    console.log(url)
    return fetch(url)
      .then(res => res.json())
      .then((json) => {
        statusMessage(dispatch, 'loading', false)
        return resolve(dispatch({
          type: 'CURRENT_LOCATION',
          data: json
        }))
      }).catch(reject)
  }).catch(async (err) => {
    await statusMessage(dispatch, 'loading', false)
    throw err.message
  })
}

export function getLocationsStaffCanSignIn(siteId, staffId) {
  const { api } = site
  
  return dispatch => new Promise(async (resolve, reject) => {
    await statusMessage(dispatch, 'loading', true)

    // Go to 
    const url = `${api}business-information/sites/${siteId}/staffs/${staffId}/login-locations`
    return fetch(url)
      .then(res => res.json())
      .then((json) => {
        statusMessage(dispatch, 'loading', false)
        return resolve(dispatch({
          type: 'SIBLING_LOCATIONS',
          data: json
        }))
      }).catch(reject)
  }).catch(async (err) => {
    await statusMessage(dispatch, 'loading', false)
    throw err.message
  })
}

/**
  * Update Profile
  */
export function updateLocation(formData) {
  const {
    latitude,
    longitude,
    streetAddress,
    city,
    stateProvince,
    postalCode,
    country,
    primaryTelephone,
    secondaryTelephone,
    contactName
  } = formData

  return dispatch => new Promise(async (resolve, reject) => {
    await statusMessage(dispatch, 'loading', true)
  }).catch(async (err) => {
    await statusMessage(dispatch, 'loading', false)
    throw err.message
  })
}
