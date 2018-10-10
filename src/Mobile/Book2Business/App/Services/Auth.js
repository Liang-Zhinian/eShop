import { setItem, getItem, mergeItem, removeItem } from './StorageService'
import { StaffsApi } from './Apis'

const requestTokenUrl = 'http://isd4u.com:5105/connect/token'
const getProfileUrl = `http://isd4u.com:5143/api/v1/staffs`

const authorize = (email, password) => {
  return new Promise((resolve, reject) => {
    var scopes = 'role openid profile offline_access identity sites'
    var data = `grant_type=password&scope=${scopes}&username=${email}&password=${password}&client_id=ro.client&client_secret=secret`

    var xhr = new XMLHttpRequest()
    xhr.withCredentials = true

    xhr.addEventListener('readystatechange', function () {
      if (this.readyState !== 4) {
        return
      }
      if (this.status === 200) {
        try {
          const responseJson = JSON.parse(this.response)
          resolve(responseJson)
        } catch (reason) {
          reject(reason)
        }
      } else {
        reject(Error(this.response))
      }
    })

    xhr.open('POST', requestTokenUrl)
    xhr.setRequestHeader('cache-control', 'no-cache')
    xhr.setRequestHeader('content-type', 'application/x-www-form-urlencoded')
    xhr.setRequestHeader('accept', 'application/json')

    xhr.send(data)
  })
}

const refresh = (access_token, refresh_token) => {
  return new Promise((resolve, reject) => {
    var data = `client_id=ro.client&client_secret=secret&refresh_token=${refresh_token}&grant_type=refresh_token`;

    var xhr = new XMLHttpRequest();
    xhr.withCredentials = true;

    xhr.addEventListener("readystatechange", function () {
      if (this.readyState !== 4) {
        return
      }
      if (this.status === 200) {
        try {
          const responseJson = JSON.parse(this.response)
          return resolve(responseJson)
        } catch (reason) {
          return reject(reason)
        }
      } else {
        return reject(Error(this.response))
      }
    });

    xhr.open("POST", requestTokenUrl);
    xhr.setRequestHeader("authorization", `Bearer ${access_token}`);
    xhr.setRequestHeader("cache-control", "no-cache");
    xhr.setRequestHeader("content-type", "application/x-www-form-urlencoded");
    // xhr.setRequestHeader('accept', 'application/json')

    xhr.send(data);

  })
}

/**
 * refresh = async () => {
    try {
      const authState = await refresh(config, {
        refreshToken: this.state.refreshToken
      });

      this.animateState({
        accessToken: authState.accessToken || this.state.accessToken,
        accessTokenExpirationDate:
          authState.accessTokenExpirationDate || this.state.accessTokenExpirationDate,
        refreshToken: authState.refreshToken || this.state.refreshToken
      });
    } catch (error) {
      Alert.alert('Failed to refresh token', error.message);
    }
  };
 */

const revoke = async () => {
  await removeItem('identity')
}

const retrieveTokenFromStorage = async () => {
  return await getItem('identity')
}

const saveTokenToStorage = async (token) => {
  return await setItem('identity', token)
}

const isExpired = (token) => {
  let currentTime = new Date()
  let auth_time = new Date(token.auth_time)
  auth_time.setSeconds(auth_time.getSeconds() + token.expires_in)
  let expires_date = auth_time
  return currentTime > expires_date
}

export const retrieveToken = retrieveTokenFromStorage

export default (function () {
  // revoke()
  signInWithEmailAndPassword = (email, password) => {
    return new Promise((resolve, reject) => {
      authorize(email, password)
        .then(async (token) => {
          await saveTokenToStorage({ auth_time: new Date(), email, password, ...token })
          // const userData = await getUserData(email)

          this.currentUser = { uid: email }

          resolve({
            token: token,
            user: this.currentUser
          })
        })
        .catch(reject)
    })
  }

  getUserData = async (username) => {
    return new Promise(async (resolve, reject) => {

      var api = new StaffsApi()

      const identity = await retrieveTokenFromStorage()
      api.setAuthorizationHeader(`${identity.token_type} ${identity.access_token}`)

      return api.getStaffByUserName(username)
        .then(res => {
          if (res.kind == "ok")
            return resolve(res.data)
          else return reject(Error(res.kind))
        })
    })
  }

  refreshToken = (access_token, refresh_token) => {
    async function _resfresh(access_token, refresh_token) {
      return await refresh(access_token, refresh_token)
        .then(async (newIdentity) => {
          const newToken = { auth_time: new Date(), email: identity.email, password: identity.password, ...newIdentity }
          await saveTokenToStorage(newToken)
          return newToken
        })
    }

    return new Promise((resolve, reject) => {
      if (!access_token) {
        return retrieveTokenFromStorage()
          .then(async identity => {
            return await _resfresh(identity.access_token, identity.refresh_token)
          })
          .catch(reject)
      } else {
        return _resfresh(access_token, refresh_token)
      }
    })
  }

  isTokenExpired = () => {
    return new Promise((resolve, reject) => {
      return retrieveTokenFromStorage()
        .then(identity => {
          resolve(isExpired(identity))
        })
        .catch(reject)
    })
  }

  this.currentUser = null
  this.auth = () => {

    return {
      signInWithEmailAndPassword: signInWithEmailAndPassword.bind(this),
      currentUser: this.currentUser,
      refreshToken: refreshToken,
      isExpired: isTokenExpired
    }
  }

  this.getUserData = getUserData

  return this
})()
