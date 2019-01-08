using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    /// <summary>
    ///     200 ok object
    /// </summary>
    [Table("corporation_market_orders_history")]
    [EsiEndpoint("/v2/corporations/{corporation_id}/orders/history/", true)]
    public class CorporationMarketOrdersHistory : IEsiResponseModel
    {

        [Key]
        [Column(Order = 0)]
        [Index("UNIQUE", 0, IsUnique = true)]
        [RestParameterMapping()]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CorporationId { get; set; }

        /// <summary>
        ///     Unique order ID
        /// </summary>
        /// <value>Unique order ID</value>
        [Column(Order = 1)]
        [Index("UNIQUE", 1, IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public long? OrderId { get; set; }

        /// <summary>
        ///     Valid order range, numbers are ranges in jumps
        /// </summary>
        /// <value>Valid order range, numbers are ranges in jumps</value>
        public RangeEnum Range { get; set; }

        /// <summary>
        ///     Current order state
        /// </summary>
        /// <value>Current order state</value>
        public StateEnum State { get; set; }

        /// <summary>
        ///     Number of days the order was valid for (starting from the issued date). An order expires at time issued + duration
        /// </summary>
        /// <value>
        ///     Number of days the order was valid for (starting from the issued date). An order expires at time issued +
        ///     duration
        /// </value>
        public int? Duration { get; set; }

        /// <summary>
        ///     For buy orders, the amount of ISK in escrow
        /// </summary>
        /// <value>For buy orders, the amount of ISK in escrow</value>
        public double? Escrow { get; set; }

        /// <summary>
        ///     True if the order is a bid (buy) order
        /// </summary>
        /// <value>True if the order is a bid (buy) order</value>
        public bool? IsBuyOrder { get; set; }

        /// <summary>
        ///     Date and time when this order was issued
        /// </summary>
        /// <value>Date and time when this order was issued</value>
        public DateTime? Issued { get; set; }

        /// <summary>
        ///     The character who issued this order
        /// </summary>
        /// <value>The character who issued this order</value>
        public int? IssuedBy { get; set; }

        /// <summary>
        ///     ID of the location where order was placed
        /// </summary>
        /// <value>ID of the location where order was placed</value>
        public long? LocationId { get; set; }

        /// <summary>
        ///     For buy orders, the minimum quantity that will be accepted in a matching sell order
        /// </summary>
        /// <value>For buy orders, the minimum quantity that will be accepted in a matching sell order</value>
        public int? MinVolume { get; set; }

        /// <summary>
        ///     Cost per unit for this order
        /// </summary>
        /// <value>Cost per unit for this order</value>
        public double? Price { get; set; }

        /// <summary>
        ///     ID of the region where order was placed
        /// </summary>
        /// <value>ID of the region where order was placed</value>
        public int? RegionId { get; set; }

        /// <summary>
        ///     The type ID of the item transacted in this order
        /// </summary>
        /// <value>The type ID of the item transacted in this order</value>
        public int? TypeId { get; set; }

        /// <summary>
        ///     Quantity of items still required or offered
        /// </summary>
        /// <value>Quantity of items still required or offered</value>
        public int? VolumeRemain { get; set; }

        /// <summary>
        ///     Quantity of items required or offered at time order was placed
        /// </summary>
        /// <value>Quantity of items required or offered at time order was placed</value>
        public int? VolumeTotal { get; set; }

        /// <summary>
        ///     The corporation wallet division used for this order
        /// </summary>
        /// <value>The corporation wallet division used for this order</value>
        public int? WalletDivision { get; set; }
    }
}
