using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Models.EsiResponseModels;

namespace EntrepreneurCommon.Models.DatabaseModels
{
    public class CorporationWalletJournalDbModel
    {
        [Key] [Column(Order = 0)] public int  CorporationId { get; set; }
        [Key] [Column(Order = 1)] public int  Division      { get; set; }
        [Key] [Column(Order = 2)] public long Id            { get; set; }

        public double?            Balance       { get; set; }
        public double?            Amount        { get; set; }
        public DateTime?    Date          { get; set; }
        public int?               FirstPartyId  { get; set; }
        public int?               SecondPartyId { get; set; }
        public string             Description   { get; set; }
        public string             Reason        { get; set; }
        public ContextIdTypeEnum? ContextIdType { get; set; }
        public long?              ContextId     { get; set; }
        public RefTypeEnum        RefType       { get; set; }
        public int?               TaxReceiverId { get; set; }
        public double?            Tax           { get; set; }
    }
}
