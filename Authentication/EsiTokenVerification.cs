using System;
using Newtonsoft.Json;

namespace EntrepreneurEsiApi.Authentication {
    /// <summary>
    /// Extra information about the token.
    /// Result of GET https://esi.tech.ccp.is/verify/
    /// </summary>
    public class EsiTokenVerification : IEsiTokenVerification
    {
        [JsonProperty("CharacterID")] public Int32 CharacterID { get; set; }
        [JsonProperty("CharacterName")] public string CharacterName { get; set; }
        [JsonProperty("CharacterOwnerHash")] public string CharacterOwnerHash { get; set; }
        [JsonProperty("ExpiresOn")] public string ExpiresOn { get; set; }
        [JsonProperty("Scopes")] public string Scopes { get; set; }
        [JsonProperty("TokenType")] public string TokenType { get; set; }
    }

    public interface IEsiTokenVerification {
        [JsonProperty("CharacterID")] Int32 CharacterID { get; set; }
        [JsonProperty("CharacterName")] string CharacterName { get; set; }
        [JsonProperty("CharacterOwnerHash")] string CharacterOwnerHash { get; set; }
        [JsonProperty("ExpiresOn")] string ExpiresOn { get; set; }
        [JsonProperty("Scopes")] string Scopes { get; set; }
        [JsonProperty("TokenType")] string TokenType { get; set; }
    }
}