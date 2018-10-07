using System;
using System.Threading.Tasks;
using EntrepreneurEsiApi.Models.Esi;
using EntrepreneurEsiApi.Util;
using Newtonsoft.Json;
using Nito.AsyncEx;
using RestSharp;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurEsiApi.Authentication
{
    public class EsiTokenVerification
    {
        [J("CharacterID")] public Int32 CharacterID { get; set; }
        [J("CharacterName")] public string CharacterName { get; set; }
        [J("CharacterOwnerHash")] public string CharacterOwnerHash { get; set; }
        [J("ExpiresOn")] public string ExpiresOn { get; set; }
        [J("Scopes")] public string Scopes { get; set; }
        [J("TokenType")] public string TokenType { get; set; }
    }

    public partial class EsiTokenResponse
    {
        [J("access_token")] public string AccessToken { get; set; }
        [J("expires_in")] public Int32 ExpiresIn { get; set; }
        [J("refresh_token")] public string RefreshToken { get; set; }
        [J("token_type")] public string TokenType { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public partial class EsiTokenInfo
    {
        // Main fields
        [J("Verificationinfo")]     private EsiTokenVerification tokenVerification;
        [J("TokenInfo")]            private EsiTokenResponse tokenAccessInfo;
        [J("Enabled")]              private bool _enabled = true;
                                    private CharacterPublicInformation characterInformation;
                                    private CharacterRolesModel characterRoles;

        // Workaround fields
        public EsiAuthClient AuthClient;

        //public EsiTokenVerification TokenVerification { get; set; }
        //public EsiTokenResponse TokenAccessInfo { get; set; }
        
        public Boolean Enabled { get => _enabled; set => _enabled = value; }

        // Forward TokenVerification Info
        public Int32 CharacterId { get => tokenVerification.CharacterID; }
        public string CharacterName { get => tokenVerification.CharacterName; }
        public string CharacterOwnerHash { get => tokenVerification.CharacterOwnerHash; }
        public string Expiry { get => tokenVerification.ExpiresOn; }
        public string Scopes { get => tokenVerification.Scopes; }
        public string TokenTypeVerification { get => tokenVerification.TokenType; }

        // Forward TokenResponse info
        public string RefreshToken { get => tokenAccessInfo.RefreshToken; }
        public string AccessToken { get => tokenAccessInfo.AccessToken; }
        public string TokenType { get => tokenAccessInfo.TokenType; }
        public Int32 ExpiresIn { get => tokenAccessInfo.ExpiresIn; }

        // Automatic token refreshing
        public string AccessTokenAuto { get => AsyncContext.Run(GetToken); }

        // Forward Character Info
        public Int32 CorporationId { get => CharacterInformation.CorporationID; }
        public Int32 AllianceId { get => CharacterInformation.AllianceID; }

        // Requestable info
        private IRestResponse<CharacterPublicInformation> characterInformationResponse { get; set; }
        private IRestResponse<CharacterRolesModel> characterRolesResponse { get; set; }

        [J("CharacterInformation")]     public CharacterPublicInformation CharacterInformation { get => GetCharacterInformation(); }
        [J("CharacterRoles")]           public CharacterRolesModel CharacterRoles { get => GetCharacterRoles(); }

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

    public partial class EsiTokenInfo
    {
        public EsiTokenInfo( EsiTokenResponse AccessToken, EsiTokenVerification TokenVerification )
        {
            this.tokenAccessInfo = AccessToken;
            this.tokenVerification = TokenVerification;
        }

        public EsiTokenInfo(EsiTokenResponse AccessToken, EsiTokenVerification TokenVerification, EsiAuthClient Client)
        {
            this.tokenAccessInfo = AccessToken;
            this.tokenVerification = TokenVerification;
            this.AuthClient = Client;
        }

        /// <summary>
        /// Fully refreshes the token's AccessToken and VerificationInfo fields.
        /// </summary>
        /// <returns></returns>
        public async Task Refresh()
        {
            DateTime now = DateTime.Now;
            DateTime exp = DateTime.Parse(Expiry);
            if (now < exp)
                return; // Token is not expired, no need to refresh it.

            tokenAccessInfo = await AuthClient.RequestAccessToken(tokenAccessInfo.RefreshToken);
            tokenVerification = await AuthClient.RequestTokenVerification(tokenAccessInfo.AccessToken);

            OnTokenUpdated?.Invoke();
        }

        /// <summary>
        /// Gets the AccessToken string, refreshing it if needed.
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetToken()
        {
            DateTime exp = DateTime.Parse(Expiry);
            DateTime now = DateTime.Now;
            if (now > exp) { await Refresh(); }
            return AccessToken;
        }


        public CharacterPublicInformation GetCharacterInformation()
        {
            // If exists AND cache has not expired, return existing entry.
            if (characterInformationResponse != null) {
                if (EveHelper.GetCacheTimer(characterInformationResponse) > DateTime.UtcNow) {
                    return characterInformation;
                }
            }

            RestClient Client = new RestClient("https://esi.tech.ccp.is");
            RestRequest request = new RestRequest(CharacterPublicInformation.EndpointVersioned, Method.GET);

            request.AddUrlSegment("character_id", this.CharacterId);
            characterInformationResponse = Client.Execute<CharacterPublicInformation>(request);

            characterInformation = characterInformationResponse.Data;

            OnTokenUpdated?.Invoke();
            return characterInformation;
        }

        public CharacterRolesModel GetCharacterRoles()
        {
            if (CheckScope("esi-characters.read_corporation_roles.v1") == false) {
                return null;
            }

            if (characterRolesResponse != null) {
                if (EveHelper.GetCacheTimer(characterRolesResponse) > DateTime.UtcNow) {
                    return characterRoles;
                }
            }

            RestClient Client = new RestClient("https://esi.tech.ccp.is");
            RestRequest request = new RestRequest(CharacterRolesModel.EndpointVersioned, Method.GET);

            request.AddUrlSegment("character_id", this.CharacterId);
            characterRolesResponse = Client.Execute<CharacterRolesModel>(request);
            characterRoles = characterRolesResponse.Data;

            OnTokenUpdated?.Invoke();
            return characterRoles;
        }

        public Boolean CheckScope( string scope )
        {
            if (this.tokenVerification.Scopes.Contains(scope))
                return true;
            else
                return false;
        }

        public Boolean CheckRole( RolesEnum role )
        {
            if (CharacterRoles == null)
                return false;

            if (CharacterRoles.Roles.Contains(role))
                return true;

            return false;
        }
    }
}
