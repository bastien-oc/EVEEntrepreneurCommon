using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurEsiApi.Models.Esi;
using RestSharp;

namespace EntrepreneurEsiApi.Api
{
    public partial class CharacterApi : CommonApi
    {
        public CharacterApi( EsiApiClient apiClient ) : base(apiClient)
        {
        }

        public IRestResponse<CharacterFatigueModel> GetCharacterFatigue( Int32 characterId, string token )
        {
            var request = new RestRequest(CharacterFatigueModel.EndpointVersioned, Method.GET);
            request.AddUrlSegment("character_id", characterId);
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = ApiClient.Execute<CharacterFatigueModel>(request);
            return response;
        }
    }
}
