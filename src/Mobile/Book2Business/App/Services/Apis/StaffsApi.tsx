import { Api } from '../api'

export default class StaffsApi extends Api {

  setHeader(key: string, value){
    this.apisauce.setHeader(key, value)
  }

  setAuthorizationHeader(value: string) {
    this.setHeader('Authorization', value)
  }

  constructor() {
    const config = {
      url: "http://localhost:55143/api/v1/staffs",
      timeout: 10000,
    }

    super(config)
    this.setup()
  }

  async getStaffByUserName(username: string): Promise<{}> {
    // make the api call
    const response = await this.apisauce.get(`?username=${username}`)

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