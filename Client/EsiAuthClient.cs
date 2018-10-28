using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Web;
using EntrepreneurCommon.Authentication;
using Newtonsoft.Json;
using RestSharp;

namespace EntrepreneurCommon.Client
{
    public partial class EsiAuthClient
    {
        private readonly RestClient restClient;

        private readonly string pathAuthorize = "oauth/authorize";
        private readonly string pathToken = "oauth/token";
        private readonly string pathVerify = "oauth/verify";
        private readonly string pathRevoke = "oauth/revoke";

        public string ClientId { get; }
        public string CallbackUrl { get; }
        public string SecretKey { get; }

        public string ClientAuthorization => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{SecretKey}"));

        public string BaseUrl { get; set; } = "https://login.eveonline.com";
        public string HostName { get; set; } = "login.eveonline.com";
        public string UserAgent { get; set; }
    }

    public partial class EsiAuthClient
    {
        public EsiAuthClient(string clientId, string secretKey, string callbackUrl)
        {
            this.ClientId = clientId;
            this.SecretKey = secretKey;
            this.CallbackUrl = callbackUrl;

#pragma warning disable 612
            _client = new HttpClient();
#pragma warning restore 612
            restClient = new RestClient(BaseUrl);
        }

        public string GetRedirectUrl(string scopes, string state = null)
        {
            NameValueCollection _q = HttpUtility.ParseQueryString("");
            _q["response_type"] = "code";
            _q["redirect_uri"] = CallbackUrl;
            _q["client_id"] = ClientId;
            _q["scope"] = scopes;
            if (state != null)
                _q["state"] = state;

            var builder = new UriBuilder(BaseUrl) {
                Path = pathAuthorize,
                Query = _q.ToString()
            };

            return builder.ToString();
        }

        public EsiTokenContainer GetTokenComposite(string tokenCodeStr, TokenAuthenticationType authType)
        {
            var tokenResponse = RequestAccessToken(tokenCodeStr, authType);
            var verification = RequestTokenVerification(tokenResponse.AccessToken);
            return new EsiTokenContainer(tokenResponse, verification, this);
        }

        public IEsiTokenResponse RequestAccessToken(string tokenCode, TokenAuthenticationType tokenType)
        {
            string body = "";

            switch (tokenType) {
                case TokenAuthenticationType.VerifyAuthCode:
                    body = JsonConvert.SerializeObject(new {
                        grant_type = "authorization_code",
                        code = tokenCode
                    });
                    break;
                case TokenAuthenticationType.RefreshToken:
                    body = JsonConvert.SerializeObject(new {
                        grant_type = "refresh_token",
                        refresh_token = tokenCode
                    });
                    break;
            }

            var request = new RestRequest(pathToken, Method.POST, DataFormat.Json);
            request.AddHeader("Authorization", $"Basic {ClientAuthorization}");
            request.AddHeader("Host", HostName);
            request.AddHeader("User-Agent", UserAgent);
            request.AddBody(body);

            var response = restClient.Execute<EsiTokenResponse>(request);
            if (!response.IsSuccessful) {
                throw new Exception(
                    $"Error has occured while requesting token from the server. {response.StatusCode} - {response.Content}");
            }

            return response.Data;
        }

        /// <summary>
        /// Request Verification data from an Access Token. Includes Refresh token.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public IEsiTokenVerification RequestTokenVerification(string accessToken)
        {
            var request = new RestRequest(pathVerify);
            request.AddHeader("User-Agent", UserAgent);
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            request.AddHeader("Host", HostName);
            var response = restClient.Execute<EsiTokenVerification>(request);
            if (!response.IsSuccessful)
                throw new Exception(
                    $"Error has occured while requesting token verification from the server. {response.StatusCode}: {response.Content}");
            return response.Data;
        }

        public bool RevokeToken(TokenAuthenticationType tokenType, string token)
        {
            // "https://login.eveonline.com/oauth/revoke"
            //var request = new HttpRequestMessage() {
            //    RequestUri = builder.Uri,
            //    Method = HttpMethod.Post
            //};

            var request = new RestRequest(pathRevoke, Method.POST);
            request.AddHeader("User-Agent", UserAgent);
            request.AddHeader("Authorization", $"Basic {ClientAuthorization}");
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Host", HostName);
            //request.Headers.Add("token");
            var hint = "";
            switch (tokenType) {
                case TokenAuthenticationType.AccessToken:
                    hint = "access_token";
                    break;
                case TokenAuthenticationType.RefreshToken:
                    hint = "refresh_token";
                    break;
                case TokenAuthenticationType.VerifyAuthCode:
                    throw new Exception("Authorization token is not a valid token for revocation.");
            }

            string body = $"token_type_hint={hint}&token={token}";
            var bodyj = new {token_type_hint = hint, token = token};
            request.AddJsonBody(JsonConvert.SerializeObject(bodyj));

            var client = new RestClient(BaseUrl);
            var response = client.Execute(request);


            if (!response.IsSuccessful) {
                throw new Exception(
                    $"[{pathRevoke}] Error has occured while revoking a token {token}: {response.StatusCode} - {response.Content}<br/>{body}");
            }

            return true;
        }

        public EnumNeedsRefreshing TokenNeedsRefreshing(IEsiTokenContainer token)
        {
            if (string.IsNullOrEmpty(token.RefreshToken)) {
                return EnumNeedsRefreshing.Invalid;
            }

            var now = DateTime.UtcNow;
            var exp = DateTime.Parse(token.ExpiresOn);

            if (exp > now) // Expiry date is in the future
                return EnumNeedsRefreshing.No;
            if (exp < now) // Expiry date is in the past
                return EnumNeedsRefreshing.Yes;

            return EnumNeedsRefreshing.Invalid; // If we don't know for sure, just say invalid.
        }

        /// <summary>
        /// Gets the up-to-date access token by refreshing the token if required.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string GetAccessToken(IEsiTokenContainer token)
        {
            RefreshToken(token);
            return token.AccessToken;
        }

        public void RefreshToken(IEsiTokenContainer token)
        {
            if (TokenNeedsRefreshing(token) != EnumNeedsRefreshing.Yes)
                return;

            var access = RequestAccessToken(token.RefreshToken, TokenAuthenticationType.RefreshToken);
            var verify = RequestTokenVerification(access.AccessToken);

            AssignTokenResponse(token, access);
            AssignTokenVerification(token, verify);
        }

        public bool CheckScope(IEsiTokenVerification token, string scope)
        {
            if (token.Scopes.ToLower().Contains(scope.ToLower())) return true;
            return false;
        }

        public bool CheckScope(IEsiTokenVerification token, params string[] scope)
        {
            // Set to false if ANY scope listed doesn't apply to the token.
            foreach (var s in scope) {
                if (!token.Scopes.Contains(s))
                    return false;
            }
            // All scopes were accounted for in the token, return true.
            return true;
        }

        private void AssignTokenResponse(IEsiTokenResponse token, IEsiTokenResponse response)
        {
            token.AccessToken = response.AccessToken;
            token.ExpiresIn = response.ExpiresIn;
            token.RefreshToken = response.RefreshToken;
            token.TokenType = response.TokenType;
        }

        private void AssignTokenVerification(IEsiTokenVerification token, IEsiTokenVerification response)
        {
            token.CharacterId = response.CharacterId;
            token.CharacterName = response.CharacterName;
            token.CharacterOwnerHash = response.CharacterOwnerHash;
            token.ExpiresOn = response.ExpiresOn;
            token.Scopes = response.Scopes;
            token.TokenType = response.TokenType;
        }
    }
}