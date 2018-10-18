import { Api } from '../api'

const url = 'http://isd4u.com:5105'
const scopes = 'role openid profile offline_access identity identityaccess sites catalog appointment mobilereservationagg schedules'

export default class AuthApi extends Api {

  setHeader(key: string, value) {
    this.apisauce.setHeader(key, value)
  }

  setAuthorizationHeader(value: string) {
    this.setHeader('Authorization', value)
  }

  constructor() {
    const config = {
      url: url,
      timeout: 10000
    }

    super(config)
    this.setup()

    this.setHeader('content-type', 'application/x-www-form-urlencoded')
  }

  async authorize(username: string, password: string): Promise<{}> {
    var data = {
      'client_id': 'ro.client',
      'scope': scopes,
      'username': username,
      'password': password,
      'client_secret': 'secret',
      'grant_type': 'password'
    }

    // this.setHeader('content-type', 'application/x-www-form-urlencoded')

    // make the api call
    const response = await this.apisauce.post('/connect/token', data)

    // the typical ways to die when calling an api
    if (!response.ok) {
      const problem = this.getGeneralApiProblem(response)
      if (problem) return problem
    }

    // transform the data into the format we are expecting
    try {
      return { kind: "ok", data: response.data }
    } catch {
      return { kind: "bad-data" }
    }
  }

  async refresh(access_token: string, token_type: string, refresh_token: string): Promise<{}> {
    var data = {
      client_id: 'ro.client',
      refresh_token: refresh_token,
      client_secret: 'secret',
      grant_type: 'refresh_token'
    }

    // this.setHeader('content-type', 'application/x-www-form-urlencoded')
    this.setAuthorizationHeader(`${token_type} ${access_token}`)

    // make the api call
    const response = await this.apisauce.post('/connect/token', data)

    // the typical ways to die when calling an api
    if (!response.ok) {
      const problem = this.getGeneralApiProblem(response)
      if (problem) return problem
    }

    // transform the data into the format we are expecting
    try {
      return { kind: "ok", data: response.data }
    } catch {
      return { kind: "bad-data" }
    }
  }

  authorizeXhr(username, password) {
    return new Promise((resolve, reject) => {
      var data = `grant_type=password&scope=${scopes}&username=${username}&password=${password}&client_id=ro.client&client_secret=secret`

      var xhr = new XMLHttpRequest()
      xhr.withCredentials = true

      xhr.addEventListener('readystatechange', function () {
        if (this.readyState !== 4) {
          return
        }
        if (this.status === 200) {
          try {
            const responseJson = JSON.parse(this.response)
            return resolve({ kind: 'ok', data: responseJson })
          } catch (reason) {
            return reject(reason)
          }
        } else {
          return reject(Error(this.response))
        }
      })

      xhr.open('POST', `${url}/connect/token`)
      xhr.setRequestHeader('cache-control', 'no-cache')
      xhr.setRequestHeader('content-type', 'application/x-www-form-urlencoded')
      xhr.setRequestHeader('accept', 'application/json')

      xhr.send(data)
    })
  }

  refreshXhr(access_token: string, token_type: string, refresh_token: string) {
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
            return resolve({ kind: 'ok', data: responseJson })
          } catch (reason) {
            return reject({ kind: reason.message })
          }
        } else {
          return reject(Error(this.response))
        }
      });

      xhr.open("POST", `${url}/connect/token`);
      xhr.setRequestHeader("authorization", `${token_type} ${access_token}`);
      xhr.setRequestHeader("cache-control", "no-cache");
      xhr.setRequestHeader("content-type", "application/x-www-form-urlencoded");
      // xhr.setRequestHeader('accept', 'application/json')

      xhr.send(data);

    })
  }

  async getMemberByUserName(username: string): Promise<{}> {
    // make the api call
    const response = await this.apisauce.get(`/api/v1/identity/users/with-user-name/${username}`)

    // the typical ways to die when calling an api
    if (!response.ok) {
      const problem = this.getGeneralApiProblem(response)
      if (problem) return problem
    }

    // transform the data into the format we are expecting
    try {
      return { kind: "ok", data: response.data }
    } catch {
      return { kind: "bad-data" }
    }
  }

}