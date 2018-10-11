using System;
using System.Threading.Tasks;
using EntrepreneurCommon.Authentication;
using EntrepreneurEsiApi.Models.Esi;
using EntrepreneurEsiApi.Util;
using RestSharp;

namespace EntrepreneurEsiApi.Authentication
{
    // TODO Null checks for RefreshToken - happens when no scopes are selected, i.e: used for authentication only.
    public partial class EsiTokenInfo : IEsiTokenContainer
    {
        public EsiTokenInfo( IEsiTokenResponse AccessToken, IEsiTokenVerification TokenVerification )
        {
            this.tokenAccessInfo = AccessToken;
            this.tokenVerification = TokenVerification;
        }

        public EsiTokenInfo(IEsiTokenResponse AccessToken, IEsiTokenVerification TokenVerification, EsiAuthClient Client)
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
            if (this.RefreshToken == null) return;
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

        public async Task<string> GetAccessToken(EsiAuthClient client = null) => await GetToken();

        public EnumNeedsRefreshing NeedsRefreshing()
        {
            if (this.RefreshToken == null) return EnumNeedsRefreshing.Invalid;

            DateTime now = DateTime.Now;
            DateTime exp = DateTime.Parse(this.Expiry);

            switch (now > exp) {
                case true:
                    return EnumNeedsRefreshing.Yes;
                case false:
                    return EnumNeedsRefreshing.No;
                default:
                    return EnumNeedsRefreshing.No;
            }
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
