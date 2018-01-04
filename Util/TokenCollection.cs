using System.Collections;
using System.Collections.Generic;
using EntrepreneurEsiApi.Authentication;

namespace EntrepreneurEsiApi.Util
{
    public class TokenCollection:IEnumerable
    {
        public List<EsiTokenInfo> Tokens;

        public TokenCollection()
        {
            Tokens = new List<EsiTokenInfo>();
        }

        public IEnumerator GetEnumerator() => ((IEnumerable)Tokens).GetEnumerator();
    }

}
