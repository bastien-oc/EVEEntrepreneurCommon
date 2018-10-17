using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurCommon.Models.Esi;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Models.DatabaseModels
{
    [Table("market_regional_history")]
    public class DbMarketHistoryModel
    {
        // Indices
        [Column("type_id", Order = 1)] [Key] public Int32 TypeId { get; set; }
        [Column("region_id", Order = 2)] [Key] public Int32 RegionId { get; set; }

        // Data
        [Column("date", Order = 3)] [DataType(DataType.Date)] [Key] public DateTime Date { get; set; }
        [Column("order_count")] public Int64 OrderCount { get; set; }
        [Column("volume")] public Int64 Volume { get; set; }
        [Column("highest")] public Double Highest { get; set; }
        [Column("average")] public Double Average { get; set; }
        [Column("lowest")] public Double Lowest { get; set; }

        [NotMapped]
        [JsonIgnore]
        public MarketsRegionHistoryResponse Response { get; internal set; }

        public void AssignFromModel( MarketsRegionHistoryResponse response )
        {
            this.TypeId = response.TypeId;
            this.RegionId = response.RegionId;
            this.Date = response.Date;
            this.OrderCount = response.OrderCount;
            this.Volume = response.Volume;
            this.Highest = response.Highest;
            this.Average = response.Average;
            this.Lowest = response.Lowest;
        }
    }
}
