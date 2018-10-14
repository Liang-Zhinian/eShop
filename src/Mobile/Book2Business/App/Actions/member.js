
import ErrorMessages from '../Constants/errors'
import statusMessage from './status'
import { AuthApi, MemberApi } from '../Services/Apis'
import { getLocationsStaffCanSignIn } from './locations'


/**
  * Set an Error Message
  */
export function setError(message) {
  return dispatch => new Promise(resolve => resolve(dispatch({
    type: 'MEMBER_ERROR',
    data: message
  })))
}

/**
  * Sign Up to Firebase
  */
export function signUp(formData) {
  const {
    email,
    password,
    password2,
    firstName,
    lastName
  } = formData

  return dispatch => new Promise(async (resolve, reject) => {
    // Validation checks
    if (!firstName) return reject({ message: ErrorMessages.missingFirstName })
    if (!lastName) return reject({ message: ErrorMessages.missingLastName })
    if (!email) return reject({ message: ErrorMessages.missingEmail })
    if (!password) return reject({ message: ErrorMessages.missingPassword })
    if (!password2) return reject({ message: ErrorMessages.missingPassword })
    if (password !== password2) return reject({ message: ErrorMessages.passwordsDontMatch })

    await statusMessage(dispatch, 'loading', true)

    // Go to Firebase
    // return Firebase.auth()
    //   .createUserWithEmailAndPassword(email, password)
    //   .then((res) => {
    //     // Send user details to Firebase database
    //     if (res && res.user.uid) {
    //       FirebaseRef.child(`users/${res.user.uid}`).set({
    //         firstName,
    //         lastName,
    //         signedUp: Firebase.database.ServerValue.TIMESTAMP,
    //         lastLoggedIn: Firebase.database.ServerValue.TIMESTAMP
    //       }).then(() => statusMessage(dispatch, 'loading', false).then(resolve))
    //     }
    //   }).catch(reject)
  }).catch(async (err) => {
    await statusMessage(dispatch, 'loading', false)
    throw err.message
  })
}

/**
  * Get this User's Details
  */
function getUserData(dispatch, username, token) {
  let memberApi = new MemberApi()

  memberApi.setAuthorizationHeader(`${token.token_type} ${token.access_token}`)

  return memberApi.getMemberByUserName(username)
    .then(res => {
      if (res.kind == "ok") {
        dispatch({
          type: 'USER_LOGIN_LOCATIONS',
          data: {
            siblingLocations: res.data.StaffLoginLocations,
          }
        })

        return dispatch({
          type: 'USER_DETAILS_UPDATE',
          data: res.data
        })
      }
      else {
        return Promise.reject(Error(res.kind))
      }
    })
    .catch(Promise.reject);
}

export function getMemberData() {
  if (Firebase === null) return () => new Promise(resolve => resolve())

  // Ensure token is up to date
  return dispatch => new Promise((resolve) => {
    // Firebase.auth().onAuthStateChanged((loggedIn) => {
    //   if (loggedIn) {
    //     return resolve(getUserData(dispatch))
    //   }

    //   return () => new Promise(() => resolve())
    // })
  })
}

/**
  * Login with Email/Password
  */
export function login(formData) {
  const {
    username,
    password
  } = formData


  return dispatch => new Promise(async (resolve, reject) => {
    await statusMessage(dispatch, 'loading', true)

    // Validation checks
    if (!username) return reject({ message: ErrorMessages.missingUserName })
    if (!password) return reject({ message: ErrorMessages.missingPassword })

    //
    var api = new AuthApi();

    return api.authorizeXhr(username, password)
      .then(async (res) => {
        if (res.kind == "ok") {
          const token = res && res.data ? res.data : null
          // if (token) {
          //   dispatch({
          //     type: 'AUTH',
          //     data: { ...token, username, password, auth_time: new Date() }
          //   })
          // }

          // Get User Data
          getUserData(dispatch, username, token)

          await statusMessage(dispatch, 'loading', false)

          // Send Login data to Redux
          return resolve(dispatch({
            type: 'USER_LOGIN',
            data: { token: { ...token, auth_time: new Date() }, username, password }
          }))
        }
        else {
          return reject(Error(res.kind))
        }
      })
      .catch(reject);
  })
    .catch(async (err) => {
      await statusMessage(dispatch, 'loading', false)
      throw err.message
    })
}

/**
  * Reset Password
  */
export function resetPassword(formData) {
  const { email } = formData

  return dispatch => new Promise(async (resolve, reject) => {
    // Validation checks
    if (!email) return reject({ message: ErrorMessages.missingEmail })

    await statusMessage(dispatch, 'loading', true)

    // Go to Firebase
    // return Firebase.auth()
    //   .sendPasswordResetEmail(email)
    //   .then(() => statusMessage(dispatch, 'success', 'We have emailed you a reset link').then(resolve(dispatch({ type: 'USER_RESET' }))))
    //   .catch(reject)
  }).catch(async (err) => {
    await statusMessage(dispatch, 'loading', false)
    throw err.message
  })
}

/**
  * Update Profile
  */
export function updateProfile(formData) {
  const {
    email,
    password,
    password2,
    firstName,
    lastName,
    changeEmail,
    changePassword
  } = formData

  return dispatch => new Promise(async (resolve, reject) => {
    // Are they a user?
    const UID = Firebase.auth().currentUser.uid
    if (!UID) return reject({ message: ErrorMessages.missingFirstName })

    // Validation checks
    if (!firstName) return reject({ message: ErrorMessages.missingFirstName })
    if (!lastName) return reject({ message: ErrorMessages.missingLastName })
    if (changeEmail) {
      if (!email) return reject({ message: ErrorMessages.missingEmail })
    }
    if (changePassword) {
      if (!password) return reject({ message: ErrorMessages.missingPassword })
      if (!password2) return reject({ message: ErrorMessages.missingPassword })
      if (password !== password2) return reject({ message: ErrorMessages.passwordsDontMatch })
    }

    await statusMessage(dispatch, 'loading', true)

    // Go to Firebase
    return FirebaseRef.child(`users/${UID}`).update({ firstName, lastName })
      .then(async () => {
        // Update Email address
        if (changeEmail) {
          await Firebase.auth().currentUser.updateEmail(email).catch(reject)
        }

        // Change the password
        if (changePassword) {
          await Firebase.auth().currentUser.updatePassword(password).catch(reject)
        }

        // Update Redux
        await getUserData(dispatch)
        await statusMessage(dispatch, 'loading', false)
        return resolve('Profile Updated')
      }).catch(reject)
  }).catch(async (err) => {
    await statusMessage(dispatch, 'loading', false)
    throw err.message
  })
}

/**
  * Logout
  */
export function logout() {
  return dispatch => new Promise((resolve, reject) => {
    dispatch({ type: 'USER_RESET' })
    setTimeout(resolve, 1000) // Resolve after 1s so that user sees a message
  }).catch(async (err) => { await statusMessage(dispatch, 'error', err.message); throw err.message })
}


export function refreshToken(dispatch, state) {
  const { access_token, token_type, refresh_token } = state.member.token

  var api = new AuthApi()

  let refreshTokenPromise =
    api.refreshXhr(access_token, token_type, refresh_token)
      .then(resp => {
        if (resp.kind == 'ok') {
          dispatch({
            type: 'DONE_REFRESHING_TOKEN',
            data: {
              error: null,
              token: { ...resp.data, auth_time: new Date() }
            }
          });
          return Promise.resolve(resp.data)
        } else {

          dispatch({
            type: 'DONE_REFRESHING_TOKEN',
            data: {
              error: 'exception refresh_token: ' + resp.kind,
              token: null
            }
          });

          return Promise.reject({
            message: 'could not refresh token'
          });
        }

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
