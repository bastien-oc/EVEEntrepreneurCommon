using System;
using System.Collections.Generic;
using System.IO;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Client;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Util
{
    [Obsolete("Obsoleted with the introduction of TokenContainer")]
    public partial class TokenManager
    {
        // Internals
        private EsiAuthClient authClient;

        // Data stores
        private List<EsiTokenInfo> _tokens = new List<EsiTokenInfo>(); // In case it is not created by loading from file.
        //private List<EveCharacter> _characters;
        //private List<EveCorporation> _corporations;
        public UniverseNameCache NamesCache = new UniverseNameCache();

        public EsiAuthClient AuthClient => this.authClient;

        public List<EsiTokenInfo> Tokens { get => this._tokens; set => this._tokens = value; }
        //public List<EveCorporation> Corporations { get => _corporations; set => _corporations = value; }
        //public List<EveCharacter> Characters { get => _characters; set => _characters = value; }

        public TokenManager( EsiAuthClient esiAuthClient )
        {
            this.authClient = esiAuthClient;
        }

        public TokenManager(String clientId, String secretKey, String callbackUrl )
        {
            EsiAuthClient AuthClient = new EsiAuthClient(clientId, secretKey, callbackUrl);
        }

        public async void AddToken(String authorizationCode, Boolean firstTime )
        {
            // In case the list was not initialized (i.e.: not loaded, as it is the first key being added), create list.
            if ( this._tokens == null) { this._tokens = new List<EsiTokenInfo>(); }
            EsiTokenInfo Token;
            Token = await this.AuthClient.GetFullToken(authorizationCode, firstTime);
            Token.AuthClient = this.AuthClient;
            this._tokens.Add(Token);
        }

        public void LoadTokensFromFile(String filePath )
        {
            // If the file does not exist, return.
            if (!File.Exists(filePath)) { return; }
            String _content = File.ReadAllText(filePath);
            this._tokens = JsonConvert.DeserializeObject<List<EsiTokenInfo>>(_content);
            if ( this._tokens == null) { return; }
            foreach (var t in this._tokens ) {
                t.AuthClient = this.AuthClient;
            }
        }

        public void SaveTokensToFile(String filePath )
        {
            // Make sure that the directory exists.
            String dirPath = Path.GetDirectoryName(filePath);
            Directory.CreateDirectory(dirPath);
            // Write as Json
            File.WriteAllText(filePath, JsonConvert.SerializeObject(this._tokens));
        }



    }
}
