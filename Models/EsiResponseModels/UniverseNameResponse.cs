using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    /// <summary>
    /// Resolve a set of IDs to names and categories. Supported ID’s for resolving are: Characters, Corporations, Alliances, Stations, Solar Systems, Constellations, Regions, Types.
    /// </summary>
    [EsiEndpoint("/v2/universe/names/")]
    public class UniverseNameResponse : IEsiResponseModel
    {
        [Key]
        [Index]
        [J("id")]
        public int ID { get; set; }

        [Index]
        [J("name")]
        public string Name { get; set; }

        [J("category")]
        public string Category { get; set; }
    }
}