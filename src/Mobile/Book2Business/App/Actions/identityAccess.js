import { IdentityAccessApi } from '../Services/Apis'

/**
  * Set an Error Message
  */
export function setError(message) {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'IDENTITY_ACCESS_ERROR',
    data: message
  })))
}

/**
  * offer Registration Invitation
  */
export function offerRegistrationInvitation(desc) {
  return (dispatch, getState) => new Promise(async (resolve, reject) => {

    dispatch({ type: 'IA_OFFER_REGISTRATION_INVITATION_LOADING', data: true })

    var api = new IdentityAccessApi()
    const token = getState().member.token

    api.setAuthorizationHeader(`${token.token_type} ${token.access_token}`)
    return api.offerRegistrationInvitation(desc)
      .then(async (res) => {
        dispatch({ type: 'IA_OFFER_REGISTRATION_INVITATION_LOADING', data: false })
        if (res.kind == "ok") {
          return resolve(dispatch({
            type: 'IA_OFFER_REGISTRATION_INVITATION_REPLACE',
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