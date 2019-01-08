using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels.Deprecated
{
    [Table("corp_wallet_transactions_esi")]
    [Obsolete("The implementation is obsolete. Use WalletCorporationTransaction instead.", true)]
    public class WalletTransactionCorpObsolete
    {
        [NotMapped] public static string EndpointVersioned { get => "/v1/corporations/{corporation_id}/wallets/{division}/transactions/"; }
        [NotMapped] public static string Scope { get => "esi-wallet.read_corporation_wallets.v1"; }

        // KEYS

        [JsonIgnore,
            Key,
            Column("owner_id", Order = 0),
            Index("UNIQUE", 0, IsUnique = true)]
        public int OwnerId { get; set; }

        [J("transaction_id"),
            Key,
            Column("transaction_id", Order = 1),
            Index("UNIQUE", 1, IsUnique = true)]
        public long TransactionId { get; set; }

        [JsonIgnore,
            Column("division")]
        public int Division { get; set; }

        #region EXTRAS

        [JsonIgnore,
            Column("owner_name")]
        public string OwnerName { get; set; }

        [JsonIgnore,
            Column("client_name")]
        public string ClientName { get; set; }

        #endregion

        // PROPERTIES

        [J("date"),
            Column("date")]
        public DateTime Date { get; set; }

        [J("type_id"),
            Column("type_id")]
        public int TypeId { get; set; }

        [J("location_id"),
            Column("location_id")]
        public long LocationId { get; set; }

        [J("unit_price"),
            Column("unit_price")]
        public double UnitPrice { get; set; }

        [J("quantity"),
            Column("quantity")]
        public int Quantity { get; set; }

        [J("client_id"),
            Column("client_id")]
        public int ClientId { get; set; }

        [J("is_buy"), Column("is_buy")]
        public bool IsBuy { get; set; }

        [J("is_personal"), Column("is_personal")]
        public bool IsPersonal { get; set; }

        [J("journal_ref_id"), Column("journal_ref_id")]
        public long JournalRefId { get; set; }

    }
}
