using System;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurEsiApi.Models.Esi
{
    /// <summary>
    /// Return a list of orders in a region; Paginated
    /// Path: region_id*
    /// Query: datasource, order_type*, page, type_id, user_agent
    /// </summary>
    public class MarketsStructureOrderResponse
    {

        public static string Endpoint { get => "/v1/markets/structures/{structure_id}/"; }

        [J("order_id")] public Int64 OrderId { get; set; }
        [J("type_id")] public Int32 TypeId { get; set; }
        [J("location_id")] public Int64 LocationId { get; set; }
        [J("volume_total")] public Int32 VolumeTotal { get; set; }
        [J("volume_remain")] public Int32 VolumeRemain { get; set; }
        [J("min_volume")] public Int32 MinVolume { get; set; }
        [J("price")] public float Price { get; set; }
        [J("is_buy_order")] public bool IsBuyOrder { get; set; }
        [J("duration")] public Int32 Duration { get; set; }
        [J("issued")] public string Issued { get; set; }
        [J("range")] public string Range { get; set; }

        private int _regionId = 0;
        public int RegionId { get => _regionId; set => _regionId = value; }
        public DateTime IssuedDateTime { get => DateTime.Parse(Issued); }
    }
}
