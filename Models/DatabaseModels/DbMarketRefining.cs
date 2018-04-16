using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntrepreneurCommon.Models.DatabaseModels
{
    [Table("market_refining")]
    public class DbMarketRefining
    {
        [Column("type_id")] [Key] [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [Column("yield")]
        public float? Yield { get; set; }
        [Column("price_esi")]
        public double? PriceEsi { get; set; }
        [Column("price_sell")]
        public double? PriceSell { get; set; }
        [Column("price_buy")]
        public double? PriceBuy { get; set; }
        [Column("price_custom")]
        public double? PriceCustom { get; set; }
        [Column("price_historical")]
        public double? PriceHistorical { get; set; }

        [Column("calculated_date")]
        public DateTime? CalculatedDate { get; set; }
    }
}
