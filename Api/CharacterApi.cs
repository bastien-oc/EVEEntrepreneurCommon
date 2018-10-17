using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurCommon.Models.EsiResponseModels;
using EntrepreneurCommon.Api;
using EntrepreneurCommon.Models.Esi;
using RestSharp;

namespace EntrepreneurCommon.Api
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

        public IRestResponse<LocationOnline> GetCharacterOnlineStatus(Int32 characterId, string token)
        {
            var request = new RestRequest(LocationOnline.Endpoint, Method.GET);
            request.AddUrlSegment("character_id", characterId);
            request.AddParameter("token", token);
            return ApiClient.Execute<LocationOnline>(request);
        }

        public IRestResponse<LocationShip> GetCharacterShip( Int32 characterId, string token )
        {
            var request = new RestRequest(LocationShip.Endpoint, Method.GET);
            request.AddUrlSegment("character_id", characterId);
            request.AddParameter("token", token);
            return ApiClient.Execute<LocationShip>(request);
        }

        public IRestResponse<LocationLocation> GetCharacterLocation( Int32 characterId, string token )
        {
            var request = new RestRequest(LocationLocation.Endpoint, Method.GET);
            request.AddUrlSegment("character_id", characterId);
            request.AddParameter("token", token);
            return ApiClient.Execute<LocationLocation>(request);
        }

        public IRestResponse<CharacterPublicInformation> GetCharacterPublicInformation(Int32 characterId)
        {
            var request = new RestRequest(CharacterPublicInformation.Endpoint);
            request.AddUrlSegment("character_id", characterId);
            return ApiClient.Execute<CharacterPublicInformation>(request);
        }
    }
}
