using Newtonsoft.Json;

namespace EntrepreneurCommon.Models.EsiResponseModels.Deprecated {
    public class CWJExtraInfo
    {
        [JsonProperty("location_id")]
        public long LocationID { get; set; }

        [JsonProperty("transaction_id")]
        public long TransactionID { get; set; }

        [JsonProperty("npc_name")]
        public string NPCName { get; set; }

        [JsonProperty("npc_id")]
        public long NPCID { get; set; }

        [JsonProperty("destroyed_ship_type_id")]
        public long DestroyedShipTypeID { get; set; }

        [JsonProperty("character_id")]
        public long CharacterID { get; set; }

        [JsonProperty("corporation_id")]
        public long CorporationID { get; set; }

        [JsonProperty("alliance_id")]
        public long AllianceID { get; set; }

        [JsonProperty("job_id")]
        public long JobID { get; set; }

        [JsonProperty("contract_id")]
        public long ContractID { get; set; }

        [JsonProperty("system_id")]
        public long SystemID { get; set; }

        [JsonProperty("planet_id")]
        public long PlanetID { get; set; }
    }
}