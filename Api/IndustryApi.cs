using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurEsiApi.Models.Esi;
using RestSharp;

namespace EntrepreneurEsiApi.Api
{
    public class IndustryApi:CommonApi
    {
        public IndustryApi( EsiApiClient apiClient ) : base(apiClient)
        {
        }

        /// <summary>
        /// Return cost indices for solar systems
        /// </summary>
        /// <returns></returns>
        public IRestResponse<List<IndustrySystemResponse>> GetIndustrySystemIndicesData()
        {
            var request = new RestRequest(IndustrySystemResponse.Endpoint);
            var response = ApiClient.Execute<List<IndustrySystemResponse>>(request);
            return response;
        }

        /// <summary>
        /// Return cost indices for solar systems
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IndustrySystemResponse> GetIndustrySystemIndices()
        {
            var response = GetIndustrySystemIndicesData();
            return response.Data;
        }

        /// <summary>
        /// List industry jobs placed by a character
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="token"></param>
        /// <param name="includeCompleted"></param>
        /// <returns></returns>
        public IRestResponse<List<IndustryCharacterJobResponse>> GetCharacterIndustryJobsData(int characterId, string token, bool includeCompleted = true)
        {
            var request = new RestRequest(IndustryCharacterJobResponse.Endpoint);
            request.AddParameter("character_id", characterId, ParameterType.UrlSegment);
            request.AddParameter("include_completed", includeCompleted);
            request.AddParameter("token", token);
            var response = ApiClient.Execute<List<IndustryCharacterJobResponse>>(request);
            return response;
        }
        /// <summary>
        /// List industry jobs placed by a character
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="token"></param>
        /// <param name="includeCompleted"></param>
        /// <returns></returns>
        public IEnumerable<IndustryCharacterJobResponse> GetCharacterIndustryJobs(int characterId, string token, bool includeCompleted = true )
        {
            var response = GetCharacterIndustryJobsData(characterId, token, includeCompleted);
            return response.Data;
        }
    }
}
