using System;
using Newtonsoft.Json;

namespace EntrepreneurEsiApi.Authentication {
    /// <summary>
    /// Extra information about the token.
    /// Result of GET https://esi.tech.ccp.is/verify/
    /// </summary>
    public class EsiTokenVerification
    {
        [JsonProperty("CharacterID")] public Int32 CharacterID { get; set; }
        [JsonProperty("CharacterName")] public string CharacterName { get; set; }
        [JsonProperty("CharacterOwnerHash")] public string CharacterOwnerHash { get; set; }
        [JsonProperty("ExpiresOn")] public string ExpiresOn { get; set; }
        [JsonProperty("Scopes")] public string Scopes { get; set; }
        [JsonProperty("TokenType")] public string TokenType { get; set; }
    }
}