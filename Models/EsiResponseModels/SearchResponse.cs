using System.Collections.Generic;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [EsiEndpoint("/v3/characters/{character_id}/search/", false, new[] {EsiCharacterScopes.SearchStructures})]
    public class SearchResponse : IEsiResponseModel
    {
        [JsonProperty("agent")]
        public List<int> Agent { get; set; }

        [JsonProperty("alliance")]
        public List<int> Alliance { get; set; }

        [JsonProperty("character")]
        public List<int> Character { get; set; }

        [JsonProperty("constellation")]
        public List<int> Constellation { get; set; }

        [JsonProperty("corporation")]
        public List<int> Corporation { get; set; }

        [JsonProperty("faction")]
        public List<int> Faction { get; set; }

        [JsonProperty("inventorytype")]
        public List<int> InventoryType { get; set; }

        [JsonProperty("region")]
        public List<int> Region { get; set; }

        [JsonProperty("solarsystem")]
        public List<int> SolarSystem { get; set; }

        [JsonProperty("station")]
        public List<int> Station { get; set; }

        [JsonProperty("structure")]
        public List<int> Structure { get; set; }

        [JsonProperty("wormhole")]
        public List<int> Wormhole { get; set; }
    }
}