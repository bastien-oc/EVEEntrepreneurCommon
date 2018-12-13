using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [Table("character_public_information")]
    [EsiEndpoint("/v4/characters/{character_id}/", false, null)]
    public class CharacterPublicInformation : IEsiResponseModel
    {
        [Key]
        [Index("UNIQUE", IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RestParameterMapping("character_id")]
        public int CharacterId { get; set; }

        public int      AllianceId     { get; set; }
        public int      AncestryId     { get; set; }
        public DateTime Birthday       { get; set; }
        public int      BloodlineId    { get; set; }
        public int      CorporationId  { get; set; }
        public string   Description    { get; set; }
        public string   Gender         { get; set; }
        public string   Name           { get; set; }
        public int      RaceId         { get; set; }
        public double   SecurityStatus { get; set; }
    }
}
