using System;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.Esi
{
    public class IndustryCharacterJobResponse
    {

        public static string Endpoint { get => "/v1/characters/{character_id}/industry/jobs/"; }

        [J] public Int32 JobId { get; set; }
        [J] public Int32 InstallerId { get; set; }
        [J] public Int64 FacilityId { get; set; }
        [J] public Int32 ActivityId { get; set; }
        [J] public Int64 BlueprintId { get; set; }
        [J] public Int32 BlueprintTypeId { get; set; }
        [J] public Int64 BlueprintLocationId { get; set; }
        [J] public Int64 OutputLocationId { get; set; }
        [J] public Int32 Runs { get; set; }
        [J] public Double? Cost { get; set; }
        [J] public Int32? LicensedRuns { get; set; }
        [J] public float? Probability { get; set; }
        [J] public Int32 ProductTypeId { get; set; }
        [J] public String Status { get; set; }
        [J] public Int32 Duration { get; set; }
        [J] public DateTime StartDate { get; set; }
        [J] public DateTime EndDate { get; set; }
        [J] public DateTime? PauseDate { get; set; }
        [J] public DateTime? CompletedDate { get; set; }
        [J] public Int32? CompletedCharacterId { get; set; }
        [J] public Int32? SuccessfulRuns { get; set; }
    }
}
