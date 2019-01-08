using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    /// <summary>
    /// Paginated record of all mining seen by an observer
    /// Path: corporation_id, observer_id
    /// Query: datasource, page, token user_agent
    /// Header: X-User-Agent
    /// </summary>
    [Table("corporation_mining_observer_ledger")]
    [EsiEndpoint("/v1/corporation/{corporation_id}/mining/observers/{observer_id}/", true,
        new[] {EsiCorporationScopes.IndustryMiningRead})]
    public class CorporationMiningObserverLedgerModel : IEsiResponseModel
    {
        // DbModel Keys
        [Column]
        [RestParameterMapping("corporation_id")]
        public int CorporationId { get; set; }

        [Key, Column(Order = 1), JsonIgnore]
        [RestParameterMapping("observer_id")]
        public long ObserverId { get; set; }

        // Data
        [Key, Column(Order = 2), J("character_id")]
        public int CharacterId { get; set; }

        [Key, Column(Order = 3), J("recorded_corporation_id")]
        public int RecordedCorporationId { get; set; }

        [Key, Column(Order = 4), J("type_id")]
        public int TypeId { get; set; }

        [J("quantity")]
        public int Quantity { get; set; }

        [Key, Column(Order = 5), J("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}