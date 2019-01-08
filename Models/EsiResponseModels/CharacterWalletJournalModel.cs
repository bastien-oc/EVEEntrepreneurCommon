using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [Table("character_wallet_journal")]
    [EsiEndpoint("/v4/characters/{character_id}/wallet/journal/", true, ScopesRequired =
        new[] {"esi-wallet.read_character_wallet.v1"})]
    public class CharacterWalletJournalModel : IEsiResponseModel
    {
        [Key]
        [Column(Order = 0)]
        [Index("UNIQUE", IsUnique = true, Order = 0)]
        [RestParameterMapping("character_id")]
        public int CharacterId { get; set; }

        [Column(Order = 1)]
        [Key]
        [Index("UNIQUE", IsUnique = true, Order = 1)]
        public long Id { get; set; }

        public double            Amount        { get; set; }
        public double            Balance       { get; set; }
        public long              ContextId     { get; set; }
        public ContextIdTypeEnum ContextIdType { get; set; }
        public DateTime          Date          { get; set; }
        public string            Description   { get; set; }
        public int               FirstPartyId  { get; set; }
        public string            Reason        { get; set; }
        public RefTypeEnum       RefType       { get; set; }
        public int               SecondPartyId { get; set; }
        public double            Tax           { get; set; }
        public int               TaxReceiverId { get; set; }
    }
}
