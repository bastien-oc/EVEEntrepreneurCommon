using System.Collections.Generic;
using EntrepreneurCommon.Client;
using EntrepreneurCommon.ExtensionMethods;
using EntrepreneurCommon.Helpers;
using EntrepreneurCommon.Models.EsiResponseModels;
using RestSharp;

namespace EntrepreneurCommon.Api
{
    public class IndustryApi : CommonApi
    {
        public IndustryApi(IEsiRestClient client) : base(client) { }

        /// <summary>
        ///     Return cost indices for solar systems
        /// </summary>
        /// <returns></returns>
        public IRestResponse<List<IndustrySystemModel>> GetIndustrySystemIndicesData()
        {
            var request = RequestHelper.GetRestRequest<IndustrySystemModel>();
            return Client.Execute<List<IndustrySystemModel>>(request);
        }

        /// <summary>
        ///     Return cost indices for solar systems
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IndustrySystemModel> GetIndustrySystemIndices()
        {
            var response = GetIndustrySystemIndicesData();
            return response.Data;
        }

        /// <summary>
        ///     List industry jobs placed by a character
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="token"></param>
        /// <param name="includeCompleted"></param>
        /// <returns></returns>
        public IRestResponse<List<CharacterIndustryJobsModel>> GetCharacterIndustryJobsData(int    characterId,
                                                                                            string token,
                                                                                            bool includeCompleted =
                                                                                                true)
        {
            var request = RequestHelper.GetRestRequest<CharacterIndustryJobsModel>(token)
                                       .AddParameter("character_id",      characterId, ParameterType.UrlSegment)
                                       .AddParameter("include_completed", includeCompleted);
            return Client.Execute<List<CharacterIndustryJobsModel>>(request);
        }

        /// <summary>
        ///     List industry jobs placed by a character
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="token"></param>
        /// <param name="includeCompleted"></param>
        /// <returns></returns>
        public IEnumerable<CharacterIndustryJobsModel> GetCharacterIndustryJobs(int    characterId,
                                                                                string token,
                                                                                bool   includeCompleted = true)
        {
            var response = GetCharacterIndustryJobsData(characterId, token, includeCompleted);
            return response.Data;
        }

        public IRestResponse<List<CorporationMiningExtractionModel>> GetMiningExtractions(int    corporationId,
                                                                                          string token)
        {
            var request = new RestRequest(CorporationMiningExtractionModel.Endpoint);
            request.AddParameter("corporation_id", corporationId, ParameterType.UrlSegment);
            request.AddParameter("token",          token);
            var response = Client.Execute<List<CorporationMiningExtractionModel>>(request);
            return response;
        }

        public EsiPaginatedResponse<CorporationMiningObserverLedgerModel> GetMiningLedgerCorp(int    corporationId,
                                                                                              long   observerId,
                                                                                              string token)
        {
            var request = RequestHelper.GetRestRequest<CorporationMiningObserverLedgerModel>(token)
                                       .AddParameter("corporation_id", corporationId, ParameterType.UrlSegment)
                                       .AddParameter("observer_id",    observerId,    ParameterType.UrlSegment);
            // request.AddParameter("token", token, ParameterType.QueryString);

            var response = Client.ExecutePaginated<CorporationMiningObserverLedgerModel>(request);

            foreach (var r in response.Items) {
                r.CorporationId = corporationId;
                r.ObserverId = observerId;
            }

            return response;
        }

        public EsiPaginatedResponse<CorporationMiningObserversModel> GetMiningObservers(int corporationId, string token)
        {
            var request = RequestHelper.GetRestRequest<CorporationMiningObserversModel>(token)
                                       .SetCorporationId(corporationId);

            var response = Client.ExecutePaginated<CorporationMiningObserversModel>(request);

            foreach (var r in response.Items) {
                r.CorporationId = corporationId;
            }

            return response;
        }

        public IEnumerable<CharacterMiningLedgerModel> GetCharacterMiningLedger(int characterId, string token)
        {
            return GetCharacterMiningLedgerA(characterId, token).Items;
        }

        public EsiPaginatedResponse<CharacterMiningLedgerModel> GetCharacterMiningLedgerA(int characterId, string token)
        {
            //var resourceUri = EndpointHelper.GetEndpointUrl<CharacterMiningLedgerModel>();
            var request = RequestHelper.GetRestRequest<CharacterMiningLedgerModel>();
            //RestRequest(CharacterMiningLedgerModel.Endpoint);
            request.AddParameter("character_id", characterId, ParameterType.UrlSegment);
            request.AddParameter("token",        token);
            var response = Client.ExecutePaginated<CharacterMiningLedgerModel>(request);
            foreach (var r in response.Items) {
                r.CharacterId = characterId;
            }

            return response;
        }
    }
}
