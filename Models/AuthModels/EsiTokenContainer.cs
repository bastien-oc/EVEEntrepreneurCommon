using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.ExtensionMethods;

namespace EntrepreneurCommon.Authentication
{
    /// <inheritdoc />
    /// <summary>
    /// A wrapper class containing all required fields and properties to perform token operations.
    /// Purpose of this class is the ability to store it persistently in a database or file, using it as a heart of information.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class EsiTokenContainer : IEsiTokenContainer
    {
    #region IEsiTokenResponse, IEsiTokenVerification

        [DisplayName("Character ID")]          public int            CharacterId        { get; set; }
        [DisplayName("Character Name")]        public string         CharacterName      { get; set; }
        [DisplayName("Owner Hash")]            public string         CharacterOwnerHash { get; set; }
        [DisplayName("Expiry Date")]           public DateTime ExpiresOn          { get; set; }
        [DisplayName("Scopes")]                public string         Scopes             { get; set; }
        [DisplayName("Token Type")]            public string         TokenType          { get; set; }
        [DisplayName("Access Token (Stored)")] public string         AccessToken        { get; set; }
        [DisplayName("Expires in...")]         public int            ExpiresIn          { get; set; }
        [DisplayName("Refresh Token")]         public string         RefreshToken       { get; set; }

    #endregion

    #region Supplementary properties

        [DisplayName("Expires in..."), NotMapped, JsonIgnore]
        public TimeSpan ExpiresInAuto {
            get {
                var span = (ExpiresOn - DateTime.UtcNow);
                if (span.TotalSeconds > 0)
                    return span;
                else
                    return default;
            }
        }

        [DisplayName("Requires refreshing?"), NotMapped, JsonIgnore]
        public EnumNeedsRefreshing RequiresRefreshing => NeedsRefreshing();

    #endregion

        public EsiTokenContainer() { }

        public EsiTokenContainer(IEsiTokenResponse tokenResponse, IEsiTokenVerification tokenVerification = null)
        {
            if (tokenResponse == null) return;
            // Get all the important data.
            this.AccessToken = tokenResponse.AccessToken;
            this.ExpiresIn = tokenResponse.ExpiresIn;
            this.RefreshToken = tokenResponse.RefreshToken;
            this.TokenType = tokenResponse.TokenType;

            if (tokenVerification != null) {
                this.CharacterId = tokenVerification.CharacterId;
                this.CharacterName = tokenVerification.CharacterName;
                this.CharacterOwnerHash = tokenVerification.CharacterOwnerHash;
                this.ExpiresOn = tokenVerification.ExpiresOn;
                this.Scopes = tokenVerification.Scopes;
                this.TokenType = tokenVerification.TokenType;
            }
        }


        public static EsiTokenContainer operator +(EsiTokenContainer left, IEsiTokenResponse right)
        {
            left.AssignTokenResponse(right);
            return left;
        }

        public static EsiTokenContainer operator +(EsiTokenContainer left, IEsiTokenVerification right)
        {
            left.AssignTokenVerification(right);
            return left;
        }


        public EnumNeedsRefreshing NeedsRefreshing()
        {
            if (this.RefreshToken == null) return EnumNeedsRefreshing.Invalid;

            DateTime now = DateTime.UtcNow;
            DateTime exp = ExpiresOn;

            switch (now > exp) {
                case true:
                    return EnumNeedsRefreshing.Yes;
                case false:
                    return EnumNeedsRefreshing.No;
                default:
                    return EnumNeedsRefreshing.No;
            }
        }
    }
}
