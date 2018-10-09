import ErrorMessages from '../Constants/errors'
import statusMessage from './status'
import { StaffsApi } from '../Services/Apis'
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

    var api = new StaffsApi()

    return api.getStaffs(siteId)
      .then(async (res) => {
        await statusMessage(dispatch, 'loading', false)
        if (res.kind == "ok") {
          return resolve(dispatch({
            type: 'STAFFS_REPLACE',
            data: res.data
          }))
        }
        else {
          reject(Error(res.kind))
        }
      })
      .catch(reject)
  }).catch(async (err) => {
    await statusMessage(dispatch, 'loading', false)
    throw err.message
  })

}
