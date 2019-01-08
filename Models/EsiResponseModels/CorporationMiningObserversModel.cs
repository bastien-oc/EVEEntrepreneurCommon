using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    /// <inheritdoc/>
    /// <summary>
    /// Paginated list of all entities capable of observing and recording mining for a corporation
    /// </summary>
    [EsiEndpoint("/v1/corporation/{corporation_id}/mining/observers/", true,
        new[] {EsiCorporationScopes.IndustryMiningRead})]
    public class CorporationMiningObserversModel : IEsiResponseModel
    {
        /// <summary>
        /// Database use only, allows us to remember which corp had access to which observer.
        /// </summary>
        [Column("corporation_id")]
        [RestParameterMapping("corporation_id")]
        public int CorporationId { get; set; }

        /// <summary>
        /// The entity that was observing the asteroid field when it was mined.
        /// </summary>
        [Column("observer_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [J("observer_id")]
        public long ObserverId { get; set; }

        /// <summary>
        /// The category of the observing entity
        /// </summary>
        [Column("observer_type")]
        [J("observer_type")]
        public string ObserverType { get; set; }

        /// <summary>
        /// last_updated string
        /// </summary>
        [Column("last_updated")]
        [J("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}