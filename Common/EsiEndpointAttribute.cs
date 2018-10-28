using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntrepreneurCommon.Common
{
    public class EsiEndpointAttribute : Attribute
    {
        public EsiEndpointAttribute(string endpoint, bool isPaginated = false, string[] scopes = null)
        {
            Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            IsPaginated = isPaginated;
            ScopesRequired = scopes ?? default;
        }

        public string Endpoint { get; set; }
        public Boolean IsPaginated { get; set; }
        public string[] ScopesRequired { get; set; }

        public static string GetEndpointUrl(object data)
        {
            var type = data.GetType();
            var info = (EsiEndpointAttribute) type.GetCustomAttribute(typeof(EsiEndpointAttribute));
            if (info != null) {
                return info.Endpoint;
            }
            else {
                throw new Exception(
                    $"The object {nameof(data)} is of type {data.GetType()} which does not implement attribut EsiEndpoint.");
            }
        }
    }
}