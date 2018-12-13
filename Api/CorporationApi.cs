using System;
using EntrepreneurCommon.Client;
using EntrepreneurCommon.ExtensionMethods;
using EntrepreneurCommon.Helpers;
using EntrepreneurCommon.Models.EsiResponseModels;
using RestSharp;

namespace EntrepreneurCommon.Api
{
    public class CorporationApi : CommonApi
    {
        public CorporationApi(IEsiRestClient client) : base(client)
        {
            this.Client = client;
        }

        public IRestResponse<CorporationPublicInformation> GetPublicInformation(Int32 id)
        {

            var request = RequestHelper.GetRestRequest<CorporationPublicInformation>().SetCorporationId(id);
            return Client.Execute<CorporationPublicInformation>(request);
        }

        public IRestResponse<AlliancePublicInformationModel> GetAllianceInformation(Int32 id)
        {
            var request = RequestHelper.GetRestRequest<AlliancePublicInformationModel>()
                                       .AddParameter("alliance_id", id, ParameterType.QueryString);
            return Client.Execute<AlliancePublicInformationModel>(request);
        }
    }
}
