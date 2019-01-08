using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    /// <summary>
    /// Extraction timers for all moon chunks being extracted by refineries belonging to a corporation.
    /// </summary>
    [EsiEndpoint("/v1/corporation/{corporation_id}/mining/extractions/", true,
        new[] {EsiCorporationScopes.IndustryMiningRead})]
    public class CorporationMiningExtractionModel : IEsiResponseModel
    {
        public static readonly string Endpoint = "/v1/corporation/{corporation_id}/mining/extractions/";

        /// <summary>
        /// Database use only. Records to which corp does this record belong to.
        /// </summary>
        [Column("corporation_id")]
        [JsonIgnore]
        [RestParameterMapping("corporation_id")]
        public int CorporationId { get; set; }

        /// <summary>
        /// structure_id integer
        /// </summary>
        [Key, Index("UNIQUE", IsUnique = true, Order = 0), Column("structure_id", Order = 0)]
        public long StructureID { get; set; }

        /// <summary>
        /// moon_id integer
        /// </summary>
        [Key, Index("UNIQUE", IsUnique = true, Order = 1), Column("moon_id", Order = 1)]
        public int MoonID { get; set; }

        /// <summary>
        /// The time at which the current extraction was initiated.
        /// </summary>
        [Key, Index("UNIQUE", IsUnique = true, Order = 2), Column("extraction_start_time", Order = 2)]
        public DateTime ExtractionStartTime { get; set; }

        /// <summary>
        /// The time at which the chunk being extracted will arrive and can be fractured by the moon mining drill.
        /// </summary>
        [Column("chunk_arrival_time")]
        public DateTime ChunkArrivalTime { get; set; }

        /// <summary>
        /// The time at which the chunk being extracted will naturally fracture if it is not first fractured by the moon mining drill.
        /// </summary>
        [Column("natural_decay_time")]
        public DateTime NaturalDecayTime { get; set; }
    }
}