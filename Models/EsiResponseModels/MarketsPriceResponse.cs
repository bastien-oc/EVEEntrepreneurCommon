using System;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurEsiApi.Models.Esi
{
    //public enum OrderRange { station, region, solarsystem, 1, 2, 3, 4, 5, 10, 20, 30, 40 }

    public class MarketsPriceResponse
    {
        public static string Endpoint { get => "/v1/markets/prices/"; }

        [J("type_id")] public Int32 TypeId { get; set; }
        [J("average_price")] public float AveragePrice { get; set; }
        [J("adjusted_price")] public float AdjustedPrice { get; set; }
    }
}
