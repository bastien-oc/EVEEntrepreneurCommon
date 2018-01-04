using System;
using System.Linq;
using Newtonsoft.Json;
using Nito.AsyncEx;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace EntrepreneurEsiApi.Models.Esi
{
    /// <summary>
    /// Paginated record of all mining done by a character for the past 30 days
    /// Endpoint: /characters/{character_id}/mining/
    /// </summary>
    [Table("character_mining")]
    public partial class MiningLedgerDataModel
    {
        [JsonIgnore] public static readonly string Endpoint = "/v1/characters/{character_id}/mining/";

        /// <summary>
        /// Identifies the owner of the entry when stored alongside other data. App and Database use only, not part of API.
        /// </summary>
        [JsonIgnore]
        [Column("character_id")] [Key]
        public long CharacterID { get; set; }

        /// <summary>
        /// date string
        /// </summary>
        [J("date")]
        [Column("date")]
        public string Date { get; set; }

        /// <summary>
        /// solar_system_id integer
        /// </summary>
        [J("solar_system_id")]
        [Column("solar_system_id")]
        public long SolarSystemID { get; set; }

        /// <summary>
        /// type_id integer
        /// </summary>
        [J("type_id")]
        [Column("type_id")]
        public long TypeID { get; set; }

        /// <summary>
        /// quantity integer
        /// </summary>
        [J("quantity")]
        [Column("quantity")]
        public long Quantity { get; set; }

        /// <summary>
        /// App use only. Returns the name of the material mined.
        /// </summary>
        [J("type_name")] public String TypeName { get; set; }
        /// <summary>
        /// App use only. Returns the name of the solar system.
        /// </summary>
        public String SolarSystemName { get; set; }
    }

    /// <summary>
    /// Paginated list of all entities capable of observing and recording mining for a corporation
    /// </summary>
    [Table("mining_observers")]
    public class MiningObserver
    {
        public static readonly String Endpoint = "/v1/corporation/{corporation_id}/mining/observers/";

        /// <summary>
        /// Database use only, allows us to remember which corp had access to which observer.
        /// </summary>
        [Column("owner_id")] public Int32 OwnerID { get; set; }
        /// <summary>
        /// The entity that was observing the asteroid field when it was mined.
        /// </summary>
        [Column("observer_id")] [J("observer_id")] public Int64 ObserverID { get; set; }
        /// <summary>
        /// The category of the observing entity
        /// </summary>
        [Column("observer_type")] [J("observer_type")] public String ObserverType { get; set; }
        /// <summary>
        /// last_updated string
        /// </summary>
        [Column("last_updated")] [J("last_updated")] public DateTime LastUpdated { get; set; }
    }

    /// <summary>
    /// Paginated record of all mining seen by an observer
    /// Path: corporation_id, observer_id
    /// Query: datasource, page, token user_agent
    /// Header: X-User-Agent
    /// </summary>
    [Table("mining_observer_ledger")]
    public class MiningObserverMiningDone
    {
        public static readonly String Endpoint = "/v1/corporation/{corporation_id}/mining/observers/{observer_id}/";
        [JsonIgnore] [Column("owner_id")] [Key] public Int32 OwnerID { get; set; }
        [JsonIgnore] [Column("observer_id")] [Key] public Int64 ObserverID { get; set; }
        [Column("character_id")] [Key] [J("character_id")] public Int32 CharacterID { get; set; }
        [Column("recorded_corporation_id")] [Key] [J("recorded_corporation_id")] public Int32 RecordedCorporationID { get; set; }
        [Column("type_id")] [J("type_id")] public Int32 TypeID { get; set; }
        [Column("quantity")] [J("quantity")] public Int32 Quantity { get; set; }
        [Column("last_updated")] [J("last_updated")] public string LastUpdate { get; set; }


        // App use only. Returns the name of the material mined.
        [J("material_name")] String TypeName { get; set; }
        [J("character_name")] String CharacterName { get; set; }
        [J("corporation_name")] String CorporationName { get; set; }
        [J("market_value")] Single MarketValue { get; set; }
        [J("refine_value")] Double RefineValue { get; set; }
    }

    /// <summary>
    /// Extraction timers for all moon chunks being extracted by refineries belonging to a corporation.
    /// </summary>
    public class MiningExtraction
    {
        public static readonly String Endpoint = "/v1/corporation/{corporation_id}/mining/extractions/";
        /// <summary>
        /// Database use only. Records to which corp does this record belong to.
        /// </summary>
        [Column("corporation_id")] [JsonIgnore] public Int32 CorporationID { get; set; }

        /// <summary>
        /// structure_id integer
        /// </summary>
        [Column("structure_id")] [J("structure_id")] public Int64 StructureID { get; set; }
        /// <summary>
        /// moon_id integer
        /// </summary>
        [Column("moon_id")] [J("moon_id")] public Int32 MoonID { get; set; }
        /// <summary>
        /// The time at which the current extraction was initiated.
        /// </summary>
        [Column("extraction_start_time")] [J("extraction_start_time")] public DateTime ExtractionStartTime { get; set; }
        /// <summary>
        /// The time at which the chunk being extracted will arrive and can be fractured by the moon mining drill.
        /// </summary>
        [Column("chunk_arrival_time")] [J("chunk_arrival_time")] public DateTime ChunkArrivalTime { get; set; }
        /// <summary>
        /// The time at which the chunk being extracted will naturally fracture if it is not first fractured by the moon mining drill.
        /// </summary>
        [Column("natural_decay_time")] [J("natural_decay_time")] public DateTime NaturalDecayTime { get; set; }
    }


}