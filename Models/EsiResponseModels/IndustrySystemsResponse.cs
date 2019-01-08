using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Common;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    /// <summary>
    ///     Return the Cost Indices for Solar Systems
    ///     Endpoint: /industry/systems/
    /// </summary>
    [EsiEndpoint("/v1/industry/systems/")]
    public class IndustrySystemModel : IEsiResponseModel
    {
        [Key] [J("solar_system_id")] public int                  SolarSystemId { get; set; }
        [J(      "cost_indices")]    public List<CostIndexModel> CostIndices   { get; set; }
    }

    public class CostIndexModel
    {
        [J("activity")] public string Activity { get; set; }

        [J("cost_index")] public double CostIndex { get; set; }
    }
}
