using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using C = System.ComponentModel.DataAnnotations.Schema.ColumnAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    /// <summary>
    ///     Return a list of historical market statistics for the specified type in a region
    /// </summary>
    [Table("markets_region_history")]
    [EsiEndpoint("/v1/markets/{region_id}/history/")]
    public class MarketsRegionHistoryResponse : IEsiResponseModel
    {
        [Key]
        [C(Order = 0)]
        [Index("UNIQUE", IsUnique = true, Order = 0)]
        [RestParameterMapping("region_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RegionId { get; set; }

        [Key]
        [Column(Order = 1)]
        [Index("UNIQUE", IsUnique = true, Order = 1)]
        [RestParameterMapping("type_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [Key]
        [Column(Order = 2)]
        [Index("UNIQUE", IsUnique = true, Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime Date { get; set; }

        public long   OrderCount { get; set; }
        public long   Volume     { get; set; }
        public double Highest    { get; set; }
        public double Average    { get; set; }
        public double Lowest     { get; set; }
    }
}
