using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [Table("character_assets")]
    [EsiEndpoint("/v3/characters/{character_id}/assets/", true, new[] {EsiCharacterScopes.AssetsRead})]
    public partial class CharacterAssetsModel : IEsiResponseModel
    {
        [Index]
        [RestParameterMapping("character_id")]
        public int CharacterId { get; set; }

        [Key]
        [Index]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long? ItemId { get; set; }

        public LocationFlagEnum LocationFlag    { get; set; }
        public LocationTypeEnum LocationType    { get; set; }
        public bool?            IsBlueprintCopy { get; set; }
        public bool?            IsSingleton     { get; set; }
        public long?            LocationId      { get; set; }
        public int?             Quantity        { get; set; }
        public int?             TypeId          { get; set; }
    }
}