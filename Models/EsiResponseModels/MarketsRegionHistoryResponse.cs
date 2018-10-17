using System;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using C = System.ComponentModel.DataAnnotations.Schema.ColumnAttribute;

namespace EntrepreneurCommon.Models.Esi
{
    /// <summary>
    /// Return a list of historical market statistics for the specified type in a region
    /// </summary>
    public class MarketsRegionHistoryResponse
    {
        public static string Endpoint { get => "/v1/markets/{region_id}/history/"; }

        [J("type_id")] [C("type_id")] public Int32 TypeId { get; set; }

        [J("date")] [C("date")] public DateTime Date { get; set; }
        [J("order_count")] [C("order_count")] public Int64 OrderCount { get; set; }
        [J("volume")] [C("volume")] public Int64 Volume { get; set; }
        [J("highest")] [C("highest")] public Double Highest { get; set; }
        [J("average")] [C("average")] public Double Average { get; set; }
        [J("lowest")] [C("lowest")] public Double Lowest { get; set; }

        // Additional fields for app use
        private int _regionId = 0;
        public Int32 RegionId { get => _regionId; set => _regionId = value; }
    }
}
