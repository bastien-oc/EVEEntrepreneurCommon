using System;
using EntrepreneurCommon.Common;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [EsiEndpoint("/v4/corporations/{corporation_id}/")]
    public class CorporationPublicInformation
    {
        public static string Route { get => "/v4/corporations/{corporation_id}/"; }

        public Int32 AllianceId { get; set; }
        public Int32 CeoId { get; set; }
        public Int32 CreatorId { get; set; }
        public DateTime DateFounded { get; set; }
        public String Description { get; set; }
        public Int32 FactionId { get; set; }
        public Int32 HomeStationId { get; set; }
        public Int32 MemberCount { get; set; }
        public String Name { get; set; }
        public Int64 Shares { get; set; }
        public Single TaxRate { get; set; }
        public String Ticker { get; set; }
        public String Url { get; set; }
    }
}