using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurCommon.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [DataContract]
    public partial class CharacterMarketOrdersModel : IEsiResponseModel
    {
        /// <summary>
        /// Valid order range, numbers are ranges in jumps
        /// </summary>
        /// <value>Valid order range, numbers are ranges in jumps</value>
        [DataMember(Name = "range")]
        public RangeEnum Range { get; set; }

        /// <summary>
        /// Number of days for which order is valid (starting from the issued date). An order expires at time issued + duration
        /// </summary>
        /// <value>Number of days for which order is valid (starting from the issued date). An order expires at time issued + duration</value>
        [DataMember(Name = "duration")]
        public int? Duration { get; set; }

        /// <summary>
        /// For buy orders, the amount of ISK in escrow
        /// </summary>
        /// <value>For buy orders, the amount of ISK in escrow</value>
        [DataMember(Name = "escrow")]
        public double? Escrow { get; set; }

        /// <summary>
        /// True if the order is a bid (buy) order
        /// </summary>
        /// <value>True if the order is a bid (buy) order</value>
        [DataMember(Name = "is_buy_order")]
        public bool? IsBuyOrder { get; set; }

        /// <summary>
        /// Signifies whether the buy/sell order was placed on behalf of a corporation.
        /// </summary>
        /// <value>Signifies whether the buy/sell order was placed on behalf of a corporation.</value>
        [DataMember(Name = "is_corporation")]
        public bool? IsCorporation { get; set; }

        /// <summary>
        /// Date and time when this order was issued
        /// </summary>
        /// <value>Date and time when this order was issued</value>
        [DataMember(Name = "issued")]
        public DateTime? Issued { get; set; }

        /// <summary>
        /// ID of the location where order was placed
        /// </summary>
        /// <value>ID of the location where order was placed</value>
        [DataMember(Name = "location_id")]
        public long? LocationId { get; set; }

        /// <summary>
        /// For buy orders, the minimum quantity that will be accepted in a matching sell order
        /// </summary>
        /// <value>For buy orders, the minimum quantity that will be accepted in a matching sell order</value>
        [DataMember(Name = "min_volume")]
        public int? MinVolume { get; set; }

        /// <summary>
        /// Unique order ID
        /// </summary>
        /// <value>Unique order ID</value>
        [DataMember(Name = "order_id")]
        public long? OrderId { get; set; }

        /// <summary>
        /// Cost per unit for this order
        /// </summary>
        /// <value>Cost per unit for this order</value>
        [DataMember(Name = "price")]
        public double? Price { get; set; }


        /// <summary>
        /// ID of the region where order was placed
        /// </summary>
        /// <value>ID of the region where order was placed</value>
        [DataMember(Name = "region_id")]
        public int? RegionId { get; set; }

        /// <summary>
        /// The type ID of the item transacted in this order
        /// </summary>
        /// <value>The type ID of the item transacted in this order</value>
        [DataMember(Name = "type_id")]
        public int? TypeId { get; set; }

        /// <summary>
        /// Quantity of items still required or offered
        /// </summary>
        /// <value>Quantity of items still required or offered</value>
        [DataMember(Name = "volume_remain")]
        public int? VolumeRemain { get; set; }

        /// <summary>
        /// Quantity of items required or offered at time order was placed
        /// </summary>
        /// <value>Quantity of items required or offered at time order was placed</value>
        [DataMember(Name = "volume_total")]
        public int? VolumeTotal { get; set; }
    }
}