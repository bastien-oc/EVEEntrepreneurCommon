using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    [EsiEndpoint("/v1/markets/groups/{market_group_id}/")]
    public class MarketsMarketGroupResponse : IEsiResponseModel
    {
        [J("market_group_id")] [Key] public int MarketGroupId {get;set;}
        [J("name")] public string Name { get; set; }
        [J("description")] public string Description { get; set; }
        [J("types")] public List<int> Types { get; set; }
        [J("parent_group_id")] public int? ParentGroupId { get; set; }
    }
}
