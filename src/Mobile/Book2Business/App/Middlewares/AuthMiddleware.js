// import * as types from './authenticationTypes';
// import Api from '../libs/api';
// import Utils from '../commons/utils';

export default function authMiddleware({ dispatch, getState }) {
  return (next) => (action) => {
    if (typeof action === 'function') {
      let state = getState();
      if(!state) {
        // if(state.token && isExpired(state.token)) {
        //   // make sure we are not already refreshing the token
        //   if(!state.refreshTokenPromise) {
        //     return refreshToken(dispatch, state).then(() => next(action));
        //   } else {
        //     return state.refreshTokenPromise.then(() => next(action));
        //   }
        // }
        console.log('authMiddleware')
      }

    }
    return next(action);
  }
}

/*
function isExpired(token) {
  let currentTime = new Date();
  let expires_date = new Date(token.expires_date);
  return currentTime > expires_date;
}

function refreshToken(dispatch, state) {
  let refreshTokenPromise = Api.post('/token', {
    grant_type: 'refresh_token',
    username: state.token.username,
    refresh_token: state.token.refresh_token
  }, null, true).then(resp => {
    dispatch({
      type: types.DONE_REFRESHING_TOKEN
    });
    dispatch({
      type: types.LOGIN_SUCCESS,
      data: resp
    });
    dispatch({
      type: types.SET_HEADER,
      header: {
        Authorization: resp.token_type + ' ' + resp.access_token,
        Instance: state.currentInstance.id
      }
    });
    return resp ? Promise.resolve(resp) : Promise.reject({
        message: 'could not refresh token'
    });
  }).catch(ex => {
    console.log('exception refresh_token', ex);
    dispatch({
      type: types.DONE_REFRESHING_TOKEN
    });
    dispatch({
      type: types.LOGIN_FAILED,
      exception: ex
    });
  });

  dispatch({
    type: types.REFRESHING_TOKEN,
    // we want to keep track of token promise in the state so that we don't     try to refresh the token again while refreshing is in process
    refreshTokenPromise
  });

  return refreshTokenPromise;
}
*/