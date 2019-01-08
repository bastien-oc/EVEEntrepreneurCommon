using System;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Authentication
{
    /// <summary>
    /// The token itself, containing AccessToken and RefreshToken.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class EsiTokenResponse : IEsiTokenResponse
    {
        public string AccessToken { get; set; }
        public Int32 ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public interface IEsiTokenResponse
    {
        [JsonProperty("access_token")]
        string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        Int32 ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        string RefreshToken { get; set; }

        [JsonProperty("token_type")]
        string TokenType { get; set; }
    }
}