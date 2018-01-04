using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntrepreneurEsiApi.Models.DatabaseModels
{
    public class DbMarketEntityOrder
    {
        [Column("order_id")] public Int64 OrderId { get; set; }
        [Column("type_id")] public Int32 TypeId { get; set; }
        [Column("region_id")] public Int32 RegionId { get; set; }
        [Column("location_id")] public Int64 LocationId { get; set; }
        [Column("range")] public string Range { get; set; }
        [Column("is_buy_order")] public bool IsBuyOrder { get; set; }
        [Column("price")] public float Price { get; set; }


        [Column("volume_total")] public Int32 VolumeTotal { get; set; }
        [Column("volume_remain")] public Int32 VolumeRemain { get; set; }
        [Column("issued")] public string Issued { get; set; }

        [Column("state")] public string State { get; set; }
        [Column("min_volume")] public Int32 MinVolume { get; set; }
        [Column("account_id")] public Int32 AccountId { get; set; }
        [Column("duration")] public Int32 Duration { get; set; }

        [Column("is_corp")] public bool IsCorp { get; set; }
        [Column("escrow")] public float Escrow { get; set; }
    }
}
