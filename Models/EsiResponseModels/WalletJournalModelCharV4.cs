using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Common;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Models.Esi {
    [Table("char_wallet_journal")]
    [EsiEndpoint("/v4/characters/{character_id}/wallet/journal/", true, ScopesRequired = new []{"esi-wallet.read_character_wallet.v1"} )]
    public class WalletJournalModelCharV4 : IEsiAnnotatedRecord, IEsiEndpoint
    {
        [JsonIgnore, NotMapped] public static readonly String Endpoint = "/v4/characters/{character_id}/wallet/journal/";
        [Column("wallet_owner_id", Order = 0), Key, Index("UNIQUE", IsUnique = true, Order = 0), EsiRecordAnnotation("character_id")]
        public int WalletOwnerID { get; set; }

        [JsonIgnore, Column("wallet_owner_name")]
        public string WalletOwnerName { get; set; }

        [JsonProperty("amount"), Column("amount")]
        public double Amount { get; set; }

        public double Balance { get; set; }
        public Int64 ContextId { get; set; }
        public string ContextIdType { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Int32 FirstPartyId { get; set; }

        [Column(Order = 1), Key, Index("UNIQUE", IsUnique = true, Order = 1)]
        public Int64 Id { get; set; }
        public string Reason { get; set; }
        public string RefType { get; set; }
        public Int32 SecondPartyId { get; set; }
        public double Tax { get; set; }
        public Int32 TaxReceiverId { get; set; }

    }
}