using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    public class NameToIdResponse
    {
        [JsonIgnore] public static readonly string Endpoint = "/v1/universe/ids/";

        [J("agent")] public List<IdName> Agents { get; set; }
        [J("alliances")] public List<IdName> Alliances { get; set; }
        [J("characters")] public List<IdName> Characters { get; set; }
        [J("constellations")] public List<IdName> Constellations { get; set; }
        [J("corporations")] public List<IdName> Corporations { get; set; }
        [J("factions")] public List<IdName> Factions { get; set; }
        [J("inventorytypes")] public List<IdName> InventoryTypes { get; set; }
        [J("regions")] public List<IdName> Regions { get; set; }
        [J("solarsystems")] public List<IdName> SolarSystems { get; set; }
        [J("stations")] public List<IdName> Stations { get; set; }
    }

    public class IdName
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
    }

}


