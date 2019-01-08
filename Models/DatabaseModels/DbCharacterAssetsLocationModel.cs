using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Models.EsiResponseModels;
using C = System.ComponentModel.DataAnnotations.Schema.ColumnAttribute;

namespace EntrepreneurCommon.Models.DatabaseModels
{
    [Table("character_assets_location")]
    public class DbCharacterAssetsLocationModel
    {
        public DbCharacterAssetsLocationModel()
        {
        }

        public DbCharacterAssetsLocationModel(CharacterAssetsLocationsModel responseObject)
        {
            ItemId = responseObject.ItemId;
            X = responseObject.Position.X;
            Y = responseObject.Position.Y;
            Z = responseObject.Position.Z;
        }

        [Key] [C("item_id")] public long ItemId { get; set; }

        [C("x")] public double? X { get; set; }
        [C("y")] public double? Y { get; set; }
        [C("z")] public double? Z { get; set; }
    }
}