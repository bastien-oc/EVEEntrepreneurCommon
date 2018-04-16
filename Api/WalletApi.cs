using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurEsiApi.Api.SystemModels;
using EntrepreneurEsiApi.Models.Esi;
using RestSharp;

namespace EntrepreneurEsiApi.Api
{
    public class WalletApi:CommonApi
    {
        public WalletApi( EsiApiClient apiClient ) : base(apiClient)
        {
        }

        public List<WalletJournalModelChar> GetCharacterWalletJournalComplete( int characterId, string token )
        {
            Parameter param = new Parameter() {
                Name = "character_id",
                Value = characterId,
                Type = ParameterType.UrlSegment
            };
            var result = ApiClient.ExecutePaginated<WalletJournalModelChar>(WalletJournalModelChar.Endpoint, Method.GET, new Parameter[] { param }, token, null);
            return result.Items;
        }

        public EsiPaginatedResponse<WalletJournalModelChar> GetCharacterWalletJournalCompleteWithInfo( int characterId, string token )
        { return ApiClient.ExecutePaginated<WalletJournalModelChar>(WalletJournalModelChar.Endpoint, Method.GET, null, token, null); }

        public List<WalletJournalModelCorp> GetCorporationWalletJournalComplete(int corporationId, int divisionId, string token)
        {
            Parameter param1 = new Parameter() { Name = "corporation_id", Value = corporationId, Type = ParameterType.UrlSegment };
            Parameter param2 = new Parameter() { Name = "division", Value = divisionId, Type = ParameterType.UrlSegment };

            var result = ApiClient.ExecutePaginated<WalletJournalModelCorp>(
                WalletJournalModelCorp.EndpointVersioned, parameters: new Parameter[] { param1, param2 }, token: token);
            return result.Items;
        }

        public EsiPaginatedResponse<WalletJournalModelCorp> GetCorporationWalletJournalCompleteWithInfo (int corporationId, int divisionId, string token)
        {
            Parameter param1 = new Parameter() { Name = "corporation_id", Value = corporationId, Type = ParameterType.UrlSegment };
            Parameter param2 = new Parameter() { Name = "division", Value = divisionId, Type = ParameterType.UrlSegment };

            return ApiClient.ExecutePaginated<WalletJournalModelCorp>(
                WalletJournalModelCorp.EndpointVersioned, parameters: new Parameter[] { param1, param2 }, token: token);
        }

        public List<WalletJournalModelChar> GetWalletJournal( int characterId, string token, int? page = null )
        {
            RestRequest request = new RestRequest(WalletJournalModelChar.Endpoint, Method.GET);
            request.AddParameter("character_id", characterId, ParameterType.UrlSegment);
            request.AddParameter("token", token);
            if (page != null) {
                request.AddParameter("page", page);
            }
            var response = ApiClient.Execute(request);
            return ApiClient.ParseResponse<List<WalletJournalModelChar>>(response);
        }
    }
}
