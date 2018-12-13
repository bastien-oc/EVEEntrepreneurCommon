using System;
using System.ComponentModel.DataAnnotations;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    /// <summary>
    /// Return a list of orders in a region; Paginated
    /// Path: region_id*
    /// Query: datasource, order_type*, page, type_id, user_agent
    /// </summary>
    [EsiEndpoint("/v1/markets/structures/{structure_id}/", true, new[] {EsiCharacterScopes.MarketsStructureMarkets})]
    public class MarketsStructureOrderResponse : IEsiResponseModel
    {
        [RestParameterMapping]
        public long StructureId { get; set; }

        [Key]
        [J("order_id")]
        public long OrderId { get; set; }

        [J("type_id")]
        public int TypeId { get; set; }

        [J("location_id")]
        public long LocationId { get; set; }

        [J("volume_total")]
        public int VolumeTotal { get; set; }

        [J("volume_remain")]
        public int VolumeRemain { get; set; }

        [J("min_volume")]
        public int MinVolume { get; set; }

        [J("price")]
        public float Price { get; set; }

        [J("is_buy_order")]
        public bool IsBuyOrder { get; set; }

        [J("duration")]
        public int Duration { get; set; }

        [J("issued")]
        public DateTime Issued { get; set; }

        [J("range")]
        public string Range { get; set; }
    }
}