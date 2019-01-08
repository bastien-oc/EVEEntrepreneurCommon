using System;
using System.Threading.Tasks;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Logging;
using EntrepreneurCommon.Models.EsiResponseModels;
using RestSharp;

namespace EntrepreneurCommon.Client
{
    /// <summary>
    /// Wrapper for EsiRestClient
    /// </summary>
    [Obsolete("Use IEsiRestClient instead", true)]
    public class EsiClient
    {
        public const     string           RestClientUrl = "https://esi.tech.ccp.is";
        private          EsiConfiguration _config;
        private readonly EsiRestClient    restClient;
        private readonly JsonSerializer   serializer = new JsonSerializer();
        private readonly ILogger             logger;

        public EsiClient(EsiConfiguration configuration,
                         IRestClient      restClient = null,
                         ILogger             logger     = null)
        {
            _config = configuration;
            this.logger = logger;
            if (restClient == null) {
                this.restClient = new EsiRestClient(configuration);
            }

            restClient = new EsiRestClient(configuration);

            restClient.AddHandler("application/json", serializer);
            restClient.AddHandler("text/json",        serializer);
            restClient.AddHandler("text/x-json",      serializer);
            restClient.AddHandler("text/javascript",  serializer);
            restClient.AddHandler("*+json",           serializer);
        }

        public IRestResponse Execute(IRestRequest request)
        {
            logger?.Debug($"Executing request {request}");
            return restClient.Execute(request);
        }

        public IRestResponse<T> Execute<T>(IRestRequest request) where T : new()
        {
            logger?.Debug($"Executing request {request}");
            return restClient.Execute<T>(request);
        }

        public async Task<IRestResponse> ExecuteTaskAsync(IRestRequest request)
        {
            logger?.Debug($"Executing request {request}");
            return await restClient.ExecuteTaskAsync(request);
        }

        public async Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request) where T : new()
        {
            logger?.Debug($"Executing request {request}");
            return await restClient.ExecuteTaskAsync<T>(request);
        }

        public EsiPaginatedResponse<T> ExecutePaginated<T>(IRestRequest request) where T : new()
        {
            logger?.Debug($"Executing request {request}");
            return restClient.ExecutePaginated<T>(request);
        }

        public T ParseResponse<T>(IRestResponse response)
        {
            return restClient.ParseResponse<T>(response);
        }

        public Uri BuildUri(IRestRequest request)
        {
            return restClient.BuildUri(request);
        }
    }
}
