import ErrorMessages from '../Constants/errors'
import statusMessage from './status'
import { ServiceCatalogApi } from '../Services/Apis'

/**
  * Set an Error Message
  */
export function setError(message) {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'APPOINTMENT_TYPES_ERROR',
    data: message
  })))
}

/**
  * Get ServiceCategories
  */
export function getAppointmentTypes(siteId, serviceCategoryId, pageSize, pageIndex) {

  return dispatch => new Promise(async (resolve, reject) => {
    await statusMessage(dispatch, 'loading', true)

    var api = new ServiceCatalogApi()

    return api.getServiceItems(siteId, serviceCategoryId, pageSize, pageIndex)
      .then(async (res) => {
        await statusMessage(dispatch, 'loading', false)
        if (res.kind == "ok") {
          return resolve(dispatch({
            type: 'APPOINTMENT_TYPES_REPLACE',
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

export function updateAppointmentType() {

}

export const setSelectedAppointmentType = (item) => {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'SET_SELECTED_APPOINTMENT_TYPE',
    data: item
  })))
}
