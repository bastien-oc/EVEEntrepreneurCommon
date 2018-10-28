using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntrepreneurCommon.Common
{
    /// <summary>
    /// Marks the class as being mapped as a response to a specific endpoint.
    /// </summary>
    public interface IEsiEndpoint { }

    public static class EsiResponseExtension
    {
        public static string GetEndpointUrl<T>(this T response) where T : IEsiEndpoint
        {
            var type = response.GetType();
            var info = (EsiEndpointAttribute) type.GetCustomAttribute(typeof(EsiEndpointAttribute));
            if ( info != null )
            {
                return info.Endpoint;
            } else
            {
                throw new Exception(
                    $"The object {nameof(response)} is of type {response.GetType()} which does not implement attribut EsiEndpoint.");
            }
        }
    }
}
