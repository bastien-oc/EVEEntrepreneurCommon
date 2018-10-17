using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurCommon.Authentication;

namespace EntrepreneurCommon.Util
{
    public partial class TokenManager
    {
        public string GetCharacterName(int id)
        {
            foreach (var t in Tokens) {
                if (t.CharacterId == id) {
                    return t.CharacterName;
                }
            }
            return null;
        }

        public int GetCharacterId(string name)
        {
            foreach (var t in Tokens) {
                if (String.Equals(name, t.CharacterName, StringComparison.OrdinalIgnoreCase)) {
                    return t.CharacterId;
                }
            }
            return -1;
        }

        public string GetCorporationName(int id)
        {
            return NamesCache.GetName(id);
        }

        public int GetCorporationId(string name)
        {
            var result = from r in NamesCache.NamesCache
                         where r.Name == name
                         select r;
            return result.First().ID;
        }

        /// <summary>
        /// Returns a list of unique character names assigned to the tokens.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<String> GetCharacterNameList()
        {
            List<String> characters = new List<String>();
            foreach (var t in Tokens) {
                if (!characters.Contains(t.CharacterName)) {
                    characters.Add(t.CharacterName);
                    yield return t.CharacterName;
                }
            }
        }
        /// <summary>
        /// Returns a list of unique corporation names assigned to the tokens.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<String> GetCorporationNameList()
        {
            List<String> corporations = new List<string>();
            foreach (var t in Tokens) {
                var corpName = GetCorporationName(t.CorporationId);
                if (!corporations.Contains(corpName)) {
                    corporations.Add(corpName);
                    yield return corpName;
                }
            }
        }
        /// <summary>
        /// Returns a list of unique character IDs assigned to the token.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Int32> GetCharacterIdList()
        {
            List<Int32> characters = new List<Int32>();
            foreach (var t in Tokens) {
                if (!characters.Contains(t.CharacterId)) {
                    characters.Add(t.CharacterId);
                    yield return t.CharacterId;
                }
            }
        }
        /// <summary>
        /// Returns a list of unique corporation IDs assigned to the token.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Int32> GetCorporationIdList()
        {
            List<Int32> corporations = new List<Int32>();
            foreach (var t in Tokens) {
                if (!corporations.Contains(t.CorporationId)) {
                    corporations.Add(t.CorporationId);
                    yield return t.CorporationId;
                }
            }
        }

        public IEnumerable<EsiTokenInfo> GetTokensForCharacter( int CharacterID )
        {
            foreach (var t in Tokens) {
                if (t.CharacterId == CharacterID)
                    yield return t;
            }
        }

        public IEnumerable<EsiTokenInfo> GetTokensForCharacter( string CharacterName )
        {
            foreach (var t in Tokens) {
                if (t.CharacterName == CharacterName)
                    yield return t;
            }
        }

        public EsiTokenInfo GetTokenWithScope(int CharacterId, string scope)
        {
            return Tokens.FirstOrDefault(x => x.CharacterId == CharacterId && x.CheckScope(scope));
        }
    }
}
