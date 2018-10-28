using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntrepreneurCommon.Models.DatabaseModels
{
    [Table("mining_extraction")]
    public class DbIndustryMiningExtraction
    {
        [Column("corporation_id")] public Int32 CorporationID { get; set; }

        /// <summary>
        /// structure_id integer
        /// </summary>
        [Key, Column("structure_id", Order = 0)] public Int64 StructureID { get; set; }
        /// <summary>
        /// moon_id integer
        /// </summary>
        [Key, Column("moon_id", Order = 1)] public Int32 MoonID { get; set; }
        /// <summary>
        /// The time at which the current extraction was initiated.
        /// </summary>
        [Key, Column("extraction_start_time", Order = 2)] public DateTime ExtractionStartTime { get; set; }
        /// <summary>
        /// The time at which the chunk being extracted will arrive and can be fractured by the moon mining drill.
        /// </summary>
        [Column("chunk_arrival_time")] public DateTime ChunkArrivalTime { get; set; }
        /// <summary>
        /// The time at which the chunk being extracted will naturally fracture if it is not first fractured by the moon mining drill.
        /// </summary>
        [Column("natural_decay_time")] public DateTime NaturalDecayTime { get; set; }
    }
}
