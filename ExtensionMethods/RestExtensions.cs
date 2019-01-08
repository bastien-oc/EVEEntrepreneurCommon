using RestSharp;
using System;
using System.Linq;

namespace EntrepreneurCommon.ExtensionMethods
{
    public static class RestExtensions
    {
        public static DateTime GetCacheExpiry(this IRestResponse response) =>
            DateTime.Parse(response.Headers.FirstOrDefault(t => t.Name == "Expires")?.Value.ToString());

        public static IRestRequest SetToken(this IRestRequest request, string token) =>
            request.AddParameter("Authorization", $"Bearer {token}", ParameterType.HttpHeader);

        public static IRestRequest SetCharacterId(this IRestRequest request, int characterId) =>
            request.AddParameter("character_id", characterId, ParameterType.UrlSegment);

        public static IRestRequest SetCorporationId(this IRestRequest request, int corporationId) =>
            request.AddParameter("corporation_id", corporationId, ParameterType.UrlSegment);

        public static IRestRequest SetMethod(this IRestRequest request, Method method)
        {
            request.Method = method;
            return request;
        }

        public static IRestRequest SetDatasource(this IRestRequest request, string datasource)
        {
            if (
                (request.Parameters.Any(p => p.Name == "datasource") == false)
                )
            {
                request.AddParameter("datasource", datasource, ParameterType.QueryString);
            }

            return request;
        }
    }
}