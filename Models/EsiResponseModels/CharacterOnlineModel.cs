using System;
using System.ComponentModel.DataAnnotations;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [EsiEndpoint("/v2/characters/{character_id}/online/", false, new []{EsiCharacterScopes.LocationOnlineRead})]
    public class CharacterOnlineModel : IEsiResponseModel
    {
        [Key, RestParameterMapping]
        public int CharacterId { get; set; }
        [J("online")] public bool Online { get; set; }
        [J("last_login")] public DateTime? LastLogin { get; set; }
        [J("last_logout")] public DateTime? LastLogout { get; set; }
        [J("logins")] public int? Logins { get; set; }
    }
}
