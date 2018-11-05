import ErrorMessages from '../Constants/errors'
import statusMessage from './status'
import { AppointmentSchedulesApi } from '../Services/Apis'
import AppointmentSchedule from '../Models/AppointmentSchedule'

/**
  * Set an Error Message
  */
export function setError(message) {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'APPOINTMENT_SCHEDULES_ERROR',
    data: message
  })))
}

/**
  * Get StaffSchedules
  */
export function getAppointmentSchedules(siteId, locationId, startDateTime, endDateTime, pageSize = 10, pageIndex = 0) {

  return dispatch => new Promise(async (resolve, reject) => {

    dispatch({
      type: 'APPOINTMENT_SCHEDULES_FETCHING_STATUS',
      data: true
    })

    var api = new StaffSchedulesApi()

    return api.getStaffSchedules(siteId, locationId, staffId, appointmentId, pageSize, pageIndex)
      .then(async (res) => {

        dispatch({
          type: 'APPOINTMENT_SCHEDULES_FETCHING_STATUS',
          data: false
        })
        if (res.kind == "ok") {
          return resolve(dispatch({
            type: 'APPOINTMENT_SCHEDULES_REPLACE',
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
      type: 'APPOINTMENT_SCHEDULES_FETCHING_STATUS',
      data: false
    })
    throw err.message
  })

}

export const setSelectedScheduleItem = (item: AppointmentSchedule) => {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'SET_SELECTED_APPOINTMENT_SCHEDULE',
    data: item
  })))
}

export function addOrUpdateAppointmentSchedule(formData: AppointmentSchedule) {
  const {
    Id,
    OrderDate,
    StartDateTime,
    EndDateTime,
    StaffId,
    LocationId,
    SiteId,
    GenderPreference,
    Duration,
    AppointmentStatusId,
    Notes,
    StaffRequested,
    ClientId,
    FirstAppointment,
    AppointmentServiceItems
  } = formData

  console.log('addOrUpdateAppointmentSchedule', formData)

  return (dispatch, getState) => new Promise(async (resolve, reject) => {

    // Validation checks

    await statusMessage(dispatch, 'loading', true)

    var api = new AppointmentSchedulesApi()

    const token = getState().member.token
    api.setAuthorizationHeader(`${token.token_type} ${token.access_token}`)

    if (!Id) {
      return api.addAppointmentSchedule({
        OrderDate,
        StartDateTime,
        EndDateTime,
        StaffId,
        LocationId,
        SiteId,
        GenderPreference,
        Duration,
        AppointmentStatusId,
        Notes,
        StaffRequested,
        ClientId,
        FirstAppointment,
        AppointmentServiceItems
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
      return api.updateAppointmentSchedule(formData)
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
