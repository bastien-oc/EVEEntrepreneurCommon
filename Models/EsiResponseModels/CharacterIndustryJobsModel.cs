using System;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels {
    [EsiEndpoint("/v1/characters/{character_id}/industry/jobs/", false, new[] {EsiCharacterScopes.IndustryJobsRead})]
    public class CharacterIndustryJobsModel : IEsiResponseModel {
        [J] public int     JobId                { get; set; }
        [J] public int     InstallerId          { get; set; }
        [J] public long     FacilityId           { get; set; }
        [J] public int     ActivityId           { get; set; }
        [J] public long     BlueprintId          { get; set; }
        [J] public int     BlueprintTypeId      { get; set; }
        [J] public long     BlueprintLocationId  { get; set; }
        [J] public long     OutputLocationId     { get; set; }
        [J] public int     Runs                 { get; set; }
        [J] public double?   Cost                 { get; set; }
        [J] public int?    LicensedRuns         { get; set; }
        [J] public float?    Probability          { get; set; }
        [J] public int     ProductTypeId        { get; set; }
        [J] public string    Status               { get; set; }
        [J] public int     Duration             { get; set; }
        [J] public DateTime  StartDate            { get; set; }
        [J] public DateTime  EndDate              { get; set; }
        [J] public DateTime? PauseDate            { get; set; }
        [J] public DateTime? CompletedDate        { get; set; }
        [J] public int?    CompletedCharacterId { get; set; }
        [J] public int?    SuccessfulRuns       { get; set; }
    }
}
