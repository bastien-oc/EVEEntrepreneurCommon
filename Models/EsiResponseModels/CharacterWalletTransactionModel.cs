using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using Newtonsoft.Json;
// using J = Newtonsoft.Json.JsonPropertyAttribute;
// using D = System.Runtime.Serialization.DataMemberAttribute;
// using K = System.ComponentModel.DataAnnotations.KeyAttribute;
// using C = System.ComponentModel.DataAnnotations.Schema.ColumnAttribute;
// using I = System.ComponentModel.DataAnnotations.Schema.IndexAttribute;
// using R = EntrepreneurCommon.Common.Attributes.RestParameterMappingAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [Table("character_wallet_transactions")]
    [EsiEndpoint("/v1/characters/{character_id}/wallet/transactions/", true,
        new[] {EsiCharacterScopes.WalletRead})]
    public class CharacterWalletTransactionModel : IEsiResponseModel
    {
        [JsonIgnore] [NotMapped] public static string Scope => "esi-wallet.read_character_wallet.v1";

        [Key]
        [Column(Order = 0)]
        [Index("UNIQUE", IsUnique = true, Order = 0)]
        [RestParameterMapping("character_id")]
        public int CharacterId { get; set; }

        [Key]
        [Column(Order = 1)]
        [Index("UNIQUE", IsUnique = true, Order = 1)]
        public long TransactionId { get; set; }

        // PROPERTIES
        public DateTime Date         { get; set; }
        public int      TypeId       { get; set; }
        public long     LocationId   { get; set; }
        public double   UnitPrice    { get; set; }
        public int      Quantity     { get; set; }
        public int      ClientId     { get; set; }
        public bool     IsBuy        { get; set; }
        public bool     IsPersonal   { get; set; }
        public long     JournalRefId { get; set; }
    }
}
