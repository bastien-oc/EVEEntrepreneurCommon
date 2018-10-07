using System;
using Newtonsoft.Json;

namespace EntrepreneurEsiApi.Authentication {
    /// <summary>
    /// The token itself, containing AccessToken and RefreshToken.
    /// </summary>
    public class EsiTokenResponse: IEsiTokenResponse
    {
        [JsonProperty("access_token")] public string AccessToken { get; set; }
        [JsonProperty("expires_in")] public Int32 ExpiresIn { get; set; }
        [JsonProperty("refresh_token")] public string RefreshToken { get; set; }
        [JsonProperty("token_type")] public string TokenType { get; set; }
    }

    public interface IEsiTokenResponse
    {
        [JsonProperty("access_token")] string AccessToken { get; set; }
        [JsonProperty("expires_in")] Int32 ExpiresIn { get; set; }
        [JsonProperty("refresh_token")] string RefreshToken { get; set; }
        [JsonProperty("token_type")] string TokenType { get; set; }
    }
}