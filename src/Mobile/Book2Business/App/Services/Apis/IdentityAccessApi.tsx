import { Api } from '../api'
import RegisterUserCommand from '../../Models/RegisterUserCommand';

export default class StaffsApi extends Api {

  setHeader(key: string, value) {
    this.apisauce.setHeader(key, value)
  }

  setAuthorizationHeader(value: string) {
    this.setHeader('Authorization', value)
  }

  constructor() {
    const config = {
      url: "http://isd4u.com:5216/api/v1/ia",
      timeout: 10000,
    }

    super(config)
    this.setup()
  }

  async offerRegistrationInvitation(tenantId: string, description: string){
    // make the api call
    const response = await this.apisauce.post(`/IdentityAccess/tenants/${tenantId}/registration-invitations?description=${description}`)

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

  async addStaff(staff: RegisterUserCommand): Promise<{}> {

    // make the api call
    const response = await this.apisauce.post(`/IdentityAccess/tenants/${staff.TenantId}/staffs`, staff)

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
