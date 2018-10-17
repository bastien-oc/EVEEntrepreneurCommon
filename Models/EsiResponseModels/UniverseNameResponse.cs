using System;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.Esi
{
    /// <summary>
    /// Resolve a set of IDs to names and categories. Supported ID’s for resolving are: Characters, Corporations, Alliances, Stations, Solar Systems, Constellations, Regions, Types.
    /// </summary>
    public class UniverseNameResponse
    {
        [JsonIgnore] public static readonly String Endpoint = "/v2/universe/names/";

        [J("id")] public Int32 ID { get; set; }
        [J("name")] public string Name { get; set; }
        [J("category")] public string Category { get; set; }
    }
}
