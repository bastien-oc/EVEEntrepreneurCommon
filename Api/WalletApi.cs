using System.Collections.Generic;
using EntrepreneurCommon.Models.Esi;
using RestSharp;

namespace EntrepreneurCommon.Api
{
    public class WalletApi : CommonApi
    {
        public WalletApi(EsiApiClient apiClient) : base(apiClient) { }

        public List<WalletJournalModelCharV4> GetCharacterWalletJournalComplete(int characterId, string token)
        {
            var request = new RestRequest(WalletJournalModelCharV4.Endpoint);
            request.AddParameter("character_id", characterId, ParameterType.UrlSegment);
            request.AddParameter("token", token);

            //Parameter param = new Parameter() {
            //    Name = "character_id",
            //    Value = characterId,
            //    Type = ParameterType.UrlSegment
            //};
            var result = ApiClient.ExecutePaginated<WalletJournalModelCharV4>(request);
            foreach (var entry in result.Items)
                entry.WalletOwnerID = characterId;
            return result.Items;
        }

        public EsiPaginatedResponse<WalletJournalModelCharV4> GetCharacterWalletJournalCompleteWithInfo(int characterId,
            string token)
        {
            var request = new RestRequest(WalletJournalModelCharV4.Endpoint);
            request.AddParameter("character_id", characterId, ParameterType.UrlSegment);
            request.AddParameter("token", token);
            var result = ApiClient.ExecutePaginated<WalletJournalModelCharV4>(request);
            foreach (var entry in result.Items)
                entry.WalletOwnerID = characterId;
            return result;
        }

        public List<WalletJournalModelCorp> GetCorporationWalletJournalComplete(int corporationId, int divisionId,
            string token)
        {
            Parameter param1 = new Parameter() {
                Name = "corporation_id",
                Value = corporationId,
                Type = ParameterType.UrlSegment
            };
            Parameter param2 = new Parameter() {Name = "division", Value = divisionId, Type = ParameterType.UrlSegment};

            var result = ApiClient.ExecutePaginated<WalletJournalModelCorp>(
                WalletJournalModelCorp.EndpointVersioned, parameters: new Parameter[] {param1, param2}, token: token);
            foreach (var entry in result.Items) {
                entry.WalletOwnerID = corporationId;
                entry.WalletDivision = divisionId;
            }
            return result.Items;
        }

        public EsiPaginatedResponse<WalletJournalModelCorp> GetCorporationWalletJournalCompleteWithInfo(
            int corporationId, int divisionId, string token)
        {
            Parameter param1 = new Parameter() {
                Name = "corporation_id",
                Value = corporationId,
                Type = ParameterType.UrlSegment
            };
            Parameter param2 = new Parameter() {Name = "division", Value = divisionId, Type = ParameterType.UrlSegment};

            var response = ApiClient.ExecutePaginated<WalletJournalModelCorp>(
                WalletJournalModelCorp.EndpointVersioned, parameters: new Parameter[] {param1, param2}, token: token);
            foreach (var entry in response.Items)
            {
                entry.WalletOwnerID = corporationId;
                entry.WalletDivision = divisionId;
            }

            return response;
        }

        public List<WalletJournalModelCharV4> GetWalletJournal(int characterId, string token, int? page = null)
        {
            RestRequest request = new RestRequest(WalletJournalModelCharV4.Endpoint, Method.GET);
            request.AddParameter("character_id", characterId, ParameterType.UrlSegment);
            request.AddParameter("token", token);
            if (page != null) {
                request.AddParameter("page", page);
            }

            var response = ApiClient.Execute(request);
            return ApiClient.ParseResponse<List<WalletJournalModelCharV4>>(response);
        }
    }
}