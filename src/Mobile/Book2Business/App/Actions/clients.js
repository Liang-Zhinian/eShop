import ErrorMessages from '../Constants/errors'
import statusMessage from './status'
import { ClientApi } from '../Services/Apis'

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
export const searchClients = (searchText, pageSize, pageIndex) => {

  return (dispatch, getState) => new Promise(async (resolve, reject) => {
    dispatch({
      type: 'CLIENTS_FETCHING_STATUS',
      data: true
    })

    var api = new ClientApi()
    const token = getState().member.token

    api.setAuthorizationHeader(`${token.token_type} ${token.access_token}`)

    return api.searchClients(searchText, pageSize, pageIndex)
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
