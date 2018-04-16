using System;
using System.Linq;
using Newtonsoft.Json;
using Nito.AsyncEx;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace EntrepreneurEsiApi.Models.Esi
{
    /// <summary>
    /// Paginated record of all mining done by a character for the past 30 days
    /// Endpoint: /characters/{character_id}/mining/
    /// </summary>
    [Table("character_mining")]
    public partial class IndustryMiningLedgerResponse
    {
        [JsonIgnore] public static readonly string Endpoint = "/v1/characters/{character_id}/mining/";

        /// <summary>
        /// Identifies the owner of the entry when stored alongside other data. App and Database use only, not part of API.
        /// </summary>
        [JsonIgnore]
        [Column("character_id")] [Key]
        public long CharacterID { get; set; }

        /// <summary>
        /// date string
        /// </summary>
        [J("date")]
        [Column("date")]
        public string Date { get; set; }

        /// <summary>
        /// solar_system_id integer
        /// </summary>
        [J("solar_system_id")]
        [Column("solar_system_id")]
        public long SolarSystemID { get; set; }

        /// <summary>
        /// type_id integer
        /// </summary>
        [J("type_id")]
        [Column("type_id")]
        public long TypeID { get; set; }

        /// <summary>
        /// quantity integer
        /// </summary>
        [J("quantity")]
        [Column("quantity")]
        public long Quantity { get; set; }

        /// <summary>
        /// App use only. Returns the name of the material mined.
        /// </summary>
        [J("type_name")] public String TypeName { get; set; }
        /// <summary>
        /// App use only. Returns the name of the solar system.
        /// </summary>
        public String SolarSystemName { get; set; }
    }


}