using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    /// <summary>
    ///     Paginated record of all mining done by a character for the past 30 days
    ///     Endpoint: /characters/{character_id}/mining/
    /// </summary>
    [Table("character_mining_ledger")]
    [EsiEndpoint("/v1/characters/{character_id}/mining/", true, new[] {EsiCharacterScopes.IndustryMiningRead})]
    public class CharacterMiningLedgerModel : IEsiResponseModel
    {
        /// <summary>
        ///     Identifies the owner of the entry when stored alongside other data. App and Database use only, not part of API.
        /// </summary>
        [JsonIgnore]
        [Column("character_id", Order = 0)]
        [Key]
        [RestParameterMapping("character_id")]
        public int CharacterId { get; set; }

        /// <summary>
        ///     date string
        /// </summary>
        [Column(Order = 1)]
        [Key]
        public DateTime Date { get; set; }

        /// <summary>
        ///     solar_system_id integer
        /// </summary>
        [Column(Order = 2)]
        [Key]
        public int SolarSystemID { get; set; }

        /// <summary>
        ///     type_id integer
        /// </summary>
        [Column(Order = 3)]
        [Key]
        public int TypeId { get; set; }

        /// <summary>
        ///     quantity integer
        /// </summary>
        [Column]
        public long Quantity { get; set; }
    }
}
