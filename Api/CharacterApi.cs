using System;
using EntrepreneurCommon.Client;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using EntrepreneurCommon.ExtensionMethods;
using EntrepreneurCommon.Helpers;
using EntrepreneurCommon.Models.EsiResponseModels;
using Newtonsoft.Json;
using RestSharp;

namespace EntrepreneurCommon.Api
{
    public partial class CharacterApi : CommonApi
    {
        public CharacterApi(IEsiRestClient client) : base(client) { }

        public IRestResponse<CharacterFatigueModel> GetCharacterFatigue(Int32 characterId, string token)
        {
            var request = RequestHelper.GetRestRequest<CharacterFatigueModel>(token)
                                       .SetCharacterId(characterId);
            return Client.Execute<CharacterFatigueModel>(request);
        }

        public IRestResponse<CharacterOnlineModel> GetCharacterOnlineStatus(Int32 characterId, string token)
        {
            var request = RequestHelper.GetRestRequest<CharacterOnlineModel>(token).SetCharacterId(characterId);
            return Client.Execute<CharacterOnlineModel>(request);
        }

        public IRestResponse<CharacterShipModel> GetCharacterShip(Int32 characterId, string token)
        {
            var request = RequestHelper.GetRestRequest<CharacterShipModel>(token)
                                       .AddParameter("character_id", characterId, ParameterType.UrlSegment);
            return Client.Execute<CharacterShipModel>(request);
        }

        public IRestResponse<CharacterLocationModel> GetCharacterLocation(Int32 characterId, string token)
        {
            var request = RequestHelper.GetRestRequest<CharacterLocationModel>(token)
                                       .SetCharacterId(characterId);
            return Client.Execute<CharacterLocationModel>(request);
        }

        public IRestResponse<CharacterPublicInformation> GetCharacterPublicInformation(Int32 characterId)
        {
            var request  = RequestHelper.GetRestRequest<CharacterPublicInformation>().SetCharacterId(characterId);
            var response = Client.Execute<CharacterPublicInformation>(request);
            response.Data.AssignAnnotationFields(request);
            return response;
        }

        public IRestResponse<CharacterRolesModel> GetCharacterRoles(Int32 characterId, string token)
        {
            var request = RequestHelper.GetRestRequest<CharacterRolesModel>(token)
                                       .SetCharacterId(characterId);
            return Client.Execute<CharacterRolesModel>(request);
        }
    }
}
