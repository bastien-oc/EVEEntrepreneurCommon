using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Nito.AsyncEx;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.Esi
{
    /// <summary>
    /// String and Number correlation between Journal Reference Type Name and Type ID
    /// </summary>
    public enum WalletJournalReferenceType
    {
        player_trading = 1,
        market_transaction = 2,
        gm_cash_transfer = 3,
        mission_reward = 7,
        clone_activation = 8,
        inheritance = 9,
        player_donation = 10,
        corporation_payment = 11,
        docking_fee = 12,
        office_rental_fee = 13,
        factory_slot_rental_fee = 14,
        repair_bill = 15,
        bounty = 16,
        bounty_prize = 17,
        insurance = 19,
        mission_expiration = 20,
        mission_completion = 21,
        shares = 22,
        courier_mission_escrow = 23,
        mission_cost = 24,
        agent_miscellaneous = 25,
        lp_store = 26,
        agent_location_services = 27,
        agent_donation = 28,
        agent_security_services = 29,
        agent_mission_collateral_paid = 30,
        agent_mission_collateral_refunded = 31,
        agents_preward = 32,
        agent_mission_reward = 33,
        agent_mission_time_bonus_reward = 34,
        cspa = 35,
        cspaofflinerefund = 36,
        corporation_account_withdrawal = 37,
        corporation_dividend_payment = 38,
        corporation_registration_fee = 39,
        corporation_logo_change_cost = 40,
        release_of_impounded_property = 41,
        market_escrow = 42,
        agent_services_rendered = 43,
        market_fine_paid = 44,
        corporation_liquidation = 45,
        brokers_fee = 46,
        corporation_bulk_payment = 47,
        alliance_registration_fee = 48,
        war_fee = 49,
        alliance_maintainance_fee = 50,
        contraband_fine = 51,
        clone_transfer = 52,
        acceleration_gate_fee = 53,
        transaction_tax = 54,
        jump_clone_installation_fee = 55,
        manufacturing = 56,
        researching_technology = 57,
        researching_time_productivity = 58,
        researching_material_productivity = 59,
        copying = 60,
        reverse_engineering = 62,
        contract_auction_bid = 63,
        contract_auction_bid_refund = 64,
        contract_collateral = 65,
        contract_reward_refund = 66,
        contract_auction_sold = 67,
        contract_reward = 68,
        contract_collateral_refund = 69,
        contract_collateral_payout = 70,
        contract_price = 71,
        contract_brokers_fee = 72,
        contract_sales_tax = 73,
        contract_deposit = 74,
        contract_deposit_sales_tax = 75,
        contract_auction_bid_corp = 77,
        contract_collateral_deposited_corp = 78,
        contract_price_payment_corp = 79,
        contract_brokers_fee_corp = 80,
        contract_deposit_corp = 81,
        contract_deposit_refund = 82,
        contract_reward_deposited = 83,
        contract_reward_deposited_corp = 84,
        bounty_prizes = 85,
        advertisement_listing_fee = 86,
        medal_creation = 87,
        medal_issued = 88,
        dna_modification_fee = 90,
        sovereignity_bill = 91,
        bounty_prize_corporation_tax = 92,
        agent_mission_reward_corporation_tax = 93,
        agent_mission_time_bonus_reward_corporation_tax = 94,
        upkeep_adjustment_fee = 95,
        planetary_import_tax = 96,
        planetary_export_tax = 97,
        planetary_construction = 98,
        corporate_reward_payout = 99,
        bounty_surcharge = 101,
        contract_reversal = 102,
        corporate_reward_tax = 103,
        store_purchase = 106,
        store_purchase_refund = 107,
        datacore_fee = 112,
        war_fee_surrender = 113,
        war_ally_contract = 114,
        bounty_reimbursement = 115,
        kill_right_fee = 116,
        security_processing_fee = 117,
        industry_job_tax = 120,
        infrastructure_hub_maintenance = 122,
        asset_safety_recovery_tax = 123,
        opportunity_reward = 124,
        project_discovery_reward = 125,
        project_discovery_tax = 126,
        reprocessing_tax = 127,
        jump_clone_activation_fee = 128,
        operation_bonus = 129,
        resource_wars_reward = 131,
        duel_wager_escrow = 132,
        duel_wager_payment = 133,
        duel_wager_refund = 134,
        reaction = 135
    }

    /// <summary>
    /// Party type in the FirstPartyID and SecondPartyID
    /// </summary>
    public enum WalletPartyType
    {
        character = 0,
        corporation = 1,
        alliance = 2,
        faction = 3,
        system = 4
    }

    [Table("char_wallet_journal")]
    public class WalletJournalModelCharV4
    {
        [JsonIgnore] public static readonly String Endpoint = "/v4/characters/{character_id}/wallet/journal/";
        [JsonIgnore]
        [Column("wallet_owner_id", Order = 0), Key, Index("UNIQUE", IsUnique = true, Order = 0)]
        public int WalletOwnerID { get; set; }

        [JsonIgnore]
        [Column("wallet_owner_name")]
        public string WalletOwnerName { get; set; }

        [J("amount")]
        [Column("amount")]
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
        [JsonIgnore] public static String Endpoint = "/v3/characters/{character_id}/wallet/journal/";

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
        public Int32 WalletDivision { get; set; }

        /// <summary>
        /// Date and time of transaction
        /// </summary>
        [J("date")]
        [Column("date")]
        //[Index("INDEX",IsUnique = false)]
        public string Date { get; set; }

        /// <summary>
        /// Unique journal reference ID
        /// </summary>
        [J("ref_id")]
        [Column("ref_id", Order = 1), Key, Index("UNIQUE", IsUnique = true, Order = 1)]
        public long RefID { get; set; }

        /// <summary>
        /// Transaction type, different type of transaction will populate different fields in extra_info Note: If you have an existing XML API application that is using ref_types, you will need to know which string ESI ref_type maps to which integer. You can use the following gist to see string->int mappings: https://gist.github.com/ccp-zoetrope/c03db66d90c2148724c06171bc52e0ec
        /// </summary>
        [J("ref_type")]
        [Column("ref_type")]
        public string RefType { get; set; }

        /// <summary>
        /// first_party_id integer
        /// </summary>
        [J("first_party_id")]
        [Column("first_party_id")]
        public int FirstPartyID { get; set; }

        /// <summary>
        /// first_party_type string = ['character', 'corporation', 'alliance', 'faction', 'system']
        /// </summary>
        [J("first_party_type")]
        [Column("first_party_type")]
        public string FirstPartyType { get; set; }

        /// <summary>
        /// Name of the first party, obtained through app for ease of access. Not obtained through ESI, but keep in case of Json Exports
        /// </summary>
        [J("first_party_name")]
        [Column("first_party_name")]
        public string FirstPartyName { get; set; }

        /// <summary>
        /// second_party_id integer
        /// </summary>
        [J("second_party_id")]
        [Column("second_party_id")]
        public int SecondPartyID { get; set; }

        /// <summary>
        /// second_party_type string = ['character', 'corporation', 'alliance', 'faction', 'system']
        /// </summary>
        [J("second_party_type")]
        [Column("second_party_type")]
        public string SecondPartyType { get; set; }

        /// <summary>
        /// Name of the second party, obtained through app for ease of access. Not obtained through ESI, but keep in case of Json exports.
        /// </summary>
        [J("second_party_name")]
        [Column("second_party_name")]
        public string SecondPartyName { get; set; }

        /// <summary>
        /// Transaction amount. Positive when value transferred to the first party. Negative otherwise
        /// </summary>
        [J("amount")]
        [Column("amount")]
        public double Amount { get; set; }

        /// <summary>
        /// Wallet balance after transaction occurred
        /// </summary>
        [J("balance")]
        [Column("balance")]
        public double Balance { get; set; }

        /// <summary>
        /// reason string
        /// </summary>
        [J("reason")]
        [Column("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// the corporation ID receiving any tax paid
        /// </summary>
        [J("tax_receiver_id")]
        [Column("tax_receiver_id")]
        public long TaxReceiverID { get; set; }

        /// <summary>
        /// Tax amount received for tax related transactions
        /// </summary>
        [J("tax")]
        [Column("tax_amount")]
        public double Tax { get; set; }

        /// <summary>
        /// Extra information for different type of transaction. Stored as TEXT in Database, but as CWJExtraInfo DataModel when obtained from ESI
        /// </summary>
        [J("extra_info"), NotMapped]
        public CWJExtraInfo ExtraInfo { get; set; }

        /// <summary>
        /// Database Use only, translated from ExtraInfo. Stores the sub-class as a json string.
        /// </summary>
        [JsonIgnore]
        [Column("extra_info")]
        public String ExtraInfo_String { get { return JsonConvert.SerializeObject(ExtraInfo); } }

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

    /// <summary>
    /// Retrieve character or corporation wallet journal
    /// /characters/{character_id}/wallet/journal/
    /// /corporations/{corporation_id}/wallets/{division}/journal/
    /// Path: (character_id) OR (corporation_id AND division)
    /// </summary>
    [Table("corp_wallet_journal")]
    public class WalletJournalModelCorp
    {
        [JsonIgnore, NotMapped] public static String Endpoint = "/corporations/{corporation_id}/wallets/{division}/journal/";
        [JsonIgnore, NotMapped] public static String EndpointVersioned = "/v2/corporations/{corporation_id}/wallets/{division}/journal/";

        /// <summary>
        /// Internal Database Use. Represents the Character or Corporation to whom the entry belongs.
        /// </summary>
        [JsonIgnore]
        [Column("wallet_owner_id", Order = 0)]
        [Key, Index("UNIQUE", 0, IsUnique =true)]
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
        public Int32 WalletDivision { get; set; }

        /// <summary>
        /// Date and time of transaction
        /// </summary>
        [J("date")]
        [Column("date")]
        public string Date { get; set; }

        /// <summary>
        /// Unique journal reference ID
        /// </summary>
        [J("ref_id")]
        [Column("ref_id", Order = 1)]
        [Key, Index("UNIQUE", 1, IsUnique = true)]
        public long RefID { get; set; }

        /// <summary>
        /// Transaction type, different type of transaction will populate different fields in extra_info Note: If you have an existing XML API application that is using ref_types, you will need to know which string ESI ref_type maps to which integer. You can use the following gist to see string->int mappings: https://gist.github.com/ccp-zoetrope/c03db66d90c2148724c06171bc52e0ec
        /// </summary>
        [J("ref_type")]
        [Column("ref_type")]
        public string RefType { get; set; }

        /// <summary>
        /// first_party_id integer
        /// </summary>
        [J("first_party_id")]
        [Column("first_party_id")]
        public int FirstPartyID { get; set; }

        /// <summary>
        /// first_party_type string = ['character', 'corporation', 'alliance', 'faction', 'system']
        /// </summary>
        [J("first_party_type")]
        [Column("first_party_type")]
        public string FirstPartyType { get; set; }

        /// <summary>
        /// Name of the first party, obtained through app for ease of access. Not obtained through ESI, but keep in case of Json Exports
        /// </summary>
        [J("first_party_name")]
        [Column("first_party_name")]
        public string FirstPartyName { get; set; }

        /// <summary>
        /// second_party_id integer
        /// </summary>
        [J("second_party_id")]
        [Column("second_party_id")]
        public int SecondPartyID { get; set; }

        /// <summary>
        /// second_party_type string = ['character', 'corporation', 'alliance', 'faction', 'system']
        /// </summary>
        [J("second_party_type")]
        [Column("second_party_type")]
        public string SecondPartyType { get; set; }

        /// <summary>
        /// Name of the second party, obtained through app for ease of access. Not obtained through ESI, but keep in case of Json exports.
        /// </summary>
        [J("second_party_name")]
        [Column("second_party_name")]
        public string SecondPartyName { get; set; }

        /// <summary>
        /// Transaction amount. Positive when value transferred to the first party. Negative otherwise
        /// </summary>
        [J("amount")]
        [Column("amount")]
        public double Amount { get; set; }

        /// <summary>
        /// Wallet balance after transaction occurred
        /// </summary>
        [J("balance")]
        [Column("balance")]
        public double Balance { get; set; }

        /// <summary>
        /// reason string
        /// </summary>
        [J("reason")]
        [Column("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// the corporation ID receiving any tax paid
        /// </summary>
        [J("tax_receiver_id")]
        [Column("tax_receiver_id")]
        public long TaxReceiverID { get; set; }

        /// <summary>
        /// Tax amount received for tax related transactions
        /// </summary>
        [J("tax")]
        [Column("tax_amount")]
        public double Tax { get; set; }

        /// <summary>
        /// Extra information for different type of transaction. Stored as TEXT in Database, but as CWJExtraInfo DataModel when obtained from ESI
        /// </summary>
        [J("extra_info")] [NotMapped]
        public CWJExtraInfo ExtraInfo { get; set; }

        /// <summary>
        /// Database Use only, translated from ExtraInfo. Stores the sub-class as a json string.
        /// </summary>
        [JsonIgnore]
        [Column("extra_info")]
        public String ExtraInfo_String { get { return JsonConvert.SerializeObject(ExtraInfo); } }

        /// <summary>
        /// OBSOLETE. Ignore. This is used to keep database in line with my old model, where ref_type was stored as number.
        /// </summary>
        [JsonIgnore] [NotMapped]
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

    public class CWJExtraInfo
    {
        [J("location_id")]
        public long LocationID { get; set; }

        [J("transaction_id")]
        public long TransactionID { get; set; }

        [J("npc_name")]
        public string NPCName { get; set; }

        [J("npc_id")]
        public long NPCID { get; set; }

        [J("destroyed_ship_type_id")]
        public long DestroyedShipTypeID { get; set; }

        [J("character_id")]
        public long CharacterID { get; set; }

        [J("corporation_id")]
        public long CorporationID { get; set; }

        [J("alliance_id")]
        public long AllianceID { get; set; }

        [J("job_id")]
        public long JobID { get; set; }

        [J("contract_id")]
        public long ContractID { get; set; }

        [J("system_id")]
        public long SystemID { get; set; }

        [J("planet_id")]
        public long PlanetID { get; set; }
    }
}
