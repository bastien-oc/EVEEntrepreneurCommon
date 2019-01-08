using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    /// <summary>
    ///     The type of the given context_id if present
    /// </summary>
    /// <value>The type of the given context_id if present</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContextIdTypeEnum
    {
        /// <summary>
        ///     Enum Structureid for value: structure_id
        /// </summary>
        [EnumMember(Value = "structure_id")] Structureid = 1,

        /// <summary>
        ///     Enum Stationid for value: station_id
        /// </summary>
        [EnumMember(Value = "station_id")] Stationid = 2,

        /// <summary>
        ///     Enum Markettransactionid for value: market_transaction_id
        /// </summary>
        [EnumMember(Value = "market_transaction_id")]
        Markettransactionid = 3,

        /// <summary>
        ///     Enum Characterid for value: character_id
        /// </summary>
        [EnumMember(Value = "character_id")] Characterid = 4,

        /// <summary>
        ///     Enum Corporationid for value: corporation_id
        /// </summary>
        [EnumMember(Value = "corporation_id")] Corporationid = 5,

        /// <summary>
        ///     Enum Allianceid for value: alliance_id
        /// </summary>
        [EnumMember(Value = "alliance_id")] Allianceid = 6,

        /// <summary>
        ///     Enum Evesystem for value: eve_system
        /// </summary>
        [EnumMember(Value = "eve_system")] Evesystem = 7,

        /// <summary>
        ///     Enum Industryjobid for value: industry_job_id
        /// </summary>
        [EnumMember(Value = "industry_job_id")]
        Industryjobid = 8,

        /// <summary>
        ///     Enum Contractid for value: contract_id
        /// </summary>
        [EnumMember(Value = "contract_id")] Contractid = 9,

        /// <summary>
        ///     Enum Planetid for value: planet_id
        /// </summary>
        [EnumMember(Value = "planet_id")] Planetid = 10,

        /// <summary>
        ///     Enum Systemid for value: system_id
        /// </summary>
        [EnumMember(Value = "system_id")] Systemid = 11,

        /// <summary>
        ///     Enum Typeid for value: type_id
        /// </summary>
        [EnumMember(Value = "type_id")] Typeid = 12
    }

    /// <summary>
    ///     Valid order range, numbers are ranges in jumps
    /// </summary>
    /// <value>Valid order range, numbers are ranges in jumps</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RangeEnum
    {
        /// <summary>
        ///     Enum _1 for value: 1
        /// </summary>
        [EnumMember(Value = "1")] _1 = 1,

        /// <summary>
        ///     Enum _10 for value: 10
        /// </summary>
        [EnumMember(Value = "10")] _10 = 2,

        /// <summary>
        ///     Enum _2 for value: 2
        /// </summary>
        [EnumMember(Value = "2")] _2 = 3,

        /// <summary>
        ///     Enum _20 for value: 20
        /// </summary>
        [EnumMember(Value = "20")] _20 = 4,

        /// <summary>
        ///     Enum _3 for value: 3
        /// </summary>
        [EnumMember(Value = "3")] _3 = 5,

        /// <summary>
        ///     Enum _30 for value: 30
        /// </summary>
        [EnumMember(Value = "30")] _30 = 6,

        /// <summary>
        ///     Enum _4 for value: 4
        /// </summary>
        [EnumMember(Value = "4")] _4 = 7,

        /// <summary>
        ///     Enum _40 for value: 40
        /// </summary>
        [EnumMember(Value = "40")] _40 = 8,

        /// <summary>
        ///     Enum _5 for value: 5
        /// </summary>
        [EnumMember(Value = "5")] _5 = 9,

        /// <summary>
        ///     Enum Region for value: region
        /// </summary>
        [EnumMember(Value = "region")] Region = 10,

        /// <summary>
        ///     Enum Solarsystem for value: solarsystem
        /// </summary>
        [EnumMember(Value = "solarsystem")] Solarsystem = 11,

        /// <summary>
        ///     Enum Station for value: station
        /// </summary>
        [EnumMember(Value = "station")] Station = 12
    }


    /// <summary>
    ///     Current order state
    /// </summary>
    /// <value>Current order state</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StateEnum
    {
        /// <summary>
        ///     Enum Cancelled for value: cancelled
        /// </summary>
        [EnumMember(Value = "cancelled")] Cancelled = 1,

        /// <summary>
        ///     Enum Expired for value: expired
        /// </summary>
        [EnumMember(Value = "expired")] Expired = 2
    }


    /// <summary>
    ///     The transaction type for the given transaction. Different transaction types will populate different attributes.
    ///     Note: If you have an existing XML API application that is using ref_types, you will need to know which string ESI
    ///     ref_type maps to which integer. You can look at the following file to see string-&gt;int mappings:
    ///     https://github.com/ccpgames/eve-glue/blob/master/eve_glue/wallet_journal_ref.py
    /// </summary>
    /// <value>
    ///     The transaction type for the given transaction. Different transaction types will populate different attributes.
    ///     Note: If you have an existing XML API application that is using ref_types, you will need to know which string ESI
    ///     ref_type maps to which integer. You can look at the following file to see string-&gt;int mappings:
    ///     https://github.com/ccpgames/eve-glue/blob/master/eve_glue/wallet_journal_ref.py
    /// </value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RefTypeEnum
    {
        /// <summary>
        ///     Enum Accelerationgatefee for value: acceleration_gate_fee
        /// </summary>
        [EnumMember(Value = "acceleration_gate_fee")]
        Accelerationgatefee = 1,

        /// <summary>
        ///     Enum Advertisementlistingfee for value: advertisement_listing_fee
        /// </summary>
        [EnumMember(Value = "advertisement_listing_fee")]
        Advertisementlistingfee = 2,

        /// <summary>
        ///     Enum Agentdonation for value: agent_donation
        /// </summary>
        [EnumMember(Value = "agent_donation")] Agentdonation = 3,

        /// <summary>
        ///     Enum Agentlocationservices for value: agent_location_services
        /// </summary>
        [EnumMember(Value = "agent_location_services")]
        Agentlocationservices = 4,

        /// <summary>
        ///     Enum Agentmiscellaneous for value: agent_miscellaneous
        /// </summary>
        [EnumMember(Value = "agent_miscellaneous")]
        Agentmiscellaneous = 5,

        /// <summary>
        ///     Enum Agentmissioncollateralpaid for value: agent_mission_collateral_paid
        /// </summary>
        [EnumMember(Value = "agent_mission_collateral_paid")]
        Agentmissioncollateralpaid = 6,

        /// <summary>
        ///     Enum Agentmissioncollateralrefunded for value: agent_mission_collateral_refunded
        /// </summary>
        [EnumMember(Value = "agent_mission_collateral_refunded")]
        Agentmissioncollateralrefunded = 7,

        /// <summary>
        ///     Enum Agentmissionreward for value: agent_mission_reward
        /// </summary>
        [EnumMember(Value = "agent_mission_reward")]
        Agentmissionreward = 8,

        /// <summary>
        ///     Enum Agentmissionrewardcorporationtax for value: agent_mission_reward_corporation_tax
        /// </summary>
        [EnumMember(Value = "agent_mission_reward_corporation_tax")]
        Agentmissionrewardcorporationtax = 9,

        /// <summary>
        ///     Enum Agentmissiontimebonusreward for value: agent_mission_time_bonus_reward
        /// </summary>
        [EnumMember(Value = "agent_mission_time_bonus_reward")]
        Agentmissiontimebonusreward = 10,

        /// <summary>
        ///     Enum Agentmissiontimebonusrewardcorporationtax for value: agent_mission_time_bonus_reward_corporation_tax
        /// </summary>
        [EnumMember(Value = "agent_mission_time_bonus_reward_corporation_tax")]
        Agentmissiontimebonusrewardcorporationtax = 11,

        /// <summary>
        ///     Enum Agentsecurityservices for value: agent_security_services
        /// </summary>
        [EnumMember(Value = "agent_security_services")]
        Agentsecurityservices = 12,

        /// <summary>
        ///     Enum Agentservicesrendered for value: agent_services_rendered
        /// </summary>
        [EnumMember(Value = "agent_services_rendered")]
        Agentservicesrendered = 13,

        /// <summary>
        ///     Enum Agentspreward for value: agents_preward
        /// </summary>
        [EnumMember(Value = "agents_preward")] Agentspreward = 14,

        /// <summary>
        ///     Enum Alliancemaintainancefee for value: alliance_maintainance_fee
        /// </summary>
        [EnumMember(Value = "alliance_maintainance_fee")]
        Alliancemaintainancefee = 15,

        /// <summary>
        ///     Enum Allianceregistrationfee for value: alliance_registration_fee
        /// </summary>
        [EnumMember(Value = "alliance_registration_fee")]
        Allianceregistrationfee = 16,

        /// <summary>
        ///     Enum Assetsafetyrecoverytax for value: asset_safety_recovery_tax
        /// </summary>
        [EnumMember(Value = "asset_safety_recovery_tax")]
        Assetsafetyrecoverytax = 17,

        /// <summary>
        ///     Enum Bounty for value: bounty
        /// </summary>
        [EnumMember(Value = "bounty")] Bounty = 18,

        /// <summary>
        ///     Enum Bountyprize for value: bounty_prize
        /// </summary>
        [EnumMember(Value = "bounty_prize")] Bountyprize = 19,

        /// <summary>
        ///     Enum Bountyprizecorporationtax for value: bounty_prize_corporation_tax
        /// </summary>
        [EnumMember(Value = "bounty_prize_corporation_tax")]
        Bountyprizecorporationtax = 20,

        /// <summary>
        ///     Enum Bountyprizes for value: bounty_prizes
        /// </summary>
        [EnumMember(Value = "bounty_prizes")] Bountyprizes = 21,

        /// <summary>
        ///     Enum Bountyreimbursement for value: bounty_reimbursement
        /// </summary>
        [EnumMember(Value = "bounty_reimbursement")]
        Bountyreimbursement = 22,

        /// <summary>
        ///     Enum Bountysurcharge for value: bounty_surcharge
        /// </summary>
        [EnumMember(Value = "bounty_surcharge")]
        Bountysurcharge = 23,

        /// <summary>
        ///     Enum Brokersfee for value: brokers_fee
        /// </summary>
        [EnumMember(Value = "brokers_fee")] Brokersfee = 24,

        /// <summary>
        ///     Enum Cloneactivation for value: clone_activation
        /// </summary>
        [EnumMember(Value = "clone_activation")]
        Cloneactivation = 25,

        /// <summary>
        ///     Enum Clonetransfer for value: clone_transfer
        /// </summary>
        [EnumMember(Value = "clone_transfer")] Clonetransfer = 26,

        /// <summary>
        ///     Enum Contrabandfine for value: contraband_fine
        /// </summary>
        [EnumMember(Value = "contraband_fine")]
        Contrabandfine = 27,

        /// <summary>
        ///     Enum Contractauctionbid for value: contract_auction_bid
        /// </summary>
        [EnumMember(Value = "contract_auction_bid")]
        Contractauctionbid = 28,

        /// <summary>
        ///     Enum Contractauctionbidcorp for value: contract_auction_bid_corp
        /// </summary>
        [EnumMember(Value = "contract_auction_bid_corp")]
        Contractauctionbidcorp = 29,

        /// <summary>
        ///     Enum Contractauctionbidrefund for value: contract_auction_bid_refund
        /// </summary>
        [EnumMember(Value = "contract_auction_bid_refund")]
        Contractauctionbidrefund = 30,

        /// <summary>
        ///     Enum Contractauctionsold for value: contract_auction_sold
        /// </summary>
        [EnumMember(Value = "contract_auction_sold")]
        Contractauctionsold = 31,

        /// <summary>
        ///     Enum Contractbrokersfee for value: contract_brokers_fee
        /// </summary>
        [EnumMember(Value = "contract_brokers_fee")]
        Contractbrokersfee = 32,

        /// <summary>
        ///     Enum Contractbrokersfeecorp for value: contract_brokers_fee_corp
        /// </summary>
        [EnumMember(Value = "contract_brokers_fee_corp")]
        Contractbrokersfeecorp = 33,

        /// <summary>
        ///     Enum Contractcollateral for value: contract_collateral
        /// </summary>
        [EnumMember(Value = "contract_collateral")]
        Contractcollateral = 34,

        /// <summary>
        ///     Enum Contractcollateraldepositedcorp for value: contract_collateral_deposited_corp
        /// </summary>
        [EnumMember(Value = "contract_collateral_deposited_corp")]
        Contractcollateraldepositedcorp = 35,

        /// <summary>
        ///     Enum Contractcollateralpayout for value: contract_collateral_payout
        /// </summary>
        [EnumMember(Value = "contract_collateral_payout")]
        Contractcollateralpayout = 36,

        /// <summary>
        ///     Enum Contractcollateralrefund for value: contract_collateral_refund
        /// </summary>
        [EnumMember(Value = "contract_collateral_refund")]
        Contractcollateralrefund = 37,

        /// <summary>
        ///     Enum Contractdeposit for value: contract_deposit
        /// </summary>
        [EnumMember(Value = "contract_deposit")]
        Contractdeposit = 38,

        /// <summary>
        ///     Enum Contractdepositcorp for value: contract_deposit_corp
        /// </summary>
        [EnumMember(Value = "contract_deposit_corp")]
        Contractdepositcorp = 39,

        /// <summary>
        ///     Enum Contractdepositrefund for value: contract_deposit_refund
        /// </summary>
        [EnumMember(Value = "contract_deposit_refund")]
        Contractdepositrefund = 40,

        /// <summary>
        ///     Enum Contractdepositsalestax for value: contract_deposit_sales_tax
        /// </summary>
        [EnumMember(Value = "contract_deposit_sales_tax")]
        Contractdepositsalestax = 41,

        /// <summary>
        ///     Enum Contractprice for value: contract_price
        /// </summary>
        [EnumMember(Value = "contract_price")] Contractprice = 42,

        /// <summary>
        ///     Enum Contractpricepaymentcorp for value: contract_price_payment_corp
        /// </summary>
        [EnumMember(Value = "contract_price_payment_corp")]
        Contractpricepaymentcorp = 43,

        /// <summary>
        ///     Enum Contractreversal for value: contract_reversal
        /// </summary>
        [EnumMember(Value = "contract_reversal")]
        Contractreversal = 44,

        /// <summary>
        ///     Enum Contractreward for value: contract_reward
        /// </summary>
        [EnumMember(Value = "contract_reward")]
        Contractreward = 45,

        /// <summary>
        ///     Enum Contractrewarddeposited for value: contract_reward_deposited
        /// </summary>
        [EnumMember(Value = "contract_reward_deposited")]
        Contractrewarddeposited = 46,

        /// <summary>
        ///     Enum Contractrewarddepositedcorp for value: contract_reward_deposited_corp
        /// </summary>
        [EnumMember(Value = "contract_reward_deposited_corp")]
        Contractrewarddepositedcorp = 47,

        /// <summary>
        ///     Enum Contractrewardrefund for value: contract_reward_refund
        /// </summary>
        [EnumMember(Value = "contract_reward_refund")]
        Contractrewardrefund = 48,

        /// <summary>
        ///     Enum Contractsalestax for value: contract_sales_tax
        /// </summary>
        [EnumMember(Value = "contract_sales_tax")]
        Contractsalestax = 49,

        /// <summary>
        ///     Enum Copying for value: copying
        /// </summary>
        [EnumMember(Value = "copying")] Copying = 50,

        /// <summary>
        ///     Enum Corporaterewardpayout for value: corporate_reward_payout
        /// </summary>
        [EnumMember(Value = "corporate_reward_payout")]
        Corporaterewardpayout = 51,

        /// <summary>
        ///     Enum Corporaterewardtax for value: corporate_reward_tax
        /// </summary>
        [EnumMember(Value = "corporate_reward_tax")]
        Corporaterewardtax = 52,

        /// <summary>
        ///     Enum Corporationaccountwithdrawal for value: corporation_account_withdrawal
        /// </summary>
        [EnumMember(Value = "corporation_account_withdrawal")]
        Corporationaccountwithdrawal = 53,

        /// <summary>
        ///     Enum Corporationbulkpayment for value: corporation_bulk_payment
        /// </summary>
        [EnumMember(Value = "corporation_bulk_payment")]
        Corporationbulkpayment = 54,

        /// <summary>
        ///     Enum Corporationdividendpayment for value: corporation_dividend_payment
        /// </summary>
        [EnumMember(Value = "corporation_dividend_payment")]
        Corporationdividendpayment = 55,

        /// <summary>
        ///     Enum Corporationliquidation for value: corporation_liquidation
        /// </summary>
        [EnumMember(Value = "corporation_liquidation")]
        Corporationliquidation = 56,

        /// <summary>
        ///     Enum Corporationlogochangecost for value: corporation_logo_change_cost
        /// </summary>
        [EnumMember(Value = "corporation_logo_change_cost")]
        Corporationlogochangecost = 57,

        /// <summary>
        ///     Enum Corporationpayment for value: corporation_payment
        /// </summary>
        [EnumMember(Value = "corporation_payment")]
        Corporationpayment = 58,

        /// <summary>
        ///     Enum Corporationregistrationfee for value: corporation_registration_fee
        /// </summary>
        [EnumMember(Value = "corporation_registration_fee")]
        Corporationregistrationfee = 59,

        /// <summary>
        ///     Enum Couriermissionescrow for value: courier_mission_escrow
        /// </summary>
        [EnumMember(Value = "courier_mission_escrow")]
        Couriermissionescrow = 60,

        /// <summary>
        ///     Enum Cspa for value: cspa
        /// </summary>
        [EnumMember(Value = "cspa")] Cspa = 61,

        /// <summary>
        ///     Enum Cspaofflinerefund for value: cspaofflinerefund
        /// </summary>
        [EnumMember(Value = "cspaofflinerefund")]
        Cspaofflinerefund = 62,

        /// <summary>
        ///     Enum Datacorefee for value: datacore_fee
        /// </summary>
        [EnumMember(Value = "datacore_fee")] Datacorefee = 63,

        /// <summary>
        ///     Enum Dnamodificationfee for value: dna_modification_fee
        /// </summary>
        [EnumMember(Value = "dna_modification_fee")]
        Dnamodificationfee = 64,

        /// <summary>
        ///     Enum Dockingfee for value: docking_fee
        /// </summary>
        [EnumMember(Value = "docking_fee")] Dockingfee = 65,

        /// <summary>
        ///     Enum Duelwagerescrow for value: duel_wager_escrow
        /// </summary>
        [EnumMember(Value = "duel_wager_escrow")]
        Duelwagerescrow = 66,

        /// <summary>
        ///     Enum Duelwagerpayment for value: duel_wager_payment
        /// </summary>
        [EnumMember(Value = "duel_wager_payment")]
        Duelwagerpayment = 67,

        /// <summary>
        ///     Enum Duelwagerrefund for value: duel_wager_refund
        /// </summary>
        [EnumMember(Value = "duel_wager_refund")]
        Duelwagerrefund = 68,

        /// <summary>
        ///     Enum Factoryslotrentalfee for value: factory_slot_rental_fee
        /// </summary>
        [EnumMember(Value = "factory_slot_rental_fee")]
        Factoryslotrentalfee = 69,

        /// <summary>
        ///     Enum Gmcashtransfer for value: gm_cash_transfer
        /// </summary>
        [EnumMember(Value = "gm_cash_transfer")]
        Gmcashtransfer = 70,

        /// <summary>
        ///     Enum Industryjobtax for value: industry_job_tax
        /// </summary>
        [EnumMember(Value = "industry_job_tax")]
        Industryjobtax = 71,

        /// <summary>
        ///     Enum Infrastructurehubmaintenance for value: infrastructure_hub_maintenance
        /// </summary>
        [EnumMember(Value = "infrastructure_hub_maintenance")]
        Infrastructurehubmaintenance = 72,

        /// <summary>
        ///     Enum Inheritance for value: inheritance
        /// </summary>
        [EnumMember(Value = "inheritance")] Inheritance = 73,

        /// <summary>
        ///     Enum Insurance for value: insurance
        /// </summary>
        [EnumMember(Value = "insurance")] Insurance = 74,

        /// <summary>
        ///     Enum Jumpcloneactivationfee for value: jump_clone_activation_fee
        /// </summary>
        [EnumMember(Value = "jump_clone_activation_fee")]
        Jumpcloneactivationfee = 75,

        /// <summary>
        ///     Enum Jumpcloneinstallationfee for value: jump_clone_installation_fee
        /// </summary>
        [EnumMember(Value = "jump_clone_installation_fee")]
        Jumpcloneinstallationfee = 76,

        /// <summary>
        ///     Enum Killrightfee for value: kill_right_fee
        /// </summary>
        [EnumMember(Value = "kill_right_fee")] Killrightfee = 77,

        /// <summary>
        ///     Enum Lpstore for value: lp_store
        /// </summary>
        [EnumMember(Value = "lp_store")] Lpstore = 78,

        /// <summary>
        ///     Enum Manufacturing for value: manufacturing
        /// </summary>
        [EnumMember(Value = "manufacturing")] Manufacturing = 79,

        /// <summary>
        ///     Enum Marketescrow for value: market_escrow
        /// </summary>
        [EnumMember(Value = "market_escrow")] Marketescrow = 80,

        /// <summary>
        ///     Enum Marketfinepaid for value: market_fine_paid
        /// </summary>
        [EnumMember(Value = "market_fine_paid")]
        Marketfinepaid = 81,

        /// <summary>
        ///     Enum Markettransaction for value: market_transaction
        /// </summary>
        [EnumMember(Value = "market_transaction")]
        Markettransaction = 82,

        /// <summary>
        ///     Enum Medalcreation for value: medal_creation
        /// </summary>
        [EnumMember(Value = "medal_creation")] Medalcreation = 83,

        /// <summary>
        ///     Enum Medalissued for value: medal_issued
        /// </summary>
        [EnumMember(Value = "medal_issued")] Medalissued = 84,

        /// <summary>
        ///     Enum Missioncompletion for value: mission_completion
        /// </summary>
        [EnumMember(Value = "mission_completion")]
        Missioncompletion = 85,

        /// <summary>
        ///     Enum Missioncost for value: mission_cost
        /// </summary>
        [EnumMember(Value = "mission_cost")] Missioncost = 86,

        /// <summary>
        ///     Enum Missionexpiration for value: mission_expiration
        /// </summary>
        [EnumMember(Value = "mission_expiration")]
        Missionexpiration = 87,

        /// <summary>
        ///     Enum Missionreward for value: mission_reward
        /// </summary>
        [EnumMember(Value = "mission_reward")] Missionreward = 88,

        /// <summary>
        ///     Enum Officerentalfee for value: office_rental_fee
        /// </summary>
        [EnumMember(Value = "office_rental_fee")]
        Officerentalfee = 89,

        /// <summary>
        ///     Enum Operationbonus for value: operation_bonus
        /// </summary>
        [EnumMember(Value = "operation_bonus")]
        Operationbonus = 90,

        /// <summary>
        ///     Enum Opportunityreward for value: opportunity_reward
        /// </summary>
        [EnumMember(Value = "opportunity_reward")]
        Opportunityreward = 91,

        /// <summary>
        ///     Enum Planetaryconstruction for value: planetary_construction
        /// </summary>
        [EnumMember(Value = "planetary_construction")]
        Planetaryconstruction = 92,

        /// <summary>
        ///     Enum Planetaryexporttax for value: planetary_export_tax
        /// </summary>
        [EnumMember(Value = "planetary_export_tax")]
        Planetaryexporttax = 93,

        /// <summary>
        ///     Enum Planetaryimporttax for value: planetary_import_tax
        /// </summary>
        [EnumMember(Value = "planetary_import_tax")]
        Planetaryimporttax = 94,

        /// <summary>
        ///     Enum Playerdonation for value: player_donation
        /// </summary>
        [EnumMember(Value = "player_donation")]
        Playerdonation = 95,

        /// <summary>
        ///     Enum Playertrading for value: player_trading
        /// </summary>
        [EnumMember(Value = "player_trading")] Playertrading = 96,

        /// <summary>
        ///     Enum Projectdiscoveryreward for value: project_discovery_reward
        /// </summary>
        [EnumMember(Value = "project_discovery_reward")]
        Projectdiscoveryreward = 97,

        /// <summary>
        ///     Enum Projectdiscoverytax for value: project_discovery_tax
        /// </summary>
        [EnumMember(Value = "project_discovery_tax")]
        Projectdiscoverytax = 98,

        /// <summary>
        ///     Enum Reaction for value: reaction
        /// </summary>
        [EnumMember(Value = "reaction")] Reaction = 99,

        /// <summary>
        ///     Enum Releaseofimpoundedproperty for value: release_of_impounded_property
        /// </summary>
        [EnumMember(Value = "release_of_impounded_property")]
        Releaseofimpoundedproperty = 100,

        /// <summary>
        ///     Enum Repairbill for value: repair_bill
        /// </summary>
        [EnumMember(Value = "repair_bill")] Repairbill = 101,

        /// <summary>
        ///     Enum Reprocessingtax for value: reprocessing_tax
        /// </summary>
        [EnumMember(Value = "reprocessing_tax")]
        Reprocessingtax = 102,

        /// <summary>
        ///     Enum Researchingmaterialproductivity for value: researching_material_productivity
        /// </summary>
        [EnumMember(Value = "researching_material_productivity")]
        Researchingmaterialproductivity = 103,

        /// <summary>
        ///     Enum Researchingtechnology for value: researching_technology
        /// </summary>
        [EnumMember(Value = "researching_technology")]
        Researchingtechnology = 104,

        /// <summary>
        ///     Enum Researchingtimeproductivity for value: researching_time_productivity
        /// </summary>
        [EnumMember(Value = "researching_time_productivity")]
        Researchingtimeproductivity = 105,

        /// <summary>
        ///     Enum Resourcewarsreward for value: resource_wars_reward
        /// </summary>
        [EnumMember(Value = "resource_wars_reward")]
        Resourcewarsreward = 106,

        /// <summary>
        ///     Enum Reverseengineering for value: reverse_engineering
        /// </summary>
        [EnumMember(Value = "reverse_engineering")]
        Reverseengineering = 107,

        /// <summary>
        ///     Enum Securityprocessingfee for value: security_processing_fee
        /// </summary>
        [EnumMember(Value = "security_processing_fee")]
        Securityprocessingfee = 108,

        /// <summary>
        ///     Enum Shares for value: shares
        /// </summary>
        [EnumMember(Value = "shares")] Shares = 109,

        /// <summary>
        ///     Enum Sovereignitybill for value: sovereignity_bill
        /// </summary>
        [EnumMember(Value = "sovereignity_bill")]
        Sovereignitybill = 110,

        /// <summary>
        ///     Enum Storepurchase for value: store_purchase
        /// </summary>
        [EnumMember(Value = "store_purchase")] Storepurchase = 111,

        /// <summary>
        ///     Enum Storepurchaserefund for value: store_purchase_refund
        /// </summary>
        [EnumMember(Value = "store_purchase_refund")]
        Storepurchaserefund = 112,

        /// <summary>
        ///     Enum Transactiontax for value: transaction_tax
        /// </summary>
        [EnumMember(Value = "transaction_tax")]
        Transactiontax = 113,

        /// <summary>
        ///     Enum Upkeepadjustmentfee for value: upkeep_adjustment_fee
        /// </summary>
        [EnumMember(Value = "upkeep_adjustment_fee")]
        Upkeepadjustmentfee = 114,

        /// <summary>
        ///     Enum Warallycontract for value: war_ally_contract
        /// </summary>
        [EnumMember(Value = "war_ally_contract")]
        Warallycontract = 115,

        /// <summary>
        ///     Enum Warfee for value: war_fee
        /// </summary>
        [EnumMember(Value = "war_fee")] Warfee = 116,

        /// <summary>
        ///     Enum Warfeesurrender for value: war_fee_surrender
        /// </summary>
        [EnumMember(Value = "war_fee_surrender")]
        Warfeesurrender = 117
    }

    /// <summary>
    ///     String and Number correlation between Journal Reference Type Name and Type ID
    /// </summary>
    public enum WalletJournalReferenceType
    {
        player_trading                                  = 1,
        market_transaction                              = 2,
        gm_cash_transfer                                = 3,
        mission_reward                                  = 7,
        clone_activation                                = 8,
        inheritance                                     = 9,
        player_donation                                 = 10,
        corporation_payment                             = 11,
        docking_fee                                     = 12,
        office_rental_fee                               = 13,
        factory_slot_rental_fee                         = 14,
        repair_bill                                     = 15,
        bounty                                          = 16,
        bounty_prize                                    = 17,
        insurance                                       = 19,
        mission_expiration                              = 20,
        mission_completion                              = 21,
        shares                                          = 22,
        courier_mission_escrow                          = 23,
        mission_cost                                    = 24,
        agent_miscellaneous                             = 25,
        lp_store                                        = 26,
        agent_location_services                         = 27,
        agent_donation                                  = 28,
        agent_security_services                         = 29,
        agent_mission_collateral_paid                   = 30,
        agent_mission_collateral_refunded               = 31,
        agents_preward                                  = 32,
        agent_mission_reward                            = 33,
        agent_mission_time_bonus_reward                 = 34,
        cspa                                            = 35,
        cspaofflinerefund                               = 36,
        corporation_account_withdrawal                  = 37,
        corporation_dividend_payment                    = 38,
        corporation_registration_fee                    = 39,
        corporation_logo_change_cost                    = 40,
        release_of_impounded_property                   = 41,
        market_escrow                                   = 42,
        agent_services_rendered                         = 43,
        market_fine_paid                                = 44,
        corporation_liquidation                         = 45,
        brokers_fee                                     = 46,
        corporation_bulk_payment                        = 47,
        alliance_registration_fee                       = 48,
        war_fee                                         = 49,
        alliance_maintainance_fee                       = 50,
        contraband_fine                                 = 51,
        clone_transfer                                  = 52,
        acceleration_gate_fee                           = 53,
        transaction_tax                                 = 54,
        jump_clone_installation_fee                     = 55,
        manufacturing                                   = 56,
        researching_technology                          = 57,
        researching_time_productivity                   = 58,
        researching_material_productivity               = 59,
        copying                                         = 60,
        reverse_engineering                             = 62,
        contract_auction_bid                            = 63,
        contract_auction_bid_refund                     = 64,
        contract_collateral                             = 65,
        contract_reward_refund                          = 66,
        contract_auction_sold                           = 67,
        contract_reward                                 = 68,
        contract_collateral_refund                      = 69,
        contract_collateral_payout                      = 70,
        contract_price                                  = 71,
        contract_brokers_fee                            = 72,
        contract_sales_tax                              = 73,
        contract_deposit                                = 74,
        contract_deposit_sales_tax                      = 75,
        contract_auction_bid_corp                       = 77,
        contract_collateral_deposited_corp              = 78,
        contract_price_payment_corp                     = 79,
        contract_brokers_fee_corp                       = 80,
        contract_deposit_corp                           = 81,
        contract_deposit_refund                         = 82,
        contract_reward_deposited                       = 83,
        contract_reward_deposited_corp                  = 84,
        bounty_prizes                                   = 85,
        advertisement_listing_fee                       = 86,
        medal_creation                                  = 87,
        medal_issued                                    = 88,
        dna_modification_fee                            = 90,
        sovereignity_bill                               = 91,
        bounty_prize_corporation_tax                    = 92,
        agent_mission_reward_corporation_tax            = 93,
        agent_mission_time_bonus_reward_corporation_tax = 94,
        upkeep_adjustment_fee                           = 95,
        planetary_import_tax                            = 96,
        planetary_export_tax                            = 97,
        planetary_construction                          = 98,
        corporate_reward_payout                         = 99,
        bounty_surcharge                                = 101,
        contract_reversal                               = 102,
        corporate_reward_tax                            = 103,
        store_purchase                                  = 106,
        store_purchase_refund                           = 107,
        datacore_fee                                    = 112,
        war_fee_surrender                               = 113,
        war_ally_contract                               = 114,
        bounty_reimbursement                            = 115,
        kill_right_fee                                  = 116,
        security_processing_fee                         = 117,
        industry_job_tax                                = 120,
        infrastructure_hub_maintenance                  = 122,
        asset_safety_recovery_tax                       = 123,
        opportunity_reward                              = 124,
        project_discovery_reward                        = 125,
        project_discovery_tax                           = 126,
        reprocessing_tax                                = 127,
        jump_clone_activation_fee                       = 128,
        operation_bonus                                 = 129,
        resource_wars_reward                            = 131,
        duel_wager_escrow                               = 132,
        duel_wager_payment                              = 133,
        duel_wager_refund                               = 134,
        reaction                                        = 135
    }

    /// <summary>
    ///     Party type in the FirstPartyID and SecondPartyID
    /// </summary>
    public enum WalletPartyType
    {
        character   = 0,
        corporation = 1,
        alliance    = 2,
        faction     = 3,
        system      = 4
    }

    public enum LocationFlagEnum
    {
        /// <summary>
        ///     Enum AutoFit for value: AutoFit
        /// </summary>
        [EnumMember(Value = "AutoFit")] AutoFit = 1,

        /// <summary>
        ///     Enum Cargo for value: Cargo
        /// </summary>
        [EnumMember(Value = "Cargo")] Cargo = 2,

        /// <summary>
        ///     Enum CorpseBay for value: CorpseBay
        /// </summary>
        [EnumMember(Value = "CorpseBay")] CorpseBay = 3,

        /// <summary>
        ///     Enum DroneBay for value: DroneBay
        /// </summary>
        [EnumMember(Value = "DroneBay")] DroneBay = 4,

        /// <summary>
        ///     Enum FleetHangar for value: FleetHangar
        /// </summary>
        [EnumMember(Value = "FleetHangar")] FleetHangar = 5,

        /// <summary>
        ///     Enum Deliveries for value: Deliveries
        /// </summary>
        [EnumMember(Value = "Deliveries")] Deliveries = 6,

        /// <summary>
        ///     Enum HiddenModifiers for value: HiddenModifiers
        /// </summary>
        [EnumMember(Value = "HiddenModifiers")]
        HiddenModifiers = 7,

        /// <summary>
        ///     Enum Hangar for value: Hangar
        /// </summary>
        [EnumMember(Value = "Hangar")] Hangar = 8,

        /// <summary>
        ///     Enum HangarAll for value: HangarAll
        /// </summary>
        [EnumMember(Value = "HangarAll")] HangarAll = 9,

        /// <summary>
        ///     Enum LoSlot0 for value: LoSlot0
        /// </summary>
        [EnumMember(Value = "LoSlot0")] LoSlot0 = 10,

        /// <summary>
        ///     Enum LoSlot1 for value: LoSlot1
        /// </summary>
        [EnumMember(Value = "LoSlot1")] LoSlot1 = 11,

        /// <summary>
        ///     Enum LoSlot2 for value: LoSlot2
        /// </summary>
        [EnumMember(Value = "LoSlot2")] LoSlot2 = 12,

        /// <summary>
        ///     Enum LoSlot3 for value: LoSlot3
        /// </summary>
        [EnumMember(Value = "LoSlot3")] LoSlot3 = 13,

        /// <summary>
        ///     Enum LoSlot4 for value: LoSlot4
        /// </summary>
        [EnumMember(Value = "LoSlot4")] LoSlot4 = 14,

        /// <summary>
        ///     Enum LoSlot5 for value: LoSlot5
        /// </summary>
        [EnumMember(Value = "LoSlot5")] LoSlot5 = 15,

        /// <summary>
        ///     Enum LoSlot6 for value: LoSlot6
        /// </summary>
        [EnumMember(Value = "LoSlot6")] LoSlot6 = 16,

        /// <summary>
        ///     Enum LoSlot7 for value: LoSlot7
        /// </summary>
        [EnumMember(Value = "LoSlot7")] LoSlot7 = 17,

        /// <summary>
        ///     Enum MedSlot0 for value: MedSlot0
        /// </summary>
        [EnumMember(Value = "MedSlot0")] MedSlot0 = 18,

        /// <summary>
        ///     Enum MedSlot1 for value: MedSlot1
        /// </summary>
        [EnumMember(Value = "MedSlot1")] MedSlot1 = 19,

        /// <summary>
        ///     Enum MedSlot2 for value: MedSlot2
        /// </summary>
        [EnumMember(Value = "MedSlot2")] MedSlot2 = 20,

        /// <summary>
        ///     Enum MedSlot3 for value: MedSlot3
        /// </summary>
        [EnumMember(Value = "MedSlot3")] MedSlot3 = 21,

        /// <summary>
        ///     Enum MedSlot4 for value: MedSlot4
        /// </summary>
        [EnumMember(Value = "MedSlot4")] MedSlot4 = 22,

        /// <summary>
        ///     Enum MedSlot5 for value: MedSlot5
        /// </summary>
        [EnumMember(Value = "MedSlot5")] MedSlot5 = 23,

        /// <summary>
        ///     Enum MedSlot6 for value: MedSlot6
        /// </summary>
        [EnumMember(Value = "MedSlot6")] MedSlot6 = 24,

        /// <summary>
        ///     Enum MedSlot7 for value: MedSlot7
        /// </summary>
        [EnumMember(Value = "MedSlot7")] MedSlot7 = 25,

        /// <summary>
        ///     Enum HiSlot0 for value: HiSlot0
        /// </summary>
        [EnumMember(Value = "HiSlot0")] HiSlot0 = 26,

        /// <summary>
        ///     Enum HiSlot1 for value: HiSlot1
        /// </summary>
        [EnumMember(Value = "HiSlot1")] HiSlot1 = 27,

        /// <summary>
        ///     Enum HiSlot2 for value: HiSlot2
        /// </summary>
        [EnumMember(Value = "HiSlot2")] HiSlot2 = 28,

        /// <summary>
        ///     Enum HiSlot3 for value: HiSlot3
        /// </summary>
        [EnumMember(Value = "HiSlot3")] HiSlot3 = 29,

        /// <summary>
        ///     Enum HiSlot4 for value: HiSlot4
        /// </summary>
        [EnumMember(Value = "HiSlot4")] HiSlot4 = 30,

        /// <summary>
        ///     Enum HiSlot5 for value: HiSlot5
        /// </summary>
        [EnumMember(Value = "HiSlot5")] HiSlot5 = 31,

        /// <summary>
        ///     Enum HiSlot6 for value: HiSlot6
        /// </summary>
        [EnumMember(Value = "HiSlot6")] HiSlot6 = 32,

        /// <summary>
        ///     Enum HiSlot7 for value: HiSlot7
        /// </summary>
        [EnumMember(Value = "HiSlot7")] HiSlot7 = 33,

        /// <summary>
        ///     Enum AssetSafety for value: AssetSafety
        /// </summary>
        [EnumMember(Value = "AssetSafety")] AssetSafety = 34,

        /// <summary>
        ///     Enum Locked for value: Locked
        /// </summary>
        [EnumMember(Value = "Locked")] Locked = 35,

        /// <summary>
        ///     Enum Unlocked for value: Unlocked
        /// </summary>
        [EnumMember(Value = "Unlocked")] Unlocked = 36,

        /// <summary>
        ///     Enum Implant for value: Implant
        /// </summary>
        [EnumMember(Value = "Implant")] Implant = 37,

        /// <summary>
        ///     Enum QuafeBay for value: QuafeBay
        /// </summary>
        [EnumMember(Value = "QuafeBay")] QuafeBay = 38,

        /// <summary>
        ///     Enum RigSlot0 for value: RigSlot0
        /// </summary>
        [EnumMember(Value = "RigSlot0")] RigSlot0 = 39,

        /// <summary>
        ///     Enum RigSlot1 for value: RigSlot1
        /// </summary>
        [EnumMember(Value = "RigSlot1")] RigSlot1 = 40,

        /// <summary>
        ///     Enum RigSlot2 for value: RigSlot2
        /// </summary>
        [EnumMember(Value = "RigSlot2")] RigSlot2 = 41,

        /// <summary>
        ///     Enum RigSlot3 for value: RigSlot3
        /// </summary>
        [EnumMember(Value = "RigSlot3")] RigSlot3 = 42,

        /// <summary>
        ///     Enum RigSlot4 for value: RigSlot4
        /// </summary>
        [EnumMember(Value = "RigSlot4")] RigSlot4 = 43,

        /// <summary>
        ///     Enum RigSlot5 for value: RigSlot5
        /// </summary>
        [EnumMember(Value = "RigSlot5")] RigSlot5 = 44,

        /// <summary>
        ///     Enum RigSlot6 for value: RigSlot6
        /// </summary>
        [EnumMember(Value = "RigSlot6")] RigSlot6 = 45,

        /// <summary>
        ///     Enum RigSlot7 for value: RigSlot7
        /// </summary>
        [EnumMember(Value = "RigSlot7")] RigSlot7 = 46,

        /// <summary>
        ///     Enum ShipHangar for value: ShipHangar
        /// </summary>
        [EnumMember(Value = "ShipHangar")] ShipHangar = 47,

        /// <summary>
        ///     Enum SpecializedFuelBay for value: SpecializedFuelBay
        /// </summary>
        [EnumMember(Value = "SpecializedFuelBay")]
        SpecializedFuelBay = 48,

        /// <summary>
        ///     Enum SpecializedOreHold for value: SpecializedOreHold
        /// </summary>
        [EnumMember(Value = "SpecializedOreHold")]
        SpecializedOreHold = 49,

        /// <summary>
        ///     Enum SpecializedGasHold for value: SpecializedGasHold
        /// </summary>
        [EnumMember(Value = "SpecializedGasHold")]
        SpecializedGasHold = 50,

        /// <summary>
        ///     Enum SpecializedMineralHold for value: SpecializedMineralHold
        /// </summary>
        [EnumMember(Value = "SpecializedMineralHold")]
        SpecializedMineralHold = 51,

        /// <summary>
        ///     Enum SpecializedSalvageHold for value: SpecializedSalvageHold
        /// </summary>
        [EnumMember(Value = "SpecializedSalvageHold")]
        SpecializedSalvageHold = 52,

        /// <summary>
        ///     Enum SpecializedShipHold for value: SpecializedShipHold
        /// </summary>
        [EnumMember(Value = "SpecializedShipHold")]
        SpecializedShipHold = 53,

        /// <summary>
        ///     Enum SpecializedSmallShipHold for value: SpecializedSmallShipHold
        /// </summary>
        [EnumMember(Value = "SpecializedSmallShipHold")]
        SpecializedSmallShipHold = 54,

        /// <summary>
        ///     Enum SpecializedMediumShipHold for value: SpecializedMediumShipHold
        /// </summary>
        [EnumMember(Value = "SpecializedMediumShipHold")]
        SpecializedMediumShipHold = 55,

        /// <summary>
        ///     Enum SpecializedLargeShipHold for value: SpecializedLargeShipHold
        /// </summary>
        [EnumMember(Value = "SpecializedLargeShipHold")]
        SpecializedLargeShipHold = 56,

        /// <summary>
        ///     Enum SpecializedIndustrialShipHold for value: SpecializedIndustrialShipHold
        /// </summary>
        [EnumMember(Value = "SpecializedIndustrialShipHold")]
        SpecializedIndustrialShipHold = 57,

        /// <summary>
        ///     Enum SpecializedAmmoHold for value: SpecializedAmmoHold
        /// </summary>
        [EnumMember(Value = "SpecializedAmmoHold")]
        SpecializedAmmoHold = 58,

        /// <summary>
        ///     Enum SpecializedCommandCenterHold for value: SpecializedCommandCenterHold
        /// </summary>
        [EnumMember(Value = "SpecializedCommandCenterHold")]
        SpecializedCommandCenterHold = 59,

        /// <summary>
        ///     Enum SpecializedPlanetaryCommoditiesHold for value: SpecializedPlanetaryCommoditiesHold
        /// </summary>
        [EnumMember(Value = "SpecializedPlanetaryCommoditiesHold")]
        SpecializedPlanetaryCommoditiesHold = 60,

        /// <summary>
        ///     Enum SpecializedMaterialBay for value: SpecializedMaterialBay
        /// </summary>
        [EnumMember(Value = "SpecializedMaterialBay")]
        SpecializedMaterialBay = 61,

        /// <summary>
        ///     Enum SubSystemSlot0 for value: SubSystemSlot0
        /// </summary>
        [EnumMember(Value = "SubSystemSlot0")] SubSystemSlot0 = 62,

        /// <summary>
        ///     Enum SubSystemSlot1 for value: SubSystemSlot1
        /// </summary>
        [EnumMember(Value = "SubSystemSlot1")] SubSystemSlot1 = 63,

        /// <summary>
        ///     Enum SubSystemSlot2 for value: SubSystemSlot2
        /// </summary>
        [EnumMember(Value = "SubSystemSlot2")] SubSystemSlot2 = 64,

        /// <summary>
        ///     Enum SubSystemSlot3 for value: SubSystemSlot3
        /// </summary>
        [EnumMember(Value = "SubSystemSlot3")] SubSystemSlot3 = 65,

        /// <summary>
        ///     Enum SubSystemSlot4 for value: SubSystemSlot4
        /// </summary>
        [EnumMember(Value = "SubSystemSlot4")] SubSystemSlot4 = 66,

        /// <summary>
        ///     Enum SubSystemSlot5 for value: SubSystemSlot5
        /// </summary>
        [EnumMember(Value = "SubSystemSlot5")] SubSystemSlot5 = 67,

        /// <summary>
        ///     Enum SubSystemSlot6 for value: SubSystemSlot6
        /// </summary>
        [EnumMember(Value = "SubSystemSlot6")] SubSystemSlot6 = 68,

        /// <summary>
        ///     Enum SubSystemSlot7 for value: SubSystemSlot7
        /// </summary>
        [EnumMember(Value = "SubSystemSlot7")] SubSystemSlot7 = 69,

        /// <summary>
        ///     Enum FighterBay for value: FighterBay
        /// </summary>
        [EnumMember(Value = "FighterBay")] FighterBay = 70,

        /// <summary>
        ///     Enum FighterTube0 for value: FighterTube0
        /// </summary>
        [EnumMember(Value = "FighterTube0")] FighterTube0 = 71,

        /// <summary>
        ///     Enum FighterTube1 for value: FighterTube1
        /// </summary>
        [EnumMember(Value = "FighterTube1")] FighterTube1 = 72,

        /// <summary>
        ///     Enum FighterTube2 for value: FighterTube2
        /// </summary>
        [EnumMember(Value = "FighterTube2")] FighterTube2 = 73,

        /// <summary>
        ///     Enum FighterTube3 for value: FighterTube3
        /// </summary>
        [EnumMember(Value = "FighterTube3")] FighterTube3 = 74,

        /// <summary>
        ///     Enum FighterTube4 for value: FighterTube4
        /// </summary>
        [EnumMember(Value = "FighterTube4")] FighterTube4 = 75,

        /// <summary>
        ///     Enum Module for value: Module
        /// </summary>
        [EnumMember(Value = "Module")] Module = 76
    }
}
