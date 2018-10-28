using System;
using EntrepreneurCommon.Models.EsiResponseModels;
using RestSharp;

namespace EntrepreneurCommon.Api
{
    public class CorporationApi : CommonApi
    {
        // private EsiApiClient ApiClient { get; set; }

        public CorporationApi(EsiApiClient apiClient) : base(apiClient)
        {
            this.ApiClient = apiClient;
        }

        public IRestResponse<CorporationPublicInformation> GetPublicInformation(Int32 id)
        {
            var request = new RestRequest(CorporationPublicInformation.Route);
            request.AddParameter("corporation_id", id, ParameterType.UrlSegment);
            var response = this.ApiClient.Execute<CorporationPublicInformation>(request);
            return response;
        }
    }
}
