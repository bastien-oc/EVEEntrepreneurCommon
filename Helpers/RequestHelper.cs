using System;
using System.Reflection;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using RestSharp;

namespace EntrepreneurCommon.Helpers
{
    public static class RequestHelper
    {
        public static string GetEndpointUrl<T>() where T : IEsiResponseModel
        {
            var type = typeof(T);
            var info = (EsiEndpointAttribute) type.GetCustomAttribute(typeof(EsiEndpointAttribute));
            if (info != null) {
                return info.Endpoint;
            }
            else {
                throw new Exception(
                    $"The type {nameof(T)} does not implement attribut EsiEndpoint.");
            }
        }

        public static IRestRequest GetRestRequest<T>() where T : IEsiResponseModel
        {
            return new RestRequest(GetEndpointUrl<T>());
        }

        public static IRestRequest GetRestRequest<T>(string authorizationToken) where T : IEsiResponseModel
        {
            return String.IsNullOrEmpty(authorizationToken)
                ? GetRestRequest<T>()
                : GetRestRequest<T>()
                    .AddParameter("Authorization", $"Bearer {authorizationToken}",ParameterType.HttpHeader);
        }
    }
}