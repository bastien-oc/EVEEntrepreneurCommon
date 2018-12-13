using System.Collections.Generic;
using System.Linq;
using EntrepreneurCommon.Client;
using EntrepreneurCommon.Common.Attributes;
using EntrepreneurCommon.ExtensionMethods;
using EntrepreneurCommon.Helpers;
using EntrepreneurCommon.Models.EsiResponseModels;
using RestSharp;

namespace EntrepreneurCommon.Api
{
    public class WalletApi : CommonApi
    {
        public WalletApi(IEsiRestClient client) : base(client) { }

        public List<CharacterWalletJournalModel> GetCharacterWalletJournalComplete(int characterId, string token)
        {
            var request = RequestHelper.GetRestRequest<CharacterWalletJournalModel>(token)
                                       .SetCharacterId(characterId);
            var result = Client.ExecutePaginated<CharacterWalletJournalModel>(request);

            // TODO: Custom Parameters
            foreach (var entry in result.Items) {
                entry.AssignAnnotationFields(request);
            }

            return result.Items;
        }

        public EsiPaginatedResponse<CharacterWalletJournalModel> GetCharacterWalletJournalCompleteWithInfo(
            int    characterId,
            string token)
        {
            var request = RequestHelper.GetRestRequest<CharacterWalletJournalModel>(token)
                                       .SetCharacterId(characterId);
            var result = Client.ExecutePaginated<CharacterWalletJournalModel>(request);

            // TODO: Custom Parameters
            foreach (var entry in result.Items) {
                entry.AssignAnnotationFields(request);
            }

            return result;
        }

        public List<CorporationWalletJournalModel> GetCorporationWalletJournalComplete(int    corporationId,
                                                                                       int    divisionId,
                                                                                       string token)
        {
            var resourceUri = RequestHelper.GetEndpointUrl<CorporationWalletJournalModel>();
            var request = RequestHelper.GetRestRequest<CorporationWalletJournalModel>(token)
                                       .AddParameter("corporation_id", corporationId, ParameterType.UrlSegment)
                                       .AddParameter("division",       divisionId,    ParameterType.UrlSegment);
            var paginatedResponse = Client.ExecutePaginated<CorporationWalletJournalModel>(request);

            foreach (var entry in paginatedResponse.Items) {
                entry.AssignAnnotationFields(paginatedResponse.Responses.First().Request.Parameters.ToArray());
            }

            return paginatedResponse.Items;
        }

        public EsiPaginatedResponse<CorporationWalletJournalModel> GetCorporationWalletJournalCompleteWithInfo(
            int    corporationId,
            int    divisionId,
            string token)
        {
            var resourceUri = RequestHelper.GetEndpointUrl<CorporationWalletJournalModel>();
            var request = RequestHelper.GetRestRequest<CorporationWalletJournalModel>(token)
                                       .AddParameter("corporation_id", corporationId, ParameterType.UrlSegment)
                                       .AddParameter("division",       divisionId,    ParameterType.UrlSegment);
            var paginatedResponse = Client.ExecutePaginated<CorporationWalletJournalModel>(request);

            foreach (var entry in paginatedResponse.Items) {
                entry.AssignAnnotationFields(paginatedResponse.Responses.First().Request.Parameters.ToArray());
            }

            return paginatedResponse;
        }

        public List<CharacterWalletJournalModel> GetWalletJournal(int characterId, string token, int? page = null)
        {
            var request = RequestHelper.GetRestRequest<CharacterWalletJournalModel>(token)
                                       .SetCharacterId(characterId);
            if (page != null) {
                request.AddParameter("page", page);
            }

            var response = Client.Execute(request);
            return Client.ParseResponse<List<CharacterWalletJournalModel>>(response);
        }

        public EsiPaginatedResponse<CorporationWalletTransactionModel> GetCorporationWalletTransactionsWithInfo(
            int    corporationId,
            int    divisionId,
            string token)
        {
            var request = RequestHelper.GetRestRequest<CorporationWalletTransactionModel>(token)
                                       .SetCorporationId(corporationId)
                                       .AddParameter("division", divisionId, ParameterType.UrlSegment);
            var response = Client.ExecutePaginated<CorporationWalletTransactionModel>(request);
            foreach (var entry in response.Items) {
                entry.AssignAnnotationFields(request);
            }

            return response;
        }

        public List<CorporationWalletTransactionModel> GetCorporationWalletTransactions(int    corporationId,
                                                                                        int    divisionId,
                                                                                        string token)
        {
            var response = GetCorporationWalletTransactionsWithInfo(corporationId, divisionId, token);
            return response.Items;
        }

        // public IRestResponse<List<CorporationWalletTransactionModel>> GetCorpMarketTransactions(int corporationId,
        //     int division, string token)
        // {
        //     //var request = new RestRequest(WalletTransactionCorpObsolete.EndpointVersioned, Method.GET);
        //     var request = RequestHelper.GetRestRequest<CorporationWalletTransactionModel>();
        //     request.AddParameter("corporation_id", corporationId, ParameterType.UrlSegment);
        //     request.AddParameter("division", division, ParameterType.UrlSegment);
        //     request.AddParameter("token", token);
        //     var result = Client.Execute<List<CorporationWalletTransactionModel>>(request);
        //
        //     // Assign request parameters
        //     foreach ( var t in result.Data )
        //     {
        //         t.AssignAnnotationFields(request);
        //     }
        //
        //     return result;
        // }

        public IRestResponse<List<CharacterWalletTransactionModel>> GetCharacterWalletTransactionsWithInfo(
            int    characterId,
            string token)
        {
            //var request = new RestRequest(WalletCharacterTransactionModel.EndpointVersioned, Method.GET);
            var request = RequestHelper.GetRestRequest<CharacterWalletTransactionModel>();
            request.AddParameter("character_id", characterId, ParameterType.UrlSegment);
            request.AddParameter("token",        token);
            var result = Client.Execute<List<CharacterWalletTransactionModel>>(request);
            foreach (var t in result.Data) {
                t.AssignAnnotationFields(request);
            }

            return result;
        }

        public List<CharacterWalletTransactionModel> GetCharacterWalletTransactions(int characterId, string token)
        {
            var request = RequestHelper.GetRestRequest<CharacterWalletTransactionModel>(token)
                                       .SetCharacterId(characterId);
            var result = Client.ExecutePaginated<CharacterWalletTransactionModel>(request);
            foreach (var t in result.Items) {
                t.AssignAnnotationFields(request);
            }

            return result.Items;
        }
    }
}
