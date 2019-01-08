using System;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [Obsolete("This endpoint is obsolete", true)]
    public class CorporationMarketOrdersV1:EntityMarketOrdersV1
    {
        public static string Endpoint => "/v1/corporations/{corporation_id}/orders/";
    }
}
