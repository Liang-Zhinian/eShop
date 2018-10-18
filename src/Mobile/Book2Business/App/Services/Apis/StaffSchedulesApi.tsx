import { Api } from '../api'

export default class StaffScheduleApi extends Api {

  setHeader(key: string, value){
    this.apisauce.setHeader(key, value)
  }

  setAuthorizationHeader(value: string) {
    this.setHeader('Authorization', value)
  }

  constructor() {
    const config = {
      url: "http://isd4u.com:5216/api/v1/sch",
      timeout: 10000,
    }

    super(config)
    this.setup()
  }

  async getStaffSchedules(siteId, locationId, staffId, appointmentTypeId, pageSize, pageIndex): Promise<{}> {
    // make the api call
    const response = await this.apisauce.get(`/Schedule/sites/${siteId}/locations/${locationId}/availabilities?pageSize=${pageSize}&pageIndex=${pageIndex}`)

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

  async addAvailability(data): Promise<{}> {
    // make the api call
    const response = await this.apisauce.post(`/Schedule/availabilities`, data)

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

  async updateAvailability(data): Promise<{}> {
    // make the api call
    const response = await this.apisauce.put(`/Schedule/availabilities`, data)

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

  async addUnavailability(data): Promise<{}> {
    // make the api call
    const response = await this.apisauce.post(`/Schedule/unavailabilities`, data)

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

  async updateUnavailability(data): Promise<{}> {
    // make the api call
    const response = await this.apisauce.put(`/Schedule/unavailabilities`, data)

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