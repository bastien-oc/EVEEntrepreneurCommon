using System;
using System.Collections.Generic;
using EntrepreneurCommon.Client;
using EntrepreneurCommon.ExtensionMethods;
using EntrepreneurCommon.Helpers;
using EntrepreneurCommon.Models.EsiResponseModels;
using RestSharp;

namespace EntrepreneurCommon.Api
{
    public class UniverseApi
    {
        public IEsiRestClient Client { get; set; }
        //private RestClient restClient { get => ApiClient.RestClient; }

        public UniverseApi(IEsiRestClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Resolve a set of IDs to names and categories. Supported ID’s for resolving are: Characters, Corporations, Alliances, Stations, Solar Systems, Constellations, Regions, Types.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<UniverseNameResponse> GetUniverseNames(int[] ids)
        {
            var request = RequestHelper.GetRestRequest<UniverseNameResponse>()
                                       .SetMethod(Method.POST)
                                       .AddJsonBody(ids);
            var result = Client.Execute<List<UniverseNameResponse>>(request);
            return result.Data;
        }

        /// <summary>
        /// Resolve a set of IDs to names and categories. Supported ID’s for resolving are: Characters, Corporations, Alliances, Stations, Solar Systems, Constellations, Regions, Types.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IRestResponse<List<UniverseNameResponse>> GetUniverseNamesResponse(int[] ids)
        {
            var request = RequestHelper.GetRestRequest<UniverseNameResponse>()
                                       .SetMethod(Method.POST)
                                       .AddJsonBody(ids);
            return Client.Execute<List<UniverseNameResponse>>(request);
        }

        public IRestResponse<SearchResponse> MakeAuthorizedSearch(string   searchString,
                                                                  string[] categories,
                                                                  int      characterId,
                                                                  string   token)
        {
            // Categories are comma divided, ie: ?categories=solar_system,station,structure

            var request = RequestHelper.GetRestRequest<SearchResponse>(token)
                                       .AddParameter("character_id", characterId, ParameterType.UrlSegment)
                                       .AddParameter("search",       searchString)
                                       .AddParameter("categories",   String.Join(",", categories));
            return Client.Execute<SearchResponse>(request);
        }

        public IRestResponse<UniverseStructureResponse> GetStructureInformation(Int64 structureId, string token = null)
        {
            var request = RequestHelper.GetRestRequest<UniverseStructureResponse>(token)
                                       .AddParameter("structure_id", structureId, ParameterType.UrlSegment);

            var response = Client.Execute<UniverseStructureResponse>(request);
            response.Data.StructureId = structureId;
            return response;
        }

        public IRestResponse<List<int>> GetRegions()
        {
            var request = new RestRequest("/v1/universe/regions/");
            var response = Client.Execute<List<int>>(request);
            return response;
        }
    }
}
