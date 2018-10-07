using System;
using EntrepreneurEsiApi.Models.Esi;
using Newtonsoft.Json;
using Nito.AsyncEx;
using RestSharp;

namespace EntrepreneurEsiApi.Authentication {
    /// <summary>
    /// Custom container class for an ESI token which composites the Access+Refresh Tokens with the Verification response for easy access to extra information. Also includes self-contained methods for retrieving additional, basic information, subject to personal definition.
    /// </summary>
    /// TODO: Rename the class to something more intuitive, like EsiTokenContainer or EsiTokenWrapper?
    [JsonObject(MemberSerialization.OptIn)]
    public partial class EsiTokenInfo
    {
        // Main fields
        [JsonProperty("Verificationinfo")]  private EsiTokenVerification tokenVerification;
        [JsonProperty("TokenInfo")]         private EsiTokenResponse tokenAccessInfo;
        [JsonProperty("Enabled")]           private bool _enabled = true;
                                            private CharacterPublicInformation characterInformation;
                                            private CharacterRolesModel characterRoles;

        /// <summary>
        /// Reference to the EsiAuthClient that was responsible for generating/refreshing the token. Assigned automatically when generating the token from the AuthClient, but needs to be manually re-assigned when loading the object from DataStore (i.e: when restarting the app and loading saved tokens).
        /// </summary>
        public EsiAuthClient AuthClient;

        //public EsiTokenVerification TokenVerification { get; set; }
        //public EsiTokenResponse TokenAccessInfo { get; set; }
        
        public Boolean Enabled { get => _enabled; set => _enabled = value; }

    #region Forward TokenVerification Info
        public Int32 CharacterId { get => tokenVerification.CharacterID; }
        public string CharacterName { get => tokenVerification.CharacterName; }
        public string CharacterOwnerHash { get => tokenVerification.CharacterOwnerHash; }
        public string Expiry { get => tokenVerification.ExpiresOn; }
        public string Scopes { get => tokenVerification.Scopes; }
        public string TokenTypeVerification { get => tokenVerification.TokenType; }
    #endregion

    #region Forward TokenResponse info
        public string RefreshToken { get => tokenAccessInfo.RefreshToken; }
        public string AccessToken { get => tokenAccessInfo.AccessToken; }
        public string TokenType { get => tokenAccessInfo.TokenType; }
        public Int32 ExpiresIn { get => tokenAccessInfo.ExpiresIn; }
    #endregion

        // Automatic token refreshing
        public string AccessTokenAuto { get => AsyncContext.Run(GetToken); }

        // Forward Character Info
        public Int32 CorporationId { get => CharacterInformation.CorporationID; }
        public Int32 AllianceId { get => CharacterInformation.AllianceID; }

        // Requestable info
        private IRestResponse<CharacterPublicInformation> characterInformationResponse { get; set; }
        private IRestResponse<CharacterRolesModel> characterRolesResponse { get; set; }

        [JsonProperty("CharacterInformation")]     public CharacterPublicInformation CharacterInformation { get => GetCharacterInformation(); }
        [JsonProperty("CharacterRoles")]           public CharacterRolesModel CharacterRoles { get => GetCharacterRoles(); }

        // Other
        public Action OnTokenUpdated;

        // Additional application-managable info
        public string CorporationName { get; set; }
        public string AllianceName { get; set; }

        public void OverrideTokenData( EsiTokenVerification tokenVerification, EsiTokenResponse tokenAccessInfo)
        {
            this.tokenVerification = tokenVerification;
            this.tokenAccessInfo = tokenAccessInfo;
        }
    }
}