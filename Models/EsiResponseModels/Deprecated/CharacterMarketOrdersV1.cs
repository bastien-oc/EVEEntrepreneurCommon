using System;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [Obsolete("This endpoint is obsolete", true)]
    public class CharacterMarketOrdersV1:EntityMarketOrdersV1
    {
        public static string Endpoint => "/v1/characters/{character_id}/orders/";
    }
}
