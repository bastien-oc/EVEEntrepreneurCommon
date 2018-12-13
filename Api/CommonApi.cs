using EntrepreneurCommon.Client;
using RestSharp;

namespace EntrepreneurCommon.Api
{
    public class CommonApi
    {
        public IEsiRestClient Client { get; set; }
        //public RestClient restClient { get => ApiClient.RestClient; }
        public CommonApi( IEsiRestClient client ) => Client = client;
    }
}
