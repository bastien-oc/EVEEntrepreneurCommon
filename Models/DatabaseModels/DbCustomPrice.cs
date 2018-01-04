using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
