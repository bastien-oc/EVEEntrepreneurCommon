using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    /// <summary>
    /// Container for list-based and/or paginated resources.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EsiPaginatedResponse<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public List<IRestResponse<List<T>>> Responses { get; set; } = new List<IRestResponse<List<T>>>();
    }

    public class CharactersNames
    {
        [J("character_id")] public int CharacterId { get; set; }
        [J("character_name")] public string CharacterName { get; set; }
    }

    public class EmploymenetHistory
    {
        [JsonIgnore] public static readonly string Endpoint = "/characters/{character_id}/corporationhistory/";

        [J("corporation_id")] public int CorporationId { get; set; }
        [J("is_deleted")] public bool? IsDeleted { get; set; }
        [J("record_id")] public int RecordId { get; set; }
        [J("start_date")] public string StartDate { get; set; }
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
        [J("type_id")] public int TypeID { get; set; }
        /// <summary>
        /// ID of the corporation that owns the structure
        /// </summary>
        [J("corporation_id")] public int CorporationID { get; set; }
        /// <summary>
        /// The solar system the structure is in
        /// </summary>
        [J("system_id")] public int SystemID;
        /// <summary>
        /// The id of the ACL profile for this citadel
        /// </summary>
        [J("profile_id")] public int ProfileID;
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
        [JsonIgnore] public static readonly string Endpoint = "/universe/stations/{station_id}/";

        [J("max_dockable_ship_volume")] public float MaxDockableShipVolume { get; set; }
        [J("name")] public string Name { get; set; }
        [J("office_rental_cost")] public float OfficeRentalCost { get; set; }
        [J("owner")] public int Owner { get; set; }
        [J("position")] public StructurePosition Position { get; set; }
        [J("race_id")] public int RaceId { get; set; }
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
}
