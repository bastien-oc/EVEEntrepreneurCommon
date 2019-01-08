using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [EsiEndpoint("/v2/characters/{character_id}/roles/", false, new[] {EsiCharacterScopes.CorporationRolesRead})]
    public class CharacterRolesModel : IEsiResponseModel
    {
        [RestParameterMapping]
        public int CharacterId { get; set; }

        [JsonProperty("roles")]          public List<RolesEnum> Roles        { get; set; }
        [JsonProperty("roles_at_hq")]    public List<RolesEnum> RolesAtHq    { get; set; }
        [JsonProperty("roles_at_base")]  public List<RolesEnum> RolesAtBase  { get; set; }
        [JsonProperty("roles_at_other")] public List<RolesEnum> RolesAtOther { get; set; }
    }
}
