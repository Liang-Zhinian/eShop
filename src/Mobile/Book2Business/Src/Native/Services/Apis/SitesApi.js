/*
import { Api } from '../api'

export class SitesApi extends Api {

    constructor() {
        const config = {
            url: "http://localhost:55105",
            timeout: 10000,
        }

        super(config)
    }

    async getRepo(repo: string): Promise<Types.GetRepoResult> {
        // make the api call
        const response: ApiResponse<any> = await this.apisauce.get(`/repos/${repo}`)
    
        // the typical ways to die when calling an api
        if (!response.ok) {
          const problem = getGeneralApiProblem(response)
          if (problem) return problem
        }
    
        // transform the data into the format we are expecting
        try {
          const resultRepo: Types.Repo = {
            id: response.data.id,
            name: response.data.name,
            owner: response.data.owner.login,
          }
          return { kind: "ok", repo: resultRepo }
        } catch {
          return { kind: "bad-data" }
        }
      }
}
*/
