import ErrorMessages from '../Constants/errors'
import statusMessage from './status'
// import { Firebase, FirebaseRef } from '../lib/firebase';
import site from '../Constants/site'

/**
  * Set an Error Message
  */
export function setError (message) {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'LOCATIONS_ERROR',
    data: message
  })))
}

export function getLocation (siteId, locationId) {
  const { api } = site

  return dispatch => new Promise(async (resolve, reject) => {
    await statusMessage(dispatch, 'loading', true)

        // Go to Firebase
    return fetch(`${api}locations/sites/${siteId}/locations/${locationId}`)
            .then(res => res.json())
          .then((json) => {
            statusMessage(dispatch, 'loading', false)
            return resolve(dispatch({
              type: 'LOCATION',
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
export function updateLocation (formData) {
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
