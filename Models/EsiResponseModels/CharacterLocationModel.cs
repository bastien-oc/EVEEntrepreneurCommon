using System.ComponentModel.DataAnnotations;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Models.EsiResponseModels {
    /// <summary>
    ///     Information about the characters current location. Returns the current solar system id, and also the current
    ///     station or structure ID if applicable.
    ///     Cache: Up to 5 seconds
    ///     Path: character_id
    /// </summary>
    [EsiEndpoint("/v1/characters/{character_id}/location/", false, new[] {EsiCharacterScopes.LocationLocationRead})]
    public class CharacterLocationModel : IEsiResponseModel {
        [Key] [RestParameterMapping]      public int  CharacterId   { get; set; }
        [JsonProperty("solar_system_id")] public int  SolarSystemId { get; set; }
        [JsonProperty("station_id")]      public int  StationId     { get; set; }
        [JsonProperty("structure_id")]    public long StructureId   { get; set; }
    }
}
