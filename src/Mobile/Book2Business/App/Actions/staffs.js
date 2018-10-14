import { StaffsApi } from '../Services/Apis'

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
  return (dispatch, getState) => new Promise(async (resolve, reject) => {

    dispatch({ type: 'STAFFS_LOADING', data: true })

    var api = new StaffsApi()
    const token = getState().member.token

    api.setAuthorizationHeader(`${token.token_type} ${token.access_token}`)
    return api.getStaffs(siteId)
      .then(async (res) => {
        dispatch({ type: 'STAFFS_LOADING', data: false })
        if (res.kind == "ok") {
          return resolve(dispatch({
            type: 'STAFFS_REPLACE',
            data: res.data
          }))
        }
        else {
          return reject(Error(res.kind))
        }
      })
  }).catch(async (err) => {
    throw err.message
  })

}
