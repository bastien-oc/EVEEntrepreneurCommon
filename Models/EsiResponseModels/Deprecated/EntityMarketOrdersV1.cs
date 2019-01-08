using System;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    /// <summary>
    /// List market orders placed by a character
    /// </summary>
    [Obsolete("This endpoint is obsolete", true)]
    public class EntityMarketOrdersV1
    {
        [J("order_id")] public long OrderId { get; set; }
        [J("type_id")] public int TypeId { get; set; }
        [J("region_id")] public int RegionId { get; set; }
        [J("location_id")] public long LocationId { get; set; }
        [J("range")] public string Range { get; set; }
        [J("is_buy_order")] public bool IsBuyOrder { get; set; }
        [J("price")] public float Price { get; set; }


        [J("volume_total")] public int VolumeTotal { get; set; }
        [J("volume_remain")] public int VolumeRemain { get; set; }
        [J("issued")] public string Issued { get; set; }

        [J("state")] public string State { get; set; }
        [J("min_volume")] public int MinVolume { get; set; }
        [J("account_id")] public int AccountId { get; set; }
        [J("duration")] public int Duration { get; set; }

        [J("is_corp")] public bool IsCorp { get; set; }
        [J("escrow")] public float Escrow { get; set; }
    }
}
