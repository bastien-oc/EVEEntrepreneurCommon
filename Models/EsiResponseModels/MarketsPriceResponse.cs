using System;
using System.ComponentModel.DataAnnotations;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    //public enum OrderRange { station, region, solarsystem, 1, 2, 3, 4, 5, 10, 20, 30, 40 }

    [EsiEndpoint("/v1/markets/prices/")]
    public class MarketsPriceResponse : IEsiResponseModel
    {
        public static string Endpoint {
            get => "/v1/markets/prices/";
        }

        [Key]
        [J("type_id")]
        public int TypeId { get; set; }

        [J("average_price")]
        public float AveragePrice { get; set; }

        [J("adjusted_price")]
        public float AdjustedPrice { get; set; }
    }
}