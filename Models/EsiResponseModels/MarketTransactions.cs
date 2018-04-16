using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [Table("corp_wallet_transactions_esi")]
    public class MarketTransactionCorp
    {
        [NotMapped] public static String EndpointVersioned { get => "/v1/corporations/{corporation_id}/wallets/{division}/transactions/"; }
        [NotMapped] public static String Scope { get => "esi-wallet.read_corporation_wallets.v1"; }

        // KEYS

        [JsonIgnore,
            Key,
            Column("owner_id", Order = 0),
            Index("UNIQUE", 0, IsUnique = true)]
        public Int32 OwnerId { get; set; }

        [J("transaction_id"),
            Key,
            Column("transaction_id", Order = 1),
            Index("UNIQUE", 1, IsUnique = true)]
        public Int64 TransactionId { get; set; }

        [JsonIgnore,
            Column("division")]
        public Int32 Division { get; set; }

        #region EXTRAS

        [JsonIgnore,
            Column("owner_name")]
        public String OwnerName { get; set; }

        [JsonIgnore,
            Column("client_name")]
        public String ClientName { get; set; }

        #endregion

        // PROPERTIES

        [J("date"),
            Column("date")]
        public DateTime Date { get; set; }

        [J("type_id"),
            Column("type_id")]
        public Int32 TypeId { get; set; }

        [J("location_id"),
            Column("location_id")]
        public Int64 LocationId { get; set; }

        [J("unit_price"),
            Column("unit_price")]
        public double UnitPrice { get; set; }

        [J("quantity"),
            Column("quantity")]
        public Int32 Quantity { get; set; }

        [J("client_id"),
            Column("client_id")]
        public Int32 ClientId { get; set; }

        [J("is_buy"), Column("is_buy")]
        public Boolean IsBuy { get; set; }

        [J("is_personal"), Column("is_personal")]
        public Boolean IsPersonal { get; set; }

        [J("journal_ref_id"), Column("journal_ref_id")]
        public Int64 JournalRefId { get; set; }

    }

    [Table("char_wallet_transactions_esi")]
    public class MarketTransactionChar
    {
        [JsonIgnore, NotMapped] public static String EndpointVersioned { get => "/v1/characters/{character_id}/wallet/transactions/"; }
        [JsonIgnore, NotMapped] public static String Scope { get => "esi-wallet.read_character_wallet.v1"; }

        [JsonIgnore,
            Key,
            Column("owner_id", Order = 0),
            Index("UNIQUE", IsUnique = true, Order = 0)]
        public Int32 OwnerId { get; set; }

        [J("transaction_id"),
            Key,
            Column("transaction_id", Order = 1),
            Index("UNIQUE", IsUnique = true, Order = 1)]
        public Int64 TransactionId { get; set; }

        // EXTRA

        [JsonIgnore,
            Column("owner_name")]
        public String OwnerName { get; set; }
        [JsonIgnore,
            Column("client_name")]
        public String ClientName { get; set; }

        // PROPERTIES

        [J("date"),
            Column("date")]
        public DateTime Date { get; set; }

        [J("type_id"),
            Column("type_id")]
        public Int32 TypeId { get; set; }

        [J("location_id"),
            Column("location_id")]
        public Int64 LocationId { get; set; }

        [J("unit_price"),
            Column("unit_price")]
        public double UnitPrice { get; set; }

        [J("quantity"),
            Column("quantity")]
        public Int32 Quantity { get; set; }

        [J("client_id"),
            Column("client_id")]
        public Int32 ClientId { get; set; }

        [J("is_buy"),
            Column("is_buy")]
        public Boolean IsBuy { get; set; }

        [J("is_personal"),
            Column("is_personal")]
        public Boolean IsPersonal { get; set; }

        [J("journal_ref_id"),
            Column("journal_ref_id")]
        public Int64 JournalRefId { get; set; }
    }
}
