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
          console.log('responsed token', this.response)
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

const isExpired = (token) => {
  if (!token.auth_time) return true
  let currentTime = new Date()
  let auth_time = new Date(token.auth_time)
  auth_time.setSeconds(auth_time.getSeconds() + token.expires_in)
  let expires_date = auth_time
  return currentTime > expires_date
}

export default (function () {
  // revoke()
  signInWithEmailAndPassword = (email, password) => {
    return new Promise((resolve, reject) => {
      authorize(email, password)
        .then(async (token) => {
          this.currentUser = { uid: email }
          this.token = token
          resolve({
            token: token,
            user: this.currentUser
          })
        })
        .catch(reject)
    })
  }

  getUserData = async (username) => {
    var that = this
    return new Promise(async (resolve, reject) => {

      var api = new StaffsApi()
      var token = that.token

      api.setAuthorizationHeader(`${token.token_type} ${token.access_token}`)

      return api.getStaffByUserName(username)
        .then(res => {
          if (res.kind == "ok")
            return resolve(res.data)
          else return reject(Error(res.kind))
        })
    })
  }

  refreshToken = (access_token, refresh_token) => {
    return refresh(access_token, refresh_token)
      .then(async (newIdentity) => {
        const newToken = { auth_time: new Date(), ...newIdentity }
        return newToken
      })
  }

  isTokenExpired = (token) => {
    return isExpired(token)
  }

  this.token = null
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
