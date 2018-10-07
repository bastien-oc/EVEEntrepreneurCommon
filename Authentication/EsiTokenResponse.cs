using System;
using Newtonsoft.Json;

namespace EntrepreneurEsiApi.Authentication {
    /// <summary>
    /// The token itself, containing AccessToken and RefreshToken.
    /// </summary>
    public partial class EsiTokenResponse
    {
        [JsonProperty("access_token")] public string AccessToken { get; set; }
        [JsonProperty("expires_in")] public Int32 ExpiresIn { get; set; }
        [JsonProperty("refresh_token")] public string RefreshToken { get; set; }
        [JsonProperty("token_type")] public string TokenType { get; set; }
    }
}