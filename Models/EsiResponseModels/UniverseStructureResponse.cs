using System;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models
{
    public class StructurePosition
    {
        [J("x")] public float X { get; set; }
        [J("y")] public float Y { get; set; }
        [J("z")] public float Z { get; set; }
    }

    /// <summary>
    /// Returns information on requested structure, if you are on the ACL. Otherwise, returns "Forbidden" for all inputs.
    /// Endpoint: /universe/structures/{structure_id}/
    /// </summary>
    public class UniverseStructureResponse
    {
        [JsonIgnore] public static readonly String Endpoint = "/v1/universe/structures/{structure_id}/";
        public long StructureId { get; set; }
        [J("name")] public string Name { get; set; }
        [J("solar_system_id")] public Int32 SolarSystemID { get; set; }
        [J("type_id")] public Int32 TypeID { get; set; }
        [J("position")] public StructurePosition position { get; set; }
    }
}
