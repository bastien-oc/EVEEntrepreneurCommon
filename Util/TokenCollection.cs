using System.Collections;
using System.Collections.Generic;
using EntrepreneurCommon.Authentication;

namespace EntrepreneurCommon.Util
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
