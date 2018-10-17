using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurCommon.Models.EsiResponseModels;
using RestSharp;

namespace EntrepreneurCommon.Api
{
    public class CorporationApi : CommonApi
    {
        private EsiApiClient ApiClient { get; set; }

        public CorporationApi(EsiApiClient apiClient) : base(apiClient)
        {
            ApiClient = apiClient;
        }

        public IRestResponse<CorporationPublicInformation> GetPublicInformation(Int32 id)
        {
            var request = new RestRequest(CorporationPublicInformation.Route);
            request.AddParameter("corporation_id", id, ParameterType.UrlSegment);
            var response = ApiClient.Execute<CorporationPublicInformation>(request);
            return response;
        }
    }
}
