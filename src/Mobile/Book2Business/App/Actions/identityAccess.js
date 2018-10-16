import { IdentityAccessApi } from '../Services/Apis'
import RegisterUserCommand from '../Models/RegisterUserCommand'

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
    const member = getState().member
    const token = member.token
    const tenantId = member.TenantId

    api.setAuthorizationHeader(`${token.token_type} ${token.access_token}`)
    return api.offerRegistrationInvitation(tenantId, desc)
      .then(async (res) => {
        dispatch({ type: 'IA_OFFER_REGISTRATION_INVITATION_LOADING', data: false })
        if (res.kind == "ok") {
          return resolve(dispatch({
            type: 'IA_OFFER_REGISTRATION_INVITATION_REPLACE',
            data: desc //res.data
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

export function addStaff(newStaff) {
  return (dispatch, getState) => new Promise(async (resolve, reject) => {

    // dispatch({ type: 'IA_OFFER_REGISTRATION_INVITATION_LOADING', data: true })

    var api = new IdentityAccessApi()
    const member = getState().member
    const token = member.token
    const tenantId = member.TenantId

    api.setAuthorizationHeader(`${token.token_type} ${token.access_token}`)
    return api.addStaff(newStaff)
      .then(async (res) => {
        // dispatch({ type: 'IA_OFFER_REGISTRATION_INVITATION_LOADING', data: false })
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

