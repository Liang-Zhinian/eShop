import ErrorMessages from '../Constants/errors'
import statusMessage from './status'
import { SitesApiUrl } from '../Constants/api'
import * as Storage from '../Services/StorageService'

/**
  * Set an Error Message
  */
export function setError(message) {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'STAFFS_ERROR',
    data: message
  })))
}

/**
  * Get ServiceCategories
  */
export function getStaffs(siteId) {

  return dispatch => new Promise(async (resolve, reject) => {
    await statusMessage(dispatch, 'loading', true)
    const url = `${SitesApiUrl}/Staffs/sites/${siteId}/staffs`
    console.log(url)
    var identity = await Storage.getItem('identity')
    console.log(identity)
    return fetch(url, {
      method: 'GET',
      headers: {
        authorization: `Bearer ${identity.access_token}`
      }
    })
      .then(res => res.json())
      .then((json) => {
        console.log(json)
        return resolve(dispatch({
          type: 'STAFFS_REPLACE',
          data: json
        }))
      }).catch(reject)
  }).catch(async (err) => {
    await statusMessage(dispatch, 'loading', false)
    throw err.message
  })
}
