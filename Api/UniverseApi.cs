using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurCommon.Api;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Models;
using EntrepreneurCommon.Models.Esi;
using RestSharp;

namespace EntrepreneurCommon.Api
{
    public class UniverseApi
    {
        public EsiApiClient ApiClient { get; set; }
        private RestClient restClient { get => ApiClient.RestClient; }

        public UniverseApi(EsiApiClient apiClient)
        {
            ApiClient = apiClient;
        }

        /// <summary>
        /// Resolve a set of IDs to names and categories. Supported ID’s for resolving are: Characters, Corporations, Alliances, Stations, Solar Systems, Constellations, Regions, Types.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<UniverseNameResponse> GetUniverseNames( int[] ids )
        {
            var request = new RestRequest(UniverseNameResponse.Endpoint, Method.POST);
            request.AddJsonBody(ids);
            var response = ApiClient.Execute<List<UniverseNameResponse>>(request);
            return response.Data;
        }

        /// <summary>
        /// Resolve a set of IDs to names and categories. Supported ID’s for resolving are: Characters, Corporations, Alliances, Stations, Solar Systems, Constellations, Regions, Types.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IRestResponse<List<UniverseNameResponse>> GetUniverseNamesResponse( int[] ids )
        {
            var request = new RestRequest(UniverseNameResponse.Endpoint, Method.POST);
            request.AddJsonBody(ids);
            var response = ApiClient.Execute<List<UniverseNameResponse>>(request);
            return response;
        }

        public IRestResponse<SearchResponse> MakeAuthorizedSearch( string searchString, string[] categories, int characterId, string token)
        {
            // Categories are comma divided, ie: ?categories=solar_system,station,structure
            var request = new RestRequest(SearchResponse.Endpoint, Method.GET);
            request.AddParameter("character_id", characterId,ParameterType.UrlSegment);
            request.AddParameter("search", searchString);
            request.AddParameter("categories", String.Join(",", categories));
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = ApiClient.Execute<SearchResponse>(request);
            return response;
        }

        public IRestResponse<UniverseStructureResponse> GetStructureInformation(Int64 structureId, string token = null)
        {
            var request = new RestRequest(UniverseStructureResponse.Endpoint);
            request.AddParameter("structure_id", structureId, ParameterType.UrlSegment);
            if (token != null) {
                request.AddParameter("token", token, ParameterType.QueryString);
            }
            var response = ApiClient.Execute<UniverseStructureResponse>(request);
            response.Data.StructureId = structureId;
            return response;
        }
    }
}
