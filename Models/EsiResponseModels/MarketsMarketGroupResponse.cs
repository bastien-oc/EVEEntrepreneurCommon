using System.Collections.Generic;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    public class MarketsMarketGroupResponse
    {
        public static string Endpoint { get => "/v1/markets/groups/{market_group_id}/"; }

        [J("market_group_id")] public int MarketGroupId {get;set;}
        [J("name")] public string Name { get; set; }
        [J("description")] public string Description { get; set; }
        [J("types")] public List<int> Types { get; set; }
        [J("parent_group_id")] public int? ParentGroupId { get; set; }
    }
}
