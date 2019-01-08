using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [Table("character_blueprints")]
    [DataContract]
    [EsiEndpoint("/v2/characters/{character_id}/blueprints/", true, new[] {EsiCharacterScopes.BlueprintsRead})]
    public class CharacterBlueprintsModel : IEsiResponseModel
    {
        [Key]
        [Column(Order             = 0)]
        [Index("UNIQUE", IsUnique = true, Order = 0)]
        [RestParameterMapping("character_id")]
        public int CharacterId { get; set; }

        /// <summary>
        ///     Unique ID for this item.
        /// </summary>
        [Key]
        [Column(Order             = 1)]
        [Index("UNIQUE", IsUnique = true, Order                 = 1)]
        [DataMember(Name          = "item_id", EmitDefaultValue = false)]
        public long? ItemId { get; set; }

        /// <summary>
        ///     Type of the location_id
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public LocationFlagEnum LocationFlag { get; set; }

        /// <summary>
        ///     References a solar system, station or item_id if this blueprint is located within a container. If the return value
        ///     is an item_id, then the Character AssetList API must be queried to find the container using the given item_id to
        ///     determine the correct location of the Blueprint.
        /// </summary>
        public long? LocationId { get; set; }

        /// <summary>
        ///     Material Efficiency Level of the blueprint.
        /// </summary>
        public int? MaterialEfficiency { get; set; }

        /// <summary>
        ///     A range of numbers with a minimum of -2 and no maximum value where -1 is an original and -2 is a copy. It can be a
        ///     positive integer if it is a stack of blueprint originals fresh from the market (e.g. no activities performed on
        ///     them yet).
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        ///     Number of runs remaining if the blueprint is a copy, -1 if it is an original.
        /// </summary>
        public int? Runs { get; set; }

        /// <summary>
        ///     Time Efficiency Level of the blueprint.
        /// </summary>
        public int? TimeEfficiency { get; set; }

        public int? TypeId { get; set; }
    }
}