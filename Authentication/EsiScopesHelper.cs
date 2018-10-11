using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Enum = System.Enum;

namespace EntrepreneurCommon.Authentication
{
    public static class EsiCharacterScopes
    {
        [ScopeString] public static string PublicData = "publicData";
        [ScopeString] public static string AssetsRead = "esi-assets.read_assets.v1";
        [ScopeString] public static string BookmarksRead = "esi-bookmarks.read_character_bookmarks.v1";
        [ScopeString] public static string CalendarRead = "esi-calendar.read_calendar_events.v1";
        [ScopeString] public static string CalendarRespond = "esi-calendar.respond_calendar_events.v1";
        [ScopeString] public static string AgentsResearchRead = "esi-characters.read_agents_research.v1";
        [ScopeString] public static string BlueprintsRead = "esi-characters.read_blueprints.v1";
        [ScopeString] public static string ChatChannelsRead = "esi-characters.read_chat_channels.v1";
        [ScopeString] public static string ContactsRead = "esi-characters.read_contacts.v1";
        [ScopeString] public static string FatigueRead = "esi-characters.read_fatigue.v1";
        [ScopeString] public static string FwStatsRead = "esi-characters.read_fw_stats.v1";
        [ScopeString] public static string LoyaltyRead = "esi-characters.read_loyalty.v1";
        [ScopeString] public static string MedalsRead = "esi-characters.read_medals.v1";
        [ScopeString] public static string NotificationsRead = "esi-characters.read_notifications.v1";
        [ScopeString] public static string OpportunitiesRead = "esi-characters.read_opportunities.v1";
        [ScopeString] public static string StandingsRead = "esi-characters.read_standings.v1";
        [ScopeString] public static string TitlesRead = "esi-characters.read_titles.v1";
        [ScopeString] public static string ContactsWrite = "esi-characters.write_contacts.v1";
        [ScopeString] public static string CharacterStatsRead = "esi-characterstats.read.v1";
        [ScopeString] public static string ClonesClonesRead = "esi-clones.read_clones.v1";
        [ScopeString] public static string ClonesImplantsRead = "esi-clones.read_implants.v1";
        [ScopeString] public static string ContractsRead = "esi-contracts.read_character_contracts.v1";
        [ScopeString] public static string FittingsRead = "esi-fittings.read_fittings.v1";
        [ScopeString] public static string FittingsWrite = "esi-fittings.write_fittings.v1";
        [ScopeString] public static string FleetRead = "esi-fleets.read_fleet.v1";
        [ScopeString] public static string FleetWrite = "esi-fleets.write_fleet.v1";
        [ScopeString] public static string IndustryJobsRead = "esi-industry.read_character_jobs.v1";
        [ScopeString] public static string IndustryMiningRead = "esi-industry.read_character_mining.v1";
        [ScopeString] public static string KillmailsRead = "esi-killmails.read_killmails.v1";
        [ScopeString] public static string LocationLocationRead = "esi-location.read_location.v1";
        [ScopeString] public static string LocationOnlineRead = "esi-location.read_online.v1";
        [ScopeString] public static string LocationShipTypeRead = "esi-location.read_ship_type.v1";
        [ScopeString] public static string MailOrganize = "esi-mail.organize_mail.v1";
        [ScopeString] public static string MailRead = "esi-mail.read_mail.v1";
        [ScopeString] public static string MailSend = "esi-mail.send_mail.v1";
        [ScopeString] public static string MarketsOrdersRead = "esi-markets.read_character_orders.v1";
        [ScopeString] public static string MarketsStructureMarkets = "esi-markets.structure_markets.v1";
        [ScopeString] public static string PlanetsManage = "esi-planets.manage_planets.v1";
        [ScopeString] public static string PlanetsCustomsOfficeRead = "esi-planets.read_customs_offices.v1";
        [ScopeString] public static string SearchStructures = "esi-search.search_structures.v1";
        [ScopeString] public static string SkillsQueueRead = "esi-skills.read_skillqueue.v1";
        [ScopeString] public static string SkillsRead = "esi-skills.read_skills.v1";
        [ScopeString] public static string UiOpenWindow = "esi-ui.open_window.v1";
        [ScopeString] public static string UiWaypointWrite = "esi-ui.write_waypoint.v1";
        [ScopeString] public static string UniverseStructuresRead = "esi-universe.read_structures.v1";
        [ScopeString] public static string WalletRead = "esi-wallet.read_character_wallet.v1";
        [ScopeString] public static string CorporationRolesRead = "esi-characters.read_corporation_roles.v1";
    }

    public static class EsiCorporationScopes
    {
        [ScopeString] public static string AlliancesContactsRead = "esi-alliances.read_contacts.v1";
        [ScopeString] public static string AssetsRead = "esi-assets.read_corporation_assets.v1";
        [ScopeString] public static string BookmarksRead = "esi-bookmarks.read_corporation_bookmarks.v1";
        [ScopeString] public static string ContractsRead = "esi-contracts.read_corporation_contracts.v1";
        [ScopeString] public static string BlueprintsRead = "esi-corporations.read_blueprints.v1";
        [ScopeString] public static string ContactsRead = "esi-corporations.read_contacts.v1";
        [ScopeString] public static string ContainerLogsRead = "esi-corporations.read_container_logs.v1";
        [ScopeString] public static string MembershipRead = "esi-corporations.read_corporation_membership.v1";
        [ScopeString] public static string DivisionsRead = "esi-corporations.read_divisions.v1";
        [ScopeString] public static string FacilitiesRead = "esi-corporations.read_facilities.v1";
        [ScopeString] public static string FwStatsRead = "esi-corporations.read_fw_stats.v1";
        [ScopeString] public static string MedalsRead = "esi-corporations.read_medals.v1";
        [ScopeString] public static string OutpostsRead = "esi-corporations.read_outposts.v1";
        [ScopeString] public static string StandingsRead = "esi-corporations.read_standings.v1";
        [ScopeString] public static string StarbasesRead = "esi-corporations.read_starbases.v1";
        [ScopeString] public static string StructuresRead = "esi-corporations.read_structures.v1";
        [ScopeString] public static string TitlesRead = "esi-corporations.read_titles.v1";
        [ScopeString] public static string TrackMembers = "esi-corporations.track_members.v1";
        [ScopeString] public static string StructuresWrite = "esi-corporations.write_structures.v1";
        [ScopeString] public static string IndustryJobsRead = "esi-industry.read_corporation_jobs.v1";
        [ScopeString] public static string IndustryMiningRead = "esi-industry.read_corporation_mining.v1";
        [ScopeString] public static string KillmailsRead = "esi-killmails.read_corporation_killmails.v1";
        [ScopeString] public static string MarketsOrdersRead = "esi-markets.read_corporation_orders.v1";
        [ScopeString] public static string WalletRead = "esi-wallet.read_corporation_wallet.v1";
        [ScopeString] public static string WalletsRead = "esi-wallet.read_corporation_wallets.v1";
    }

    internal class ScopeStringAttribute : Attribute { }

    public static class EsiScopesHelper
    {
        /// <summary>
        /// Parses multiple scope strings into a single, valid scope string.
        /// </summary>
        /// <param name="scopes"></param>
        /// <returns></returns>
        public static string ParseScopes(params string[] scopes)
        {
            var _str = "";
            foreach (var s in scopes) {
                _str += $"{s} ";
            }

            _str = _str.Trim();
            return _str;
        }

        /// <summary>
        /// Get list of valid scopes based on entries in the EsiCharacterScopes and EsiCorporationScopes.
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetScopes(ScopeEntityType entityType)
        {
            if ((entityType == ScopeEntityType.Character) || (entityType == ScopeEntityType.Both))
                foreach (var p in typeof(EsiCharacterScopes).GetFields()
                    .Where(a => a.IsDefined(typeof(ScopeStringAttribute), false))) {
                    yield return (string) p.GetValue(null);
                }

            if ((entityType == ScopeEntityType.Corporation) || (entityType == ScopeEntityType.Both)) {
                var type = typeof(EsiCorporationScopes);
                foreach (var p in type.GetFields()
                    .Where(a => a.IsDefined(typeof(ScopeStringAttribute), false))) {
                    yield return (string) p.GetValue(null);
                }
            }
        }
    }

    public enum ScopeEntityType
    {
        Character,
        Corporation,
        Both
    };

    //[Flags]
    //public enum EsiScopesCharacter
    //{
    //    [StringValue("publicData")] PublicData = 0x0,

    //    [StringValue("esi-assets.read_assets.v1")]
    //    Assets = 1,

    //    [StringValue("esi-bookmarks.read_character_bookmarks.v1")]
    //    Bookmarks = 2,

    //    [StringValue("esi-calendar.read_calendar_events.v1")]
    //    CalendarRead = 4,

    //    [StringValue("esi-calendar.respond_calendar_events.v1")]
    //    CalendarRespond = 8,

    //    [StringValue("esi-characters.read_agents_research.v1")]
    //    AgentsResearchRead = 16,

    //    [StringValue("esi-characters.read_blueprints.v1")]
    //    BlueprintsRead = 32,

    //    [StringValue("esi-characters.read_chat_channels.v1")]
    //    ChatChannelsRead = 0x7,

    //    [StringValue("esi-characters.read_contacts.v1")]
    //    ContactsRead = 0x8,

    //    [StringValue("esi-characters.read_corporation_roles.v1")]
    //    CorporationRolesRead = 0x9,

    //    [StringValue("esi-characters.read_fatigue.v1")]
    //    FatigueRead,

    //    [StringValue("esi-characters.read_fw_stats.v1")]
    //    FwStatsRead,

    //    [StringValue("esi-characters.read_loyalty.v1")]
    //    LoyaltyRead,

    //    [StringValue("esi-characters.read_medals.v1")]
    //    MedalsRead,

    //    [StringValue("esi-characters.read_notifications.v1")]
    //    NotificationsRead,

    //    [StringValue("esi-characters.read_opportunities.v1")]
    //    OpportunitiesRead,

    //    [StringValue("esi-characters.read_standings.v1")]
    //    StandingsRead,

    //    [StringValue("esi-characters.read_titles.v1")]
    //    TitlesRead,

    //    [StringValue("esi-characters.write_contacts.v1")]
    //    ContactsWrite,

    //    [StringValue("esi-characterstats.read.v1")]
    //    CharacterStats,

    //    [StringValue("esi-clones.read_clones.v1")]
    //    ClonesClonesRead,

    //    [StringValue("esi-clones.read_implants.v1")]
    //    ClonesImplantsRead,

    //    [StringValue("esi-contracts.read_character_contracts.v1")]
    //    ContractsRead,

    //    [StringValue("esi-fittings.read_fittings.v1")]
    //    FittingsRead,

    //    [StringValue("esi-fittings.write_fittings.v1")]
    //    FitingsWrite,

    //    [StringValue("esi-fleets.read_fleet.v1")]
    //    FleetRead,

    //    [StringValue("esi-fleets.write_fleet.v1")]
    //    FleetWrite,

    //    [StringValue("esi-industry.read_character_jobs.v1")]
    //    IndustryReadJobs,

    //    [StringValue("esi-industry.read_character_mining.v1")]
    //    IndustryReadMining,

    //    [StringValue("esi-killmails.read_killmails.v1")]
    //    Killmail,

    //    [StringValue("esi-location.read_location.v1")]
    //    LocationRead,

    //    [StringValue("esi-location.read_online.v1")]
    //    LocationOnline,

    //    [StringValue("esi-location.read_ship_type.v1")]
    //    LocationShip,

    //    [StringValue("esi-mail.organize_mail.v1")]
    //    MailOrganize,
    //    [StringValue("esi-mail.read_mail.v1")] MailRead,
    //    [StringValue("esi-mail.send_mail.v1")] MailSend,

    //    [StringValue("esi-markets.read_character_orders.v1")]
    //    MarketsOrdersRead,

    //    [StringValue("esi-markets.structure_markets.v1")]
    //    MarketsStructureMarkets,

    //    [StringValue("esi-planets.manage_planets.v1")]
    //    PlanetsManagePlanets,

    //    [StringValue("esi-planets.read_customs_offices.v1")]
    //    PlanetsReadCustomsOffices,

    //    [StringValue("esi-search.search_structures.v1")]
    //    SearchStructuresSearch,

    //    [StringValue("esi-skills.read_skillqueue.v1")]
    //    SkillsReadQueue,

    //    [StringValue("esi-skills.read_skills.v1")]
    //    SkillsReadSkills,
    //    [StringValue("esi-ui.open_window.v1")] UiOpenWindow,

    //    [StringValue("esi-ui.write_waypoint.v1")]
    //    UiWriteWaypoint,

    //    [StringValue("esi-universe.read_structures.v1")]
    //    UniverseReadStructures,

    //    [StringValue("esi-wallet.read_character_wallet.v1")]
    //    WalletRead
    //}

    public class StringValueAttribute : Attribute
    {
        public string Value;

        public StringValueAttribute(string value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Value;
        }

        //public static StringValueAttribute GetAttributeOfType(EsiScopesCharacter enumVal)
        //{
        //    var type = enumVal.GetType();
        //    var memInfo = type.GetMember(enumVal.ToString());
        //    var attributes = memInfo[0].GetCustomAttributes(typeof(StringValueAttribute), false);
        //    return (attributes.Length > 0) ? (StringValueAttribute) attributes[0] : null;
        //}
    }
}