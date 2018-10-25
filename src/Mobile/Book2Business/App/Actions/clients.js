import ErrorMessages from '../Constants/errors'
import statusMessage from './status'
import { ServiceCatalogApi } from '../Services/Apis'

/**
  * Set an Error Message
  */
export function setError(message) {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'CLIENTS_ERROR',
    data: message
  })))
}

/**
  * Get AppointmentCategories
  */
export const searchClients = (keywords, pageSize, pageIndex) => {

  return dispatch => new Promise(async (resolve, reject) => {
    dispatch({
      type: 'CLIENTS_FETCHING_STATUS',
      data: true
    })

    var api = new ServiceCatalogApi()

    return api.getAppointmentCategories(siteId, pageSize, pageIndex)
      .then(async (res) => {
        dispatch({
          type: 'CLIENTS_FETCHING_STATUS',
          data: false
        })
        if (res.kind == "ok") {
          return resolve(dispatch({
            type: 'CLIENTS_REPLACE',
            data: res.data
          }))
        }
        else {
          return reject(res.kind)
        }
      })
      .catch(reject)
  }).catch(async (err) => {
    dispatch({
      type: 'CLIENTS_FETCHING_STATUS',
      data: false
    })
    throw err.message
  })
}

export const setSelectedClient = (item) => {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'SET_SELECTED_CLIENT',
    data: item
  })))
}

export const addOrUpdateClient = (formData) => {
  const {
    Id,
    Name,
    Description,
    AllowOnlineScheduling,
    ScheduleTypeId,
    SiteId,
  } = formData

  return (dispatch, getState) => new Promise(async (resolve, reject) => {

    // Validation checks
    if (!Name) return reject({ message: ErrorMessages.missingAppointmentCategoryName })

    await statusMessage(dispatch, 'loading', true)

    var api = new ServiceCatalogApi()

    const token = getState().member.token
    api.setAuthorizationHeader(`${token.token_type} ${token.access_token}`)

    if (!Id) {
      return api.addAppointmentCategory({
        Name,
        Description,
        AllowOnlineScheduling,
        ScheduleTypeId,
        SiteId,
      })
        .then(async (res) => {
          await statusMessage(dispatch, 'loading', false)
          if (res.kind == "ok") {
            return resolve('Appointment Category Saved')
          }
          else {
            return reject(Error(res.kind))
          }
        })
        .catch(reject)
    }
    else {
      return api.updateAppointmentCategory(formData)
        .then(async (res) => {
          await statusMessage(dispatch, 'loading', false)
          if (res.kind == "ok") {
            return resolve('Appointment Category Saved')
          }
          else {
            return reject(Error(res.kind))
          }
        })
        .catch(reject)
    }
  }).catch(async (err) => {
    await statusMessage(dispatch, 'loading', false)
    throw err.message
  })
}
