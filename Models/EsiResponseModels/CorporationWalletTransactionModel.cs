using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [Table("corporation_wallet_transactions")]
    [EsiEndpoint("/v1/corporations/{corporation_id}/wallets/{division}/transactions/", true,
        new[] {EsiCorporationScopes.WalletsRead})]
    public class CorporationWalletTransactionModel : IEsiResponseModel
    {
        [Key]
        [Column(Order = 0)]
        [RestParameterMapping]
        [Index("UNIQUE", IsUnique = true, Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CorporationId { get; set; }

        [Key]
        [Column(Order = 1)]
        [RestParameterMapping]
        [Index("UNIQUE", IsUnique = true, Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Division { get; set; }

        [Key]
        [Column(Order = 2)]
        [RestParameterMapping]
        [Index("UNIQUE", IsUnique = true, Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TransactionId { get; set; }

        public DateTime Date         { get; set; }
        public int      ClientId     { get; set; }
        public bool     IsBuy        { get; set; }
        public int      TypeId       { get; set; }
        public double   UnitPrice    { get; set; }
        public int      Quantity     { get; set; }
        public long     JournalRefId { get; set; }
        public long     LocationId   { get; set; }
    }
}
