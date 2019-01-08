using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [EsiEndpoint("/v2/corporations/{corporation_id}/assets/locations/", false, new[] {EsiCorporationScopes.AssetsRead})]
    [DataContract]
    public class CorporationAssetsLocationsModel : IEsiResponseModel
    {
        /// <summary>
        /// item_id integer
        /// </summary>
        /// <value>item_id integer</value>
        [DataMember(Name = "item_id", EmitDefaultValue = false)]
        public long? ItemId { get; set; }

        /// <summary>
        /// Gets or Sets Position
        /// </summary>
        [DataMember(Name = "position", EmitDefaultValue = false)]
        public AssetsLocationsPosition Position { get; set; }
    }
}