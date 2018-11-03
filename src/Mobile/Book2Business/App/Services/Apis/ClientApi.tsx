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

  async searchClients(searchText: string, pageSize=10, pageIndex=0): Promise<{}> {
    // make the api call
    const response = await this.apisauce.get(`/api/v1/identity/users?searchtext=${searchText}&pageSize=${pageSize}&pageIndex=${pageIndex}`)

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