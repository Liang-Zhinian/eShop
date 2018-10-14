import { Api } from '../api'

export default class MemberApi extends Api {

  setHeader(key: string, value) {
    this.apisauce.setHeader(key, value)
  }

  setAuthorizationHeader(value: string) {
    this.setHeader('Authorization', value)
  }

  constructor() {
    const config = {
      url: "http://isd4u.com:5216/api/v1/s",
      timeout: 10000,
    }

    super(config)
    this.setup()
  }

  async getMemberByUserName(username: string): Promise<{}> {
    // make the api call
    const response = await this.apisauce.get(`/staffs?username=${username}`)

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