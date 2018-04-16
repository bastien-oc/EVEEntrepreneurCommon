using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurEsiApi.Api;
using EntrepreneurEsiApi.Authentication;
using EntrepreneurEsiApi.Models.Esi;
using EntrepreneurEsiApi.Util.Entities;
using Newtonsoft.Json;

namespace EntrepreneurEsiApi.Util
{
    public partial class TokenManager
    {
        // Internals
        private EsiAuthClient authClient;

        // Data stores
        private List<EsiTokenInfo> _tokens = new List<EsiTokenInfo>(); // In case it is not created by loading from file.
        //private List<EveCharacter> _characters;
        //private List<EveCorporation> _corporations;
        public UniverseNameCache NamesCache = new UniverseNameCache();

        public EsiAuthClient AuthClient { get => authClient; }

        public List<EsiTokenInfo> Tokens { get => _tokens; set => _tokens = value; }
        //public List<EveCorporation> Corporations { get => _corporations; set => _corporations = value; }
        //public List<EveCharacter> Characters { get => _characters; set => _characters = value; }

        public TokenManager( EsiAuthClient esiAuthClient )
        {
            authClient = esiAuthClient;
        }

        public TokenManager( string clientId, string secretKey, string callbackUrl )
        {
            EsiAuthClient AuthClient = new EsiAuthClient(clientId, secretKey, callbackUrl);
        }

        public async void AddToken( string authorizationCode, bool firstTime )
        {
            // In case the list was not initialized (i.e.: not loaded, as it is the first key being added), create list.
            if (_tokens == null) { _tokens = new List<EsiTokenInfo>(); }
            EsiTokenInfo Token;
            Token = await AuthClient.GetFullToken(authorizationCode, firstTime);
            Token.AuthClient = AuthClient;
            _tokens.Add(Token);
        }

        public void LoadTokensFromFile( string filePath )
        {
            // If the file does not exist, return.
            if (!File.Exists(filePath)) { return; }
            string _content = File.ReadAllText(filePath);
            _tokens = JsonConvert.DeserializeObject<List<EsiTokenInfo>>(_content);
            if (_tokens == null) { return; }
            foreach (var t in _tokens) {
                t.AuthClient = AuthClient;
            }
        }

        public void SaveTokensToFile( string filePath )
        {
            // Make sure that the directory exists.
            string dirPath = Path.GetDirectoryName(filePath);
            Directory.CreateDirectory(dirPath);
            // Write as Json
            File.WriteAllText(filePath, JsonConvert.SerializeObject(_tokens));
        }



    }
}
