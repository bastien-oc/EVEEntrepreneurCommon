using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntrepreneurCommon.Models.DatabaseModels
{
    [Table("market_price_custom")]
    public class DbMarketPriceCustom
    {
        [Column("type_id")] [Key] [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [Column("price_value")]     public double PriceValue { get; set; }
        [Column("price_method")]    public string PriceMethod { get; set; }
        [Column("price_arg1")]      public string PriceArg1 { get; set; }
        [Column("price_arg2")]      public string PriceArg2 { get; set; }
    }
}
