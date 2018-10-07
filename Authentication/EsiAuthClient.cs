using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace EntrepreneurEsiApi.Authentication
{
    public partial class EsiAuthClient
    {
        private HttpClient _client;

        private string clientId;
        private string secretKey;
        private string callbackUrl;

        private string baseUrl = "https://login.eveonline.com";
        private string hostName = "login.eveonline.com";

        private readonly string pathAuthorize = "oauth/authorize";
        private readonly string pathToken = "oauth/token";
        private readonly string pathVerify = "oauth/verify";

        public string ClientId { get => clientId; }
        public string CallbackUrl { get => callbackUrl; }

        public string ClientAuthorization { get => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{secretKey}")); }

        public string BaseUrl { get => baseUrl; set => baseUrl = value; }
        public string HostName { get => hostName; set => hostName = value; }
        public string UserAgent { get; set; }
    }

    public partial class EsiAuthClient
    {
        public EsiAuthClient( string clientId, string secretKey, string callbackUrl )
        {
            this.clientId = clientId;
            this.secretKey = secretKey;
            this.callbackUrl = callbackUrl;

            _client = new HttpClient();
        }

        public string GetRedirectUrl(string scopes)
        {
            NameValueCollection _q = HttpUtility.ParseQueryString("");
            _q["response_type"] = "code";
            _q["redirect_uri"] = CallbackUrl;
            _q["client_id"] = ClientId;
            _q["scope"] = scopes;

            var builder = new UriBuilder(BaseUrl) {
                Path = pathAuthorize,
                Query = _q.ToString()
            };

            return builder.ToString();
        }

        public void RedirectToLogin( string scopes )
        {
            NameValueCollection _q = HttpUtility.ParseQueryString("");
            _q["response_type"] = "code";
            _q["redirect_uri"] = CallbackUrl;
            _q["client_id"] = ClientId;
            _q["scope"] = scopes;

            var builder = new UriBuilder(BaseUrl) {
                Path = pathAuthorize,
                Query = _q.ToString()
            };

            System.Diagnostics.Process.Start(builder.ToString());
        }

        public enum TokenAuthenticationType { VerifyAuthCode, RefreshToken }

        /// <summary>
        /// One-click authorization for NEW tokens. This function is used only for the first authorization.
        /// </summary>
        /// <param name="authorizationCode"></param>
        /// <param name="firstTime">True if you're generating new token from SSO-provided Code param. False if you're using refreshToken.</param>
        /// <returns></returns>
        public async Task<EsiTokenInfo> GetFullToken( string authorizationCode, bool firstTime /*= false*/ )
        {
            var Token = await RequestAccessToken(authorizationCode, firstTime);
            var Verification = await RequestTokenVerification(Token.AccessToken);
            // Create token composite with reference to self (EsiAuthClient) to allow tokens self-refresh functionality.
            return new EsiTokenInfo(Token, Verification, this);
        }

        /// <summary>
        /// One-call method for 
        /// </summary>
        /// <param name="tokenCodeStr"></param>
        /// <param name="authType"></param>
        /// <returns></returns>
        public async Task<EsiTokenInfo> GetFullToken(string tokenCodeStr, TokenAuthenticationType authType)
        {
            bool isAuthorizationCode;
            if (authType == TokenAuthenticationType.VerifyAuthCode) isAuthorizationCode = true;
            else isAuthorizationCode = false;

            var Token = await RequestAccessToken(tokenCodeStr, isAuthorizationCode);
            var Verification = await RequestTokenVerification(Token.AccessToken);
            // Create token composite with reference to self (EsiAuthClient) to allow tokens self-refresh functionality.
            return new EsiTokenInfo(Token, Verification, this);
        }

        /// <summary>
        /// Request Access Token from either a single-use Authorization Code or refreshable Refresh Token
        /// https://eveonline-third-party-documentation.readthedocs.io/en/latest/sso/authentication.html
        /// https://eveonline-third-party-documentation.readthedocs.io/en/latest/sso/refreshtokens.html
        /// </summary>
        /// <param name="tokenCode"></param>
        /// <param name="isAuthorizationCode"></param>
        /// <returns></returns>
        public async Task<EsiTokenResponse> RequestAccessToken( string tokenCode, bool isAuthorizationCode = false )
        {
            // If we're requesting token from Authorization Code (first time use), make sure we use appropriate 'grant_type'. Getting token from reusable RefreshToken uses different 'grant_type.
            NameValueCollection query = HttpUtility.ParseQueryString("");
            if (isAuthorizationCode) {
                query["grant_type"] = "authorization_code";
                query["code"] = tokenCode;
            } else {
                query["grant_type"] = "refresh_token";
                query["refresh_token"] = tokenCode;
            }
            string _query = query.ToString();

            // We now build the URL based for the given use-case. 
            var builder = new UriBuilder(baseUrl) {
                Path = pathToken,
                Query = _query
            };

            // Define request
            var request = new HttpRequestMessage() {
                RequestUri = builder.Uri,
                Method = HttpMethod.Post
            };

            // Add required headers for authorization. Without them, the process will fail.
            request.Headers.Add("Authorization", $"Basic {ClientAuthorization}");
            request.Headers.Add("Host", hostName);
            request.Headers.Add("User-Agent", UserAgent);

            var response = await _client.SendAsync(request);
            var response_string = await response.Content.ReadAsStringAsync();


            // If response.IsSuccessStatusCode is false, something went wrong. Throw new exception.
            if (!response.IsSuccessStatusCode) {
                // Let us know that something went wrong.
                throw new Exception($"Error has occured while requesting token from the server. Details: {response.StatusCode.ToString()} {response_string}");
            }

            EsiTokenResponse Token = JsonConvert.DeserializeObject<EsiTokenResponse>(response_string);
            return Token;
        }

        /// <summary>
        /// Obtain Token Verification information
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<EsiTokenVerification> RequestTokenVerification( string accessToken )
        {
            var builder = new UriBuilder(baseUrl) {
                Path = pathVerify,
                Query = ""
            };

            var request = new HttpRequestMessage() {
                RequestUri = builder.Uri,
                Method = HttpMethod.Get
            };

            request.Headers.Add("User-Agent", UserAgent);
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            request.Headers.Add("Host", HostName);

            var response = await _client.SendAsync(request);
            var response_string = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode) {
                // Let us know that something went wrong.
                throw new Exception($"Error has occured while requesting token verification from the server. Details: {response_string}");
            }

            EsiTokenVerification TokenVerification = JsonConvert.DeserializeObject<EsiTokenVerification>(response_string);
            return TokenVerification;
        }
    }
}
