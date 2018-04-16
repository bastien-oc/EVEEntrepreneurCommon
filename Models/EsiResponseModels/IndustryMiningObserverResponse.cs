using System;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EntrepreneurEsiApi.Models.Esi
{
    /// <summary>
    /// Paginated list of all entities capable of observing and recording mining for a corporation
    /// </summary>
    [Table("mining_observers")]
    public class IndustryMiningObserverResponse
    {
        public static readonly String Endpoint = "/v1/corporation/{corporation_id}/mining/observers/";

        /// <summary>
        /// Database use only, allows us to remember which corp had access to which observer.
        /// </summary>
        [Column("owner_id")]
        public Int32 OwnerID { get; set; }
        /// <summary>
        /// The entity that was observing the asteroid field when it was mined.
        /// </summary>
        [Column("observer_id"), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [J("observer_id")]
        public Int64 ObserverID { get; set; }
        /// <summary>
        /// The category of the observing entity
        /// </summary>
        [Column("observer_type")] [J("observer_type")] public String ObserverType { get; set; }
        /// <summary>
        /// last_updated string
        /// </summary>
        [Column("last_updated")] [J("last_updated")] public DateTime LastUpdated { get; set; }
    }


}