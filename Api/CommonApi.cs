using RestSharp;

namespace EntrepreneurCommon.Api
{
    public class CommonApi
    {
        public EsiApiClient ApiClient { get; set; }
        public RestClient restClient { get => ApiClient.RestClient; }
        public CommonApi( EsiApiClient apiClient ) => ApiClient = apiClient;
    }
}
