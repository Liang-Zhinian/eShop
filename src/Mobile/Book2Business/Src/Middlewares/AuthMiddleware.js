// import * as types from './authenticationTypes';
// import Api from '../libs/api';
// import Utils from '../commons/utils';
import book2 from '../Services/Auth'

export default function authMiddleware ({ dispatch, getState }) {
  return (next) => (action) => {
    if (typeof action === 'function') {
      let state = getState()
      if (state) {
        if(state.member && state.member.token && isExpired(state.member.token)) {
          // make sure we are not already refreshing the token
          if(!state.refreshTokenPromise) {
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

function refreshToken(dispatch, state) {
  let refreshTokenPromise = 
  book2.auth().refreshToken(state.member.token.access_token, state.member.token.refresh_token)
  .then(resp => {
    dispatch({
      type: 'DONE_REFRESHING_TOKEN',
      data: {
        error: null,
        token: resp
      }
    });

    return resp ? Promise.resolve(resp) : Promise.reject({
        message: 'could not refresh token'
    });
  }).catch(ex => {
    console.log('exception refresh_token', ex);
    dispatch({
      type: 'DONE_REFRESHING_TOKEN',
      data: {
        error: 'exception refresh_token',
        token: null
      }
    });
  });

  dispatch({
    type: 'REFRESHING_TOKEN',
    // we want to keep track of token promise in the state so that we don't     try to refresh the token again while refreshing is in process
    data: refreshTokenPromise
  });

  return refreshTokenPromise;
}

