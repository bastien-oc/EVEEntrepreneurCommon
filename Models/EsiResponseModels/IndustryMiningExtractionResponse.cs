using System;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntrepreneurCommon.Models.Esi
{
    /// <summary>
    /// Extraction timers for all moon chunks being extracted by refineries belonging to a corporation.
    /// </summary>
    public class IndustryMiningExtractionResponse
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