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
export function getAppointmentTypes(siteId, appointmentCategoryId, pageSize, pageIndex) {

  return dispatch => new Promise(async (resolve, reject) => {
    
    dispatch({
      type: 'APPOINTMENT_TYPES_FETCHING_STATUS',
      data: true
    })

    var api = new ServiceCatalogApi()

    return api.getAppointmentTypes(siteId, appointmentCategoryId, pageSize, pageIndex)
      .then(async (res) => {
        
    dispatch({
      type: 'APPOINTMENT_TYPES_FETCHING_STATUS',
      data: false
    })
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
    
    dispatch({
      type: 'APPOINTMENT_TYPES_FETCHING_STATUS',
      data: false
    })
    throw err.message
  })

}

export const setSelectedAppointmentType = (item) => {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'SET_SELECTED_APPOINTMENT_TYPE',
    data: item
  })))
}

export const addOrUpdateAppointmentType = (formData) => {
  const {
    Id,
    Name,
    Description,
    DefaultTimeLength,
    ServiceCategoryId,
    IndustryStandardCategoryName,
    IndustryStandardSubcategoryName,
    Price,
    AllowOnlineScheduling,
    TaxRate,
    TaxAmount,
    SiteId,
  } = formData

  return (dispatch, getState) => new Promise(async (resolve, reject) => {

    // Validation checks
    if (!Name) return reject({ message: ErrorMessages.missingAppointmentTypeName })

    await statusMessage(dispatch, 'loading', true)

    var api = new ServiceCatalogApi()

    const token = getState().member.token
    api.setAuthorizationHeader(`${token.token_type} ${token.access_token}`)

    if (!Id) {
      return api.addAppointmentType({
        Name,
        Description,
        DefaultTimeLength,
        ServiceCategoryId,
        IndustryStandardCategoryName,
        IndustryStandardSubcategoryName,
        Price,
        AllowOnlineScheduling,
        TaxRate,
        TaxAmount,
        SiteId,
      })
        .then(async (res) => {
          await statusMessage(dispatch, 'loading', false)
          if (res.kind == "ok") {
            return resolve('Appointment Type Saved')
          }
          else {
            reject(Error(res.kind))
          }
        })
        .catch(reject)
    }
    else {
      return api.updateAppointmentType(formData)
        .then(async (res) => {
          await statusMessage(dispatch, 'loading', false)
          if (res.kind == "ok") {
            return resolve('Appointment Type Saved')
          }
          else {
            reject(Error(res.kind))
          }
        })
        .catch(reject)
    }
  }).catch(async (err) => {
    await statusMessage(dispatch, 'loading', false)
    throw err.message
  })
}
