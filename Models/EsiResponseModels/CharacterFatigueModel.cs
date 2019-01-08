using System;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels {
    [EsiEndpoint("/v1/characters/{character_id}/fatigue/", false, new[] {EsiCharacterScopes.FatigueRead})]
    public class CharacterFatigueModel : IEsiResponseModel {
        [RestParameterMapping]          public int      CharacterId           { get; set; }
        [J("last_jump_date")]           public DateTime LastJumpDate          { get; set; }
        [J("jump_fatigue_expire_date")] public DateTime JumpFatigueExpireDate { get; set; }
        [J("last_update_date")]         public DateTime LastUpdateDate        { get; set; }
    }
}
