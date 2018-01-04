using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurEsiApi.Models.Esi;
using Newtonsoft.Json;

namespace EntrepreneurEsiApi.Models.DatabaseModels
{
    [Table("market_orders")]
    public class DbMarketOrderModel
    {
        // Indices
        /// <summary>
        /// Unique key for entry, regardless of region.
        /// </summary>
        [Column("type_id", Order = 1)] [Key] public Int32 TypeId { get; set; }
        [Column("order_id", Order = 2)] [Key] public Int64 OrderId { get; set; }

        // Data
        [Column("region_id")] public Int32 RegionId { get; set; }

        [Column("location_id")] public Int64 LocationId { get; set; }
        [Column("volume_total")] public Int32 VolumeTotal { get; set; }
        [Column("volume_remain")] public Int32 VolumeRemain { get; set; }
        [Column("min_volume")] public Int32 MinVolume { get; set; }
        [Column("price")] public float Price { get; set; }
        [Column("is_buy_order")] public bool IsBuyOrder { get; set; }
        [Column("duration")] public Int32 Duration { get; set; }
        [Column("issued")] public DateTime Issued { get; set; }
        [Column("range")] public string Range { get; set; }

        /// <summary>
        /// Optional Esi response model. Used for Response -> DB transference.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public MarketsRegionOrderResponse response;

        public void AssignFromResponse( MarketsRegionOrderResponse response )
        {
            this.OrderId = response.OrderId;
            this.RegionId = response.RegionId;
            this.TypeId = response.TypeId;
            this.LocationId = response.LocationId;
            this.VolumeTotal = response.VolumeTotal;
            this.VolumeRemain = response.VolumeRemain;
            this.MinVolume = response.MinVolume;
            this.Price = response.Price;
            this.IsBuyOrder = response.IsBuyOrder;
            this.Duration = response.Duration;
            this.Issued = response.Issued;
            this.Range = response.Range;

            this.response = response;
        }
    }
}
