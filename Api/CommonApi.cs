using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace EntrepreneurEsiApi.Api
{
    public class CommonApi
    {
        public EsiApiClient ApiClient { get; set; }
        public RestClient restClient { get => ApiClient.RestClient; }
        public CommonApi( EsiApiClient apiClient ) => ApiClient = apiClient;
    }
}
