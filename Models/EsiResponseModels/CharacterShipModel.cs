using System;
using System.ComponentModel.DataAnnotations;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [EsiEndpoint("/v1/characters/{character_id}/ship/", false, new[] {EsiCharacterScopes.LocationShipTypeRead})]
    public class CharacterShipModel : IEsiResponseModel
    {
        [Key, RestParameterMapping]
        public int CharacterId { get; set; }

        [JsonProperty("ship_type_id")]
        public int ShipTypeId { get; set; }

        [JsonProperty("ship_item_id")]
        public long ShipItemId { get; set; }

        [JsonProperty("ship_name")]
        public string ShipName { get; set; }
    }
}