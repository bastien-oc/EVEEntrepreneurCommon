using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurCommon.Authentication;

namespace EntrepreneurCommon.ExtensionMethods
{
    public static class EsiTokenContainerExtensions
    {
        public static IEsiTokenContainer AssignTokenResponse(this IEsiTokenContainer token, IEsiTokenResponse response)
        {
            token.AccessToken = response.AccessToken;
            token.ExpiresIn = response.ExpiresIn;
            token.RefreshToken = response.RefreshToken;
            token.TokenType = response.TokenType;
            return token;
        }

        public static IEsiTokenContainer AssignTokenVerification(this IEsiTokenContainer token, IEsiTokenVerification response)
        {
            token.CharacterId = response.CharacterId;
            token.CharacterName = response.CharacterName;
            token.CharacterOwnerHash = response.CharacterOwnerHash;
            token.ExpiresOn = response.ExpiresOn;
            token.Scopes = response.Scopes;
            token.TokenType = response.TokenType;
            return token;
        }
        
        public static bool CheckScope(this IEsiTokenVerification token, string scope)
        {
            return string.IsNullOrEmpty(scope) || token.Scopes.ToLower().Contains(scope.ToLower());
        }
    }
}
