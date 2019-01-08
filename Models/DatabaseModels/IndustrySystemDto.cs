using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Models.EsiResponseModels;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Models.DatabaseModels {
    /// <summary>
    ///     Database Transfer Object
    /// </summary>
    public class IndustrySystemDto
    {
        public IndustrySystemDto() { }

        public IndustrySystemDto(IndustrySystemModel apiModel)
        {
            SolarSystemId = apiModel.SolarSystemId;
            CostIndices = apiModel.CostIndices;
        }

        [Key]
        [Column("solar_system_id")] public  int    SolarSystemId { get; set; }
        [Column("cost_indices")]    private string costIndices   { get; set; }

        [NotMapped]
        public IEnumerable<CostIndexModel> CostIndices {
            get => JsonConvert.DeserializeObject<List<CostIndexModel>>(costIndices);
            set => costIndices = JsonConvert.SerializeObject(value);
        }
    }
}