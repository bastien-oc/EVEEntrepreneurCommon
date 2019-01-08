using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Models.EsiResponseModels.Deprecated {
    /// <summary>
    /// Retrieve character or corporation wallet journal
    /// /characters/{character_id}/wallet/journal/
    /// /corporations/{corporation_id}/wallets/{division}/journal/
    /// Path: (character_id) OR (corporation_id AND division)
    /// </summary>
    [Table("char_wallet_journal")]
    [Obsolete("Deprecated endpoint", true)]
    public class WalletJournalModelCharV3
    {
        [JsonIgnore] public static string Endpoint = "/v3/characters/{character_id}/wallet/journal/";

        /// <summary>
        /// Internal Database Use. Represents the Character or Corporation to whom the entry belongs.
        /// </summary>
        [JsonIgnore]
        [Column("wallet_owner_id", Order = 0), Key, Index("UNIQUE", IsUnique = true, Order = 0)]
        public int WalletOwnerID { get; set; }

        /// <summary>
        /// Internal Database use. Represents the name of the entity to whom the entry belongs.
        /// </summary>
        [JsonIgnore]
        [Column("wallet_owner_name")]
        public string WalletOwnerName { get; set; }

        /// <summary>
        /// Internal Database use. If corporate wallet, represents the ID of the wallet to which the entry belongs.
        /// </summary>
        [JsonIgnore]
        [Column("division")]
        public int WalletDivision { get; set; }

        /// <summary>
        /// Date and time of transaction
        /// </summary>
        [JsonProperty("date")]
        [Column("date")]
        //[Index("INDEX",IsUnique = false)]
        public string Date { get; set; }

        /// <summary>
        /// Unique journal reference ID
        /// </summary>
        [JsonProperty("ref_id")]
        [Column("ref_id", Order = 1), Key, Index("UNIQUE", IsUnique = true, Order = 1)]
        public long RefID { get; set; }

        /// <summary>
        /// Transaction type, different type of transaction will populate different fields in extra_info Note: If you have an existing XML API application that is using ref_types, you will need to know which string ESI ref_type maps to which integer. You can use the following gist to see string->int mappings: https://gist.github.com/ccp-zoetrope/c03db66d90c2148724c06171bc52e0ec
        /// </summary>
        [JsonProperty("ref_type")]
        [Column("ref_type")]
        public string RefType { get; set; }

        /// <summary>
        /// first_party_id integer
        /// </summary>
        [JsonProperty("first_party_id")]
        [Column("first_party_id")]
        public int FirstPartyID { get; set; }

        /// <summary>
        /// first_party_type string = ['character', 'corporation', 'alliance', 'faction', 'system']
        /// </summary>
        [JsonProperty("first_party_type")]
        [Column("first_party_type")]
        public string FirstPartyType { get; set; }

        /// <summary>
        /// Name of the first party, obtained through app for ease of access. Not obtained through ESI, but keep in case of Json Exports
        /// </summary>
        [JsonProperty("first_party_name")]
        [Column("first_party_name")]
        public string FirstPartyName { get; set; }

        /// <summary>
        /// second_party_id integer
        /// </summary>
        [JsonProperty("second_party_id")]
        [Column("second_party_id")]
        public int SecondPartyID { get; set; }

        /// <summary>
        /// second_party_type string = ['character', 'corporation', 'alliance', 'faction', 'system']
        /// </summary>
        [JsonProperty("second_party_type")]
        [Column("second_party_type")]
        public string SecondPartyType { get; set; }

        /// <summary>
        /// Name of the second party, obtained through app for ease of access. Not obtained through ESI, but keep in case of Json exports.
        /// </summary>
        [JsonProperty("second_party_name")]
        [Column("second_party_name")]
        public string SecondPartyName { get; set; }

        /// <summary>
        /// Transaction amount. Positive when value transferred to the first party. Negative otherwise
        /// </summary>
        [JsonProperty("amount")]
        [Column("amount")]
        public double Amount { get; set; }

        /// <summary>
        /// Wallet balance after transaction occurred
        /// </summary>
        [JsonProperty("balance")]
        [Column("balance")]
        public double Balance { get; set; }

        /// <summary>
        /// reason string
        /// </summary>
        [JsonProperty("reason")]
        [Column("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// the corporation ID receiving any tax paid
        /// </summary>
        [JsonProperty("tax_receiver_id")]
        [Column("tax_receiver_id")]
        public long TaxReceiverID { get; set; }

        /// <summary>
        /// Tax amount received for tax related transactions
        /// </summary>
        [JsonProperty("tax")]
        [Column("tax_amount")]
        public double Tax { get; set; }

        /// <summary>
        /// Extra information for different type of transaction. Stored as TEXT in Database, but as CWJExtraInfo DataModel when obtained from ESI
        /// </summary>
        [JsonProperty("extra_info"), NotMapped]
        public CWJExtraInfo ExtraInfo { get; set; }

        /// <summary>
        /// Database Use only, translated from ExtraInfo. Stores the sub-class as a json string.
        /// </summary>
        [JsonIgnore]
        [Column("extra_info")]
        public string ExtraInfo_String { get { return JsonConvert.SerializeObject(ExtraInfo); } }

        /// <summary>
        /// OBSOLETE. Ignore. This is used to keep database in line with my old model, where ref_type was stored as number.
        /// </summary>
        [JsonIgnore]
        public int RefTypeNum { get { return (int)Enum.Parse(typeof(WalletJournalReferenceType), RefType); } }

        /// <summary>
        /// Converts the database string storage of ExtraInfo and returns the CWJExtraInfo object.
        /// </summary>
        /// <returns></returns>
        public CWJExtraInfo RetrieveExtraInfo()
        {
            return JsonConvert.DeserializeObject<CWJExtraInfo>(ExtraInfo_String);
        }
    }
}