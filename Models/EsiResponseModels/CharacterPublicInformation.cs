using System;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurEsiApi.Models.Esi
{
    public class CharacterPublicInformation
    {
        [JsonIgnore] public static string Endpoint { get => "/characters/{character_id}/"; }
        [JsonIgnore] public static string EndpointVersioned { get => "/v4/characters/{character_id}/"; }

        [J("alliance_id")] public Int32 AllianceID { get; set; }
        [J("ancestry_id")] public Int32 AncestryID { get; set; }
        [J("birthday")] public string Birthday { get; set; }
        [J("bloodline_id")] public Int32 BloodlineID { get; set; }
        [J("corporation_id")] public Int32 CorporationID { get; set; }
        [J("description")] public string Description { get; set; }
        [J("gender")] public string Gender { get; set; }
        [J("name")] public string Name { get; set; }
        [J("race_id")] public Int32 RaceId { get; set; }
        [J("security_status")] public double SecurityStatus { get; set; }
    }
}
