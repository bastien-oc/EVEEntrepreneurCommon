using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EntrepreneurCommon.Models.Esi
{

    //public abstract class EsiDataModel
    //{
    //    [JsonIgnore] private IRestResponse restResponse;
    //    [JsonIgnore] public IRestResponse RestResponse { get; internal set; }

    //    [JsonIgnore] private DateTime cacheDate = default(DateTime);
    //    [JsonIgnore] private DateTime cacheLastModified = default(DateTime);
    //    [JsonIgnore] private DateTime cacheExpires = default(DateTime);

    //    [JsonIgnore] public DateTime CacheDate { get => cacheDate; internal set => cacheDate = value; }
    //    [JsonIgnore] public DateTime CacheLastModified { get => cacheLastModified; internal set => cacheLastModified = value; }
    //    [JsonIgnore] public DateTime CacheExpires { get => cacheExpires; internal set => cacheExpires = value; }

    //    private void SetCacheDates()
    //    {
    //        if (restResponse == null)
    //            return;
    //        try { CacheDate = DateTime.Parse((string)restResponse.Headers?.Where(x => x.Name == "Date").FirstOrDefault().Value ?? ""); }
    //        catch { }
    //        try { CacheLastModified = DateTime.Parse((string)restResponse.Headers?.Where(x => x.Name == "Last-Modified").FirstOrDefault().Value ?? ""); }
    //        catch { }
    //        try { CacheExpires = DateTime.Parse((string)restResponse.Headers?.Where(x => x.Name == "Expires").FirstOrDefault().Value ?? ""); }
    //        catch { }
    //    }

    //    public void AssignRestResponse( IRestResponse restResponse )
    //    {
    //        this.restResponse = restResponse;
    //        SetCacheDates();
    //    }
    //}

    /// <summary>
    /// Container for list-based and/or paginated resources.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EsiPaginatedResponse<T>
    {
        private List<T> items = new List<T>();
        private List<IRestResponse<List<T>>> responses = new List<IRestResponse<List<T>>>();

        public List<T> Items { get => items; set => items = value; }
        public List<IRestResponse<List<T>>> Responses { get => responses; set => responses = value; }
    }

    public class CharacterBlueprint
    {
        [JsonIgnore] public static readonly string Endpoint = "/characters/{character_id}/blueprints/";

        [J("item_id")] public Int64 ItemId { get; set; }
        [J("location_flag")] public string LocationFlag { get; set; }
        [J("location_id")] public Int64 LocationId { get; set; }
        [J("material_efficiency")] public Int32 MaterialEfficiency { get; set; }
        [J("quantity")] public Int32 Quantity { get; set; }
        [J("runs")] public Int32 Runs { get; set; }
        [J("time_efficiency")] public Int32 TimeEfficiency { get; set; }
        [J("type_id")] public Int32 TypeId { get; set; }

        [J("character_id")] public Int32 OwnerCharacterID { get; set; }
    }

    public class CharactersNames
    {
        [J("character_id")] public Int32 CharacterId { get; set; }
        [J("character_name")] public string CharacterName { get; set; }
    }

    public class EmploymenetHistory
    {
        [JsonIgnore] public static readonly String Endpoint = "/characters/{character_id}/corporationhistory/";

        [J("corporation_id")] public Int32 CorporationId { get; set; }
        [J("is_deleted")] public bool? IsDeleted { get; set; }
        [J("record_id")] public Int32 RecordId { get; set; }
        [J("start_date")] public string StartDate { get; set; }
    }

    public class SearchResponse
    {
        [JsonIgnore] public static readonly string Endpoint = "/v3/characters/{character_id}/search/";

        [J("agent")]            public List<int> Agent { get; set; }
        [J("alliance")]         public List<int> Alliance { get; set; }
        [J("character")]        public List<int> Character { get; set; }
        [J("constellation")]    public List<int> Constellation { get; set; }
        [J("corporation")]      public List<int> Corporation { get; set; }
        [J("faction")]          public List<int> Faction { get; set; }
        [J("inventorytype")]    public List<int> InventoryType { get; set; }
        [J("region")]           public List<int> Region { get; set; }
        [J("solarsystem")]      public List<int> SolarSystem { get; set; }
        [J("station")]          public List<int> Station { get; set; }
        [J("structure")]        public List<int> Structure { get; set; }
        [J("wormhole")]         public List<int> Wormhole { get; set; }
    }

    public class IndustryFacilities
    {
        [JsonIgnore] public static readonly String Endpoint = "/industry/facilities/";

        [J("facility_id")] public Int64 FacilityId { get; set; }
        [J("owner_id")] public Int32 OwnerId { get; set; }
        [J("region_id")] public Int32 RegionId { get; set; }
        [J("solar_system_id")] public Int32 SolarSystemId { get; set; }
        [J("tax")] public float Tax { get; set; }
        [J("type_id")] public Int32 TypeId { get; set; }
    }

    

    /// <summary>
    /// Get a list of corporation structures
    /// </summary>
    public class CorporationStructureDetail
    {
        [JsonIgnore] public static readonly string Endpoint = "/corporations/{corporation_id}/structures/";
        /// <summary>
        /// The Item ID of the structure
        /// </summary>
        [J("structure_id")] public long StructureID { get; set; }
        /// <summary>
        /// The type id of the structure
        /// </summary>
        [J("type_id")] public Int32 TypeID { get; set; }
        /// <summary>
        /// ID of the corporation that owns the structure
        /// </summary>
        [J("corporation_id")] public Int32 CorporationID { get; set; }
        /// <summary>
        /// The solar system the structure is in
        /// </summary>
        [J("system_id")] public Int32 SystemID;
        /// <summary>
        /// The id of the ACL profile for this citadel
        /// </summary>
        [J("profile_id")] public Int32 ProfileID;
        /// <summary>
        /// This week's vulnerability windows, Monday is day 0
        /// </summary>
        [J("current_vul")] public List<StructureVulnerability> CurrentVul { get; set; }
        /// <summary>
        /// Next week's vulnerability windows, Monday is day 0
        /// </summary>
        [J("next_vul")] public List<StructureVulnerability> NextVul { get; set; }
        /// <summary>
        /// Date on which the structure will run out of fuel
        /// </summary>
        [J("fuel_expires")] public string FuelExpires { get; set; }
        /// <summary>
        /// Contains a list of service upgrades, and their state (max 10)
        /// </summary>
        [J("services")] public List<StructureService> Services { get; set; }
        /// <summary>
        /// Date at which the structure entered it's current state
        /// </summary>
        [J("state_timer_start")] public string StateTimerStart { get; set; }
        /// <summary>
        /// Date at which the structure will move to it's next state
        /// </summary>
        [J("state_timer_end")] public string StateTimerEnd { get; set; }
        /// <summary>
        /// Date at which the structure will unanchor
        /// </summary>
        [J("unanchors_at")] public string Unanchors { get; set; }
    }

    public class StructureVulnerability
    {
        [J("day")] public int Day { get; set; }
        [J("hour")] public int Hour { get; set; }
    }

    public class StructureService
    {
        [J("name")] public string Name { get; set; }
        [J("state")] public string State { get; set; }
    }

    /// <summary>
    /// Get information on a station
    /// This route is cached for up to 300 seconds
    /// /universe/stations/{station_id}/
    /// </summary>
    public class StationInformation
    {
        [JsonIgnore] public static readonly String Endpoint = "/universe/stations/{station_id}/";

        [J("max_dockable_ship_volume")] public float MaxDockableShipVolume { get; set; }
        [J("name")] public string Name { get; set; }
        [J("office_rental_cost")] public float OfficeRentalCost { get; set; }
        [J("owner")] public Int32 Owner { get; set; }
        [J("position")] public StructurePosition Position { get; set; }
        [J("race_id")] public Int32 RaceId { get; set; }
        [J("reprocessing_efficiency")] public float ReprocessingEfficiency { get; set; }
        [J("reprocessing_stations_take")] public float ReprocessingStationsTake { get; set; }
        [J("services")] public List<string> Services { get; set; }
        [J("station_id")] public long StationId { get; set; }
        [J("system_id")] public long SystemId { get; set; }
        [J("type_id")] public long TypeId { get; set; }
    }


    public enum PathParamType { CharacterID, CorporationID };
    public enum LocationType { station, solar_system, other };
    public enum LocationFlag { AssetSafety, AutoFit, Bonus, Booster, BoosterBay, Capsule, Cargo, CorpDeliveries, CorpSAG1, CorpSAG2, CorpSAG3, CorpSAG4, CorpSAG5, CorpSAG6, CorpSAG7, CrateLoot, Deliveries, DroneBay, DustBattle, DustDatabank, FighterBay, FighterTube0, FighterTube1, FighterTube2, FighterTube3, FighterTube4, FleetHangar, Hangar, HangarAll, HiSlot0, HiSlot1, HiSlot2, HiSlot3, HiSlot4, HiSlot5, HiSlot6, HiSlot7, HiddenModifers, Implant, Impounded, JunkyardReprocessed, JunkyardTrashed, LoSlot0, LoSlot1, LoSlot2, LoSlot3, LoSlot4, LoSlot5, LoSlot6, LoSlot7, Locked, MedSlot0, MedSlot1, MedSlot2, MedSlot3, MedSlot4, MedSlot5, MedSlot6, MedSlot7, OfficeFolder, Pilot, PlanetSurface, QuafeBay, Reward, RigSlot0, RigSlot1, RigSlot2, RigSlot3, RigSlot4, RigSlot5, RigSlot6, RigSlot7, SecondaryStorage, ServiceSlot0, ServiceSlot1, ServiceSlot2, ServiceSlot3, ServiceSlot4, ServiceSlot5, ServiceSlot6, ServiceSlot7, ShipHangar, ShipOffline, Skill, SkillInTraining, SpecializedAmmoHold, SpecializedCommandCenterHold, SpecializedFuelBay, SpecializedGasHold, SpecializedIndustrialShipHold, SpecializedLargeShipHold, SpecializedMaterialBay, SpecializedMediumShipHold, SpecializedMineralHold, SpecializedOreHold, SpecializedPlanetaryCommoditiesHold, SpecializedSalvageHold, SpecializedShipHold, SpecializedSmallShipHold, StructureActive, StructureFuel, StructureInactive, StructureOffline, SubSystemSlot0, SubSystemSlot1, SubSystemSlot2, SubSystemSlot3, SubSystemSlot4, SubSystemSlot5, SubSystemSlot6, SubSystemSlot7, SubsystemBay, Unlocked, Wallet, Wardrobe }

    /// <summary>
    /// Return a list of the character's/corporation's assets
    /// Method: GET
    /// Path: character_id
    /// Query: datasource, page, token, user_agent
    /// Header: X-User-Agent
    /// </summary>
    [Table("assets")]
    public class AssetsModel
    {
        public static readonly String[] Endpoint = {
            "/v3/characters/{character_id}/assets/",
            "/v2/corporations/{corporation_id}/assets/"
        };
        public static readonly PathParamType[] ParamTypes = {
        PathParamType.CharacterID,
        PathParamType.CorporationID
    };

        //! ESI Returned Points
        [J("type_id")] [Column("type_id")] public Int32 TypeID { get; set; }

        [J("quantity")] [Column("quantity")] public Int32 Quantity { get; set; }
        [J("location_id")] [Column("location_id")] public Int64 LocationID { get; set; }

        [J("location_type")] [Column("location_type")] public string LocationType { get; set; }
        [J("item_id")] [Column("item_id")] public long ItemID { get; set; }
        [J("location_flag")] [Column("location_flag")] public string LocationFlag { get; set; }
        [J("is_singleton")] [Column("is_singleton")] public bool IsSingleton { get; set; }

        // Application fields, combine with follow-up requests;
        [Column("owner_id")] public Int32 OwnerID { get; set; }
        /// <summary>
        /// Get through /assets/names/ endpoint
        /// </summary>
        [Column("item_name")] public string ItemName { get; set; }
        /// <summary>
        /// Get through /assets/locations/ endpoint
        /// </summary>
        [Column("item_location")] public string ItemLocation { get; set; }
        [Column("is_owner_corp")] public Boolean IsOwnerCorp { get; set; }
    }

    /// <summary>
    /// Supplemental query to AssetModel. Return names for a set of item ids, which you can get from character or corporation assets endpoint. Typically used for items that can customize names, like containers or ships.
    /// Path: character_id or corporation_id
    /// Query: datasource, token, user_agent
    /// Body: item_ids = array of integer $int64
    /// Header: X-User-Agent
    /// </summary>
    public class AssetsName
    {
        public static readonly String[] Endpoint = {
            "/v2/characters/{character_id}/assets/names/",
            "/v2/corporations/{corporation_id}/assets/names/"
        };
        public static readonly PathParamType[] ParamTypes = {
            PathParamType.CharacterID,
            PathParamType.CorporationID
        };

        /// <summary>
        /// post_corporations_corporation_id_assets_names_item_id
        /// </summary>
        [J("item_id")]
        [Column("item_id")]
        public long ItemID { get; set; }

        /// <summary>
        /// post_corporations_corporation_id_assets_names_name
        /// </summary>
        [J("name")]
        [Column("name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// Return locations for a set of item ids, which you can get from character or corporation assets endpoint. Coordinates for items in hangars or stations are set to (0,0,0)
    /// </summary>
    public class AssetLocation
    {
        public static readonly String[] Endpoint = {
            "/v2/characters/{character_id}/assets/locations/",
            "/v2/corporations/{corporation_id}/assets/locations/"
        };

        public static readonly PathParamType[] ParamTypes = {
            PathParamType.CharacterID,
            PathParamType.CorporationID
        };

        // Response data

        /// <summary>
        /// 
        /// </summary>
        [J("item_id")]
        [Column("item_id")]
        public long ItemID { get; set; }

        [J("x")] public double X { get; set; }
        [J("y")] public double Y { get; set; }
        [J("z")] public double Z { get; set; }
    }

    /// <summary>
    /// Information about the characters current location. Returns the current solar system id, and also the current station or structure ID if applicable.
    /// Cache: Up to 5 seconds
    /// Path: character_id
    /// </summary>
    [Table("character_location")]
    public class CharacterLocation
    {
        public static readonly string Endpoint = "/characters/{character_id}/location/";
        public static readonly PathParamType ParamType = PathParamType.CharacterID;


        [Column("solar_system_id")] [J("solar_system_id")] public Int32 SolarSystemID { get; set; }
        [Column("station_id")] [J("station_id")] public Int32 StationID { get; set; }
        [Column("structure_id")] [J("structure_id")] public Int64 StructureID { get; set; }

        // App use only
        [Column("character_id")] [JsonIgnore] public Int32 CharacterId { get; set; }
    }

    public class CharacterShip
    {
        public static readonly string Endpoint = "/characters/{character_id}/ship/";
        public static readonly PathParamType ParamType = PathParamType.CharacterID;

        [J("ship_type_id")] public Int32 ship_type_id { get; set; }
        [J("ship_item_id")] public Int64 ship_item_id { get; set; }
        [J("ship_name")] public String ship_name { get; set; }
    }

    

    [JsonConverter(typeof(StringEnumConverter))]
    public enum RolesEnum
    {
        [EnumMember(Value = "Account_Take_1")] AccountTake1,
        [EnumMember(Value = "Account_Take_2")] AccountTake2,
        [EnumMember(Value = "Account_Take_3")] AccountTake3,
        [EnumMember(Value = "Account_Take_4")] AccountTake4,
        [EnumMember(Value = "Account_Take_5")] AccountTake5,
        [EnumMember(Value = "Account_Take_6")] AccountTake6,
        [EnumMember(Value = "Account_Take_7")] AccountTake7,
        [EnumMember(Value = "Accountant")] Accountant,
        [EnumMember(Value = "Auditor")] Auditor,
        [EnumMember(Value = "Communications_Officer")] CommunicationsOfficer,
        [EnumMember(Value = "Config_Equipment")] ConfigEquipment,
        [EnumMember(Value = "Config_Starbase_Equipment")] ConfigStarbaseEquipment,
        [EnumMember(Value = "Container_Take_1")] ContainerTake1,
        [EnumMember(Value = "Container_Take_2")] ContainerTake2,
        [EnumMember(Value = "Container_Take_3")] ContainerTake3,
        [EnumMember(Value = "Container_Take_4")] ContainerTake4,
        [EnumMember(Value = "Container_Take_5")] ContainerTake5,
        [EnumMember(Value = "Container_Take_6")] ContainerTake6,
        [EnumMember(Value = "Container_Take_7")] ContainerTake7,
        [EnumMember(Value = "Contract_Manager")] ContractManager,
        [EnumMember(Value = "Diplomat")] Diplomat,
        [EnumMember(Value = "Director")] Director,
        [EnumMember(Value = "Factory_Manager")] FactoryManager,
        [EnumMember(Value = "Fitting_Manager")] FittingManager,
        [EnumMember(Value = "Hangar_Query_1")] HangarQuery1,
        [EnumMember(Value = "Hangar_Query_2")] HangarQuery2,
        [EnumMember(Value = "Hangar_Query_3")] HangarQuery3,
        [EnumMember(Value = "Hangar_Query_4")] HangarQuery4,
        [EnumMember(Value = "Hangar_Query_5")] HangarQuery5,
        [EnumMember(Value = "Hangar_Query_6")] HangarQuery6,
        [EnumMember(Value = "Hangar_Query_7")] HangarQuery7,
        [EnumMember(Value = "Hangar_Take_1")] HangarTake1,
        [EnumMember(Value = "Hangar_Take_2")] HangarTake2,
        [EnumMember(Value = "Hangar_Take_3")] HangarTake3,
        [EnumMember(Value = "Hangar_Take_4")] HangarTake4,
        [EnumMember(Value = "Hangar_Take_5")] HangarTake5,
        [EnumMember(Value = "Hangar_Take_6")] HangarTake6,
        [EnumMember(Value = "Hangar_Take_7")] HangarTake7,
        [EnumMember(Value = "Junior_Accountant")] JuniorAccountant,
        [EnumMember(Value = "Personnel_Manager")] PersonnelManager,
        [EnumMember(Value = "Rent_Factory_Facility")] RentFactoryFacility,
        [EnumMember(Value = "Rent_Office")] RentOffice,
        [EnumMember(Value = "Rent_Research_Facility")] RentResearchFacility,
        [EnumMember(Value = "Security_Officer")] SecurityOfficer,
        [EnumMember(Value = "Starbase_Defense_Operator")] StarbaseDefenseOperator,
        [EnumMember(Value = "Starbase_Fuel_Technician")] StarbaseFuelTechnician,
        [EnumMember(Value = "Station_Manager")] StationManager,
        [EnumMember(Value = "Terrestrial_Combat_Officer")] TerrestrialCombatOfficer,
        [EnumMember(Value = "Terrestrial_Logistics_Officer")] TerrestrialLogisticsOfficer,
        [EnumMember(Value = "Trader")] Trader
    }

    public class CharacterRolesModel
    {
        [JsonIgnore] public static String EndpointVersioned { get => "/v2/characters/{character_id}/roles/"; }
        [JsonIgnore] public static String Scope { get => ""; }

        [J("roles")] public List<RolesEnum> Roles { get; set; }
        [J("roles_at_hq")] public List<RolesEnum> RolesAtHq { get; set; }
        [J("roles_at_base")] public List<RolesEnum> RolesAtBase { get; set; }
        [J("roles_at_other")] public List<RolesEnum> RolesAtOther { get; set; }
    }

    public class CharacterFatigueModel
    {
        [JsonIgnore] public static String EndpointVersioned { get => "/v1/characters/{character_id}/fatigue/"; }
        [J("last_jump_date")] public DateTime LastJumpDate { get; set; }
        [J("jump_fatigue_expire_date")] public DateTime JumpFatigueExpireDate { get; set; }
        [J("last_update_date")] public DateTime LastUpdateDate { get; set; }

        [JsonIgnore] public Int32 CharacterId { get; set; }
        [JsonIgnore] public String CharacterName { get; set; }
        [JsonIgnore]
        public TimeSpan Remaining
        {
            get {
                var span = JumpFatigueExpireDate - DateTime.UtcNow;
                if (span.TotalSeconds < 0) { return new TimeSpan(0, 0, 0); } else
                    return span;
            }
        }
    }
}
