using System;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [EsiEndpoint("/v1/industry/facilities/")]
    public class IndustryFacilitiesModel : IEsiAnnotatedRecord
    {
        [JsonProperty("facility_id")]
        public long FacilityId { get; set; }

        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }

        [JsonProperty("region_id")]
        public int RegionId { get; set; }

        [JsonProperty("solar_system_id")]
        public int SolarSystemId { get; set; }

        [JsonProperty("tax")]
        public float Tax { get; set; }

        [JsonProperty("type_id")]
        public int TypeId { get; set; }
    }
}