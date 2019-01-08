using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntrepreneurCommon.Models.DatabaseModels
{
    public class CorporationWalletTransactionDbModel
    {
        [Key] [Column(Order = 0)] public int  CharacterId   { get; set; }
        [Key] [Column(Order = 1)] public long TransactionId { get; set; }
        [Key] [Column(Order = 2)] public int  Division      { get; set; }

        public int             ClientId     { get; set; }
        public DateTime? Date         { get; set; }
        public bool            IsBuy        { get; set; }
        public bool            IsPersonal   { get; set; }
        public long            JournalRefId { get; set; }
        public long            LocationId   { get; set; }
        public int             Quantity     { get; set; }
        public int             TypeId       { get; set; }
        public double          UnitPrice    { get; set; }
    }
}
