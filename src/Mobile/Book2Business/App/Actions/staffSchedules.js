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
  * Get ServiceCategories
  */
export function getStaffSchedules(siteId, locationId, staffId, appointmentId, pageSize=10, pageIndex=0) {

  return dispatch => new Promise(async (resolve, reject) => {
    await statusMessage(dispatch, 'loading', true)

    var api = new StaffSchedulesApi()

    return api.getStaffSchedules(siteId, locationId, staffId, appointmentId, pageSize, pageIndex)
      .then(async (res) => {
        await statusMessage(dispatch, 'loading', false)
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
    await statusMessage(dispatch, 'loading', false)
    throw err.message
  })

}

export const setSelectedScheduleItem = (item) => {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'SET_SELECTED_SCHEDULE_ITEM',
    data: item
  })))
}
