using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [Table("corporation_wallet_journal")]
    [EsiEndpoint("/v3/corporations/{corporation_id}/wallets/{division}/journal/", true,
        new[] {EsiCorporationScopes.WalletsRead})]
    public class
        CorporationWalletJournalModel :
            IEsiResponseModel
    {
        [RestParameterMapping("corporation_id")]
        [Key]
        [Column(Order = 0)]
        [Index("UNIQUE", IsUnique = true, Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CorporationId { get; set; }

        [RestParameterMapping("division")]
        [Key]
        [Column(Order = 1)]
        [Index("UNIQUE", IsUnique = true, Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Division { get; set; }

        /// <summary>
        ///     Unique journal reference ID
        /// </summary>
        /// <value>Unique journal reference ID</value>
        [Key]
        [Column(Order = 2)]
        [Index("UNIQUE", IsUnique = true, Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long? Id { get; set; }


        public DateTime?          Date          { get; set; }
        public double?            Amount        { get; set; }
        public double?            Balance       { get; set; }
        public int?               FirstPartyId  { get; set; }
        public int?               SecondPartyId { get; set; }
        public string             Description   { get; set; }
        public string             Reason        { get; set; }
        public long?              ContextId     { get; set; }
        public ContextIdTypeEnum? ContextIdType { get; set; }
        public RefTypeEnum?       RefType       { get; set; }
        public double?            Tax           { get; set; }
        public int?               TaxReceiverId { get; set; }
    }
}
