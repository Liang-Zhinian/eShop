
import { refreshToken } from '../Actions/member'

export default function authMiddleware({ dispatch, getState }) {
  return (next) => (action) => {
    if (typeof action === 'function') {
      let state = getState()
      if (state) {
        if (state.member && state.member.token && isExpired(state.member.token)) {
          // make sure we are not already refreshing the token
          if (!state.refreshTokenPromise) {
            return refreshToken(dispatch, state).then(() => next(action));
          } else {
            return state.refreshTokenPromise.then(() => next(action));
          }
        }
      }
    }
    return next(action)
  }
}


const isExpired = (token) => {
  let currentTime = new Date()
  let auth_time = new Date(token.auth_time)
  auth_time.setSeconds(auth_time.getSeconds() + token.expires_in)
  let expires_date = auth_time
  return currentTime > expires_date
}
