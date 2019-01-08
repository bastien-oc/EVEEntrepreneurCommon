using System;
using System.Runtime.Serialization;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [DataContract]
    [EsiEndpoint("/corporations/{corporation_id}/orders/", true, new[] {EsiCorporationScopes.MarketsOrdersRead})]
    public partial class CorporationMarketOrdersModel : IEsiResponseModel
    {
        /// <summary>
        /// Valid order range, numbers are ranges in jumps
        /// </summary>
        /// <value>Valid order range, numbers are ranges in jumps</value>
        [DataMember(Name = "range", EmitDefaultValue = false)]
        public RangeEnum Range { get; set; }

        /// <summary>
        /// Number of days for which order is valid (starting from the issued date). An order expires at time issued + duration
        /// </summary>
        /// <value>Number of days for which order is valid (starting from the issued date). An order expires at time issued + duration</value>
        [DataMember(Name = "duration", EmitDefaultValue = false)]
        public int? Duration { get; set; }

        /// <summary>
        /// For buy orders, the amount of ISK in escrow
        /// </summary>
        /// <value>For buy orders, the amount of ISK in escrow</value>
        [DataMember(Name = "escrow", EmitDefaultValue = false)]
        public double? Escrow { get; set; }

        /// <summary>
        /// True if the order is a bid (buy) order
        /// </summary>
        /// <value>True if the order is a bid (buy) order</value>
        [DataMember(Name = "is_buy_order", EmitDefaultValue = false)]
        public bool? IsBuyOrder { get; set; }

        /// <summary>
        /// Date and time when this order was issued
        /// </summary>
        /// <value>Date and time when this order was issued</value>
        [DataMember(Name = "issued", EmitDefaultValue = false)]
        public DateTime? Issued { get; set; }

        /// <summary>
        /// The character who issued this order
        /// </summary>
        /// <value>The character who issued this order</value>
        [DataMember(Name = "issued_by", EmitDefaultValue = false)]
        public int? IssuedBy { get; set; }

        /// <summary>
        /// ID of the location where order was placed
        /// </summary>
        /// <value>ID of the location where order was placed</value>
        [DataMember(Name = "location_id", EmitDefaultValue = false)]
        public long? LocationId { get; set; }

        /// <summary>
        /// For buy orders, the minimum quantity that will be accepted in a matching sell order
        /// </summary>
        /// <value>For buy orders, the minimum quantity that will be accepted in a matching sell order</value>
        [DataMember(Name = "min_volume", EmitDefaultValue = false)]
        public int? MinVolume { get; set; }

        /// <summary>
        /// Unique order ID
        /// </summary>
        /// <value>Unique order ID</value>
        [DataMember(Name = "order_id", EmitDefaultValue = false)]
        public long? OrderId { get; set; }

        /// <summary>
        /// Cost per unit for this order
        /// </summary>
        /// <value>Cost per unit for this order</value>
        [DataMember(Name = "price", EmitDefaultValue = false)]
        public double? Price { get; set; }

        /// <summary>
        /// ID of the region where order was placed
        /// </summary>
        /// <value>ID of the region where order was placed</value>
        [DataMember(Name = "region_id", EmitDefaultValue = false)]
        public int? RegionId { get; set; }

        /// <summary>
        /// The type ID of the item transacted in this order
        /// </summary>
        /// <value>The type ID of the item transacted in this order</value>
        [DataMember(Name = "type_id", EmitDefaultValue = false)]
        public int? TypeId { get; set; }

        /// <summary>
        /// Quantity of items still required or offered
        /// </summary>
        /// <value>Quantity of items still required or offered</value>
        [DataMember(Name = "volume_remain", EmitDefaultValue = false)]
        public int? VolumeRemain { get; set; }

        /// <summary>
        /// Quantity of items required or offered at time order was placed
        /// </summary>
        /// <value>Quantity of items required or offered at time order was placed</value>
        [DataMember(Name = "volume_total", EmitDefaultValue = false)]
        public int? VolumeTotal { get; set; }

        /// <summary>
        /// The corporation wallet division used for this order.
        /// </summary>
        /// <value>The corporation wallet division used for this order.</value>
        [DataMember(Name = "wallet_division", EmitDefaultValue = false)]
        public int? WalletDivision { get; set; }
    }
}