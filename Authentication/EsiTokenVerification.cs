using System;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Authentication
{
    /// <summary>
    /// Extra information about the token.
    /// Result of GET https://esi.tech.ccp.is/verify/
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class EsiTokenVerification : IEsiTokenVerification
    {
        public Int32 CharacterId { get; set; }
        public string CharacterName { get; set; }
        public string CharacterOwnerHash { get; set; }
        public string ExpiresOn { get; set; }
        public string Scopes { get; set; }
        public string TokenType { get; set; }

        [Obsolete("This is a legacy property, use CharacterId instead. Change in case.", true)]
        public Int32 CharacterID {
            get => CharacterId;
            set => CharacterId = value;
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public interface IEsiTokenVerification
    {
        [JsonProperty("CharacterID")]
        Int32 CharacterId { get; set; }

        [JsonProperty("CharacterName")]
        string CharacterName { get; set; }

        [JsonProperty("CharacterOwnerHash")]
        string CharacterOwnerHash { get; set; }

        [JsonProperty("ExpiresOn")]
        string ExpiresOn { get; set; }

        [JsonProperty("Scopes")]
        string Scopes { get; set; }

        [JsonProperty("TokenType")]
        string TokenType { get; set; }
    }
}