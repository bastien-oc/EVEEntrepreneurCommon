using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurEsiApi.Authentication;

namespace EntrepreneurCommon.Authentication {
    public class EsiTokenComposite : IEsiTokenResponse, IEsiTokenVerification {
        public int CharacterID { get; set; }
        public string CharacterName { get; set; }
        public string CharacterOwnerHash { get; set; }
        public string ExpiresOn { get; set; }
        public string Scopes { get; set; }
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }

        public static EsiTokenComposite operator +(EsiTokenComposite left, IEsiTokenResponse right)
        {
            left.AccessToken = right.AccessToken;
            left.ExpiresIn = right.ExpiresIn;
            left.RefreshToken = right.RefreshToken;
            left.TokenType = right.TokenType;
            return left;
        }

        public static EsiTokenComposite operator +(EsiTokenComposite left, IEsiTokenVerification right)
        {
            left.CharacterID = right.CharacterID;
            left.CharacterName = right.CharacterName;
            left.CharacterOwnerHash = right.CharacterOwnerHash;
            left.ExpiresOn = right.ExpiresOn;
            left.Scopes = right.Scopes;
            left.TokenType = right.TokenType;
            return left;
        }
    }
}
