using System;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntrepreneurCommon.Models.Esi
{
    /// <summary>
    /// Paginated record of all mining seen by an observer
    /// Path: corporation_id, observer_id
    /// Query: datasource, page, token user_agent
    /// Header: X-User-Agent
    /// </summary>
    //[Table("mining_observer_ledger"), Obsolete("Make separate DbModel", true)]
    public class IndustryMiningObserverMiningDone
    {
        [NotMapped]
        public static readonly String Endpoint = "/v1/corporation/{corporation_id}/mining/observers/{observer_id}/";

        // DbModel Keys
        [Key, Index("UNIQUE", 0, IsUnique = true), Column("owner_id", Order = 0), JsonIgnore]
        public Int32 OwnerID { get; set; }
        [Key, Index("UNIQUE", 1, IsUnique = true), Column("observer_id", Order = 1), JsonIgnore]
        public Int64 ObserverID { get; set; }

        // Data
        [Key, Index("UNIQUE", 2, IsUnique = true),  Column("character_id", Order = 2), J("character_id")]
        public Int32 CharacterID { get; set; }
        [Key, Index("UNIQUE", 3, IsUnique = true), Column("recorded_corporation_id", Order = 3), J("recorded_corporation_id")]
        public Int32 RecordedCorporationID { get; set; }
        [Index("UNIQUE", 4, IsUnique = true), Column("type_id"), J("type_id")]
        public Int32 TypeID { get; set; }
        [Column("quantity"), J("quantity")]
        public Int32 Quantity { get; set; }
        [Index("UNIQUE", 5, IsUnique = true), Column("last_updated"), J("last_updated")]
        public string LastUpdated { get; set; }
    }


}