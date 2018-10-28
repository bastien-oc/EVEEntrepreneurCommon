using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntrepreneurCommon.Models.DatabaseModels
{
    [Table("market_customprice")]
    public class DbCustomPrice
    {
        [Column("type_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 TypeId { get; set; }

        [Column("price")]
        public double Price { get; set; }
    }
}
