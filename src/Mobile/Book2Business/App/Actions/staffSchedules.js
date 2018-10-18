import ErrorMessages from '../Constants/errors'
import statusMessage from './status'
import { StaffSchedulesApi } from '../Services/Apis'

/**
  * Set an Error Message
  */
export function setError(message) {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'STAFF_SCHEDULES_ERROR',
    data: message
  })))
}

/**
  * Get StaffSchedules
  */
export function getStaffSchedules(siteId, locationId, staffId, appointmentId, pageSize = 10, pageIndex = 0) {

  return dispatch => new Promise(async (resolve, reject) => {

    dispatch({
      type: 'STAFF_SCHEDULES_FETCHING_STATUS',
      data: true
    })

    var api = new StaffSchedulesApi()

    return api.getStaffSchedules(siteId, locationId, staffId, appointmentId, pageSize, pageIndex)
      .then(async (res) => {

        dispatch({
          type: 'STAFF_SCHEDULES_FETCHING_STATUS',
          data: false
        })
        if (res.kind == "ok") {
          return resolve(dispatch({
            type: 'STAFF_SCHEDULES_REPLACE',
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
      type: 'STAFF_SCHEDULES_FETCHING_STATUS',
      data: false
    })
    throw err.message
  })

}

export const setSelectedScheduleItem = (item) => {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'SET_SELECTED_SCHEDULE_ITEM',
    data: item
  })))
}

export function addOrUpdateAvailability(formData) {
  const {
    Id,
    StartDateTime,
    EndDateTime,
    StaffId,
    ServiceItemId,
    LocationId,
    SiteId,
    BookableEndDateTime,
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
  } = formData

  return (dispatch, getState) => new Promise(async (resolve, reject) => {

    // Validation checks
    // if (!Name) return reject({ message: ErrorMessages.missingAppointmentCategoryName })

    await statusMessage(dispatch, 'loading', true)

    var api = new StaffSchedulesApi()

    const token = getState().member.token
    api.setAuthorizationHeader(`${token.token_type} ${token.access_token}`)

    if (!Id) {
      return api.addAvailability({
        StartDateTime,
        EndDateTime,
        StaffId,
        ServiceItemId,
        LocationId,
        SiteId,
        BookableEndDateTime,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
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
      return api.updateAvailability(formData)
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

export function addOrUpdateUnavailability(formData) {
  const {
    Id,
    StartDateTime,
    EndDateTime,
    StaffId,
    ServiceItemId,
    LocationId,
    SiteId,
    BookableEndDateTime,
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
  } = formData

  return (dispatch, getState) => new Promise(async (resolve, reject) => {

    // Validation checks
    // if (!Name) return reject({ message: ErrorMessages.missingAppointmentCategoryName })

    await statusMessage(dispatch, 'loading', true)

    var api = new StaffSchedulesApi()

    const token = getState().member.token
    api.setAuthorizationHeader(`${token.token_type} ${token.access_token}`)

    if (!Id) {
      return api.addUnavailability({
        StartDateTime,
        EndDateTime,
        StaffId,
        ServiceItemId,
        LocationId,
        SiteId,
        BookableEndDateTime,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
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
      return api.updateUnavailability(formData)
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
