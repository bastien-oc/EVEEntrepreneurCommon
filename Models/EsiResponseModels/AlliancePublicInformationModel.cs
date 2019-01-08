using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [EsiEndpoint("/v3/alliances/{alliance_id}/", false, null)]
    public class AlliancePublicInformationModel : IEsiResponseModel
    {
        [RestParameterMapping]
        [JsonProperty("alliance_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AllianceId { get; set; }

        public int            CreatorCorporationId  { get; set; }
        public int            CreatorId             { get; set; }
        public DateTime DateFounded           { get; set; }
        public int            ExecutorCorporationId { get; set; }
        public int            FactionId             { get; set; }
        public string         Name                  { get; set; }
        public string         Ticker                { get; set; }
    }
}
