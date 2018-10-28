using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    public class LocationLocation
    {
        public static readonly string Endpoint = "/v1/characters/{character_id}/location/";
        public static readonly string Scope = "esi-location.read_location.v1";

        [J("solar_system_id")] public int SolarSystemId { get; set; }
        [J("station_id")] public int? StationId { get; set; }
        [J("structure_id")] public long? StructureId { get; set; }
    }
}
