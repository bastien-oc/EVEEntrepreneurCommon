using System;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurEsiApi.Models.Esi
{
    /// <summary>
    /// List market orders placed by a character
    /// </summary>
    public class EntityMarketOrders
    {
        [J("order_id")] public Int64 OrderId { get; set; }
        [J("type_id")] public Int32 TypeId { get; set; }
        [J("region_id")] public Int32 RegionId { get; set; }
        [J("location_id")] public Int64 LocationId { get; set; }
        [J("range")] public string Range { get; set; }
        [J("is_buy_order")] public bool IsBuyOrder { get; set; }
        [J("price")] public float Price { get; set; }


        [J("volume_total")] public Int32 VolumeTotal { get; set; }
        [J("volume_remain")] public Int32 VolumeRemain { get; set; }
        [J("issued")] public string Issued { get; set; }

        [J("state")] public string State { get; set; }
        [J("min_volume")] public Int32 MinVolume { get; set; }
        [J("account_id")] public Int32 AccountId { get; set; }
        [J("duration")] public Int32 Duration { get; set; }

        [J("is_corp")] public bool IsCorp { get; set; }
        [J("escrow")] public float Escrow { get; set; }
    }
}
