using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurEsiApi.Authentication;
using EntrepreneurEsiApi.Models.Esi;

namespace EntrepreneurEsiApi.Util.Entities
{
    public partial class EveEntity
    {
        public String Name;
        public Int32 Id;

        public List<EsiTokenInfo> Tokens = new List<EsiTokenInfo>();
        public CharacterPublicInformation CharacterInformation;
        public List<String> Grous = new List<string>();
    }

    public partial class EveEntity
    {
        public EveEntity( string name, int id )
        {
            this.Name = name;
            this.Id = id;
        }

        public void AssignTokensForCharacter( int characterId , EsiTokenInfo[] tokens)
        {
            foreach (var t in tokens) {
                if (t.CharacterId == characterId) {
                    Tokens.Add(t);
                }
            }
        }

        public void AssignTokensForCorporation( int corporationId, EsiTokenInfo[] tokens )
        {
            foreach (var t in tokens) {
                if (t.CorporationId == corporationId) {
                    Tokens.Add(t);
                }
            }
        }
    }
}
