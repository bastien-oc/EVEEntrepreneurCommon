using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurEsiApi.Models.Esi
{
    /// <summary>
    /// Return the Cost Indices for Solar Systems
    /// Endpoint: /industry/systems/
    /// </summary>
    public class IndustrySystemResponse
    {
        [JsonIgnore] public static readonly string Endpoint = "/v1/industry/systems/";

        [J("cost_indices")] public List<CostIndexModel> CostIndices { get; set; }
        [J("solar_system_id")] public Int32 SolarSystemId { get; set; }
    }

    public class CostIndexModel
    {
        [J("activity")] public string Activity { get; set; }
        [J("cost_index")] public double CostIndex { get; set; }
    }
}
