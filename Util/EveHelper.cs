using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace EntrepreneurCommon.Util
{
    public enum CacheType { CacheDate, CacheExpires, CacheLastModified }

    public static class EveHelper
    {
        public static DateTime GetCacheTimer( IRestResponse restResponse, CacheType cacheType = CacheType.CacheExpires )
        {
            string header;
            switch (cacheType) {
                case CacheType.CacheDate:
                    header = "Date";
                    break;
                case CacheType.CacheExpires:
                    header = "Expires";
                    break;
                case CacheType.CacheLastModified:
                    header = "Last-Modified";
                    break;
                default:
                    header = "Expires";
                    break;
            }

            DateTime result = default;
            try {
                result = DateTime.Parse((string)restResponse.Headers?.Where(x => x.Name == header).FirstOrDefault().Value ?? "");
            }
            catch { }

            return result;
        }
    }
}
