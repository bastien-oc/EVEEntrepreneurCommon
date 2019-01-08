using System;
using System.Reflection;

namespace EntrepreneurCommon.Common.Attributes
{
    public class EsiEndpointAttribute : Attribute
    {
        public EsiEndpointAttribute(string endpoint, bool isPaginated = false, string[] scopes = null)
        {
            Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            IsPaginated = isPaginated;
            ScopesRequired = scopes ?? default;
        }

        // public EsiEndpointAttribute(string endpoint, bool isPaginated = false, string scope = null)
        // {
        //     Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
        //     IsPaginated = isPaginated;
        //     ScopesRequired = scope == null ? default : new[] {scope};
        // }

        public string   Endpoint       { get; set; }
        public bool     IsPaginated    { get; set; }
        public string[] ScopesRequired { get; set; }

        public static string GetEndpointUrl(object data)
        {
            var type = data.GetType();
            var info = (EsiEndpointAttribute) type.GetCustomAttribute(typeof(EsiEndpointAttribute));
            if (info != null) {
                return info.Endpoint;
            }

            throw new Exception(
                                $"The object {nameof(data)} is of type {data.GetType()} which does not implement attribut EsiEndpoint.");
        }
    }
}
