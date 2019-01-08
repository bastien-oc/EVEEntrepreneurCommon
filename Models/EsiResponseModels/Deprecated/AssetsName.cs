using System;
using System.ComponentModel.DataAnnotations.Schema;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    /// <summary>
    /// Supplemental query to AssetModel. Return names for a set of item ids, which you can get from character or corporation assets endpoint. Typically used for items that can customize names, like containers or ships.
    /// Path: character_id or corporation_id
    /// Query: datasource, token, user_agent
    /// Body: item_ids = array of integer $int64
    /// Header: X-User-Agent
    /// </summary>
    [Obsolete("Obsolete model.", true)]
    public class AssetsName
    {
        public static readonly string[] Endpoint = {
            "/v2/characters/{character_id}/assets/names/",
            "/v2/corporations/{corporation_id}/assets/names/"
        };
        public static readonly PathParamType[] ParamTypes = {
            PathParamType.CharacterID,
            PathParamType.CorporationID
        };

        /// <summary>
        /// post_corporations_corporation_id_assets_names_item_id
        /// </summary>
        [J("item_id")]
        [Column("item_id")]
        public long ItemID { get; set; }

        /// <summary>
        /// post_corporations_corporation_id_assets_names_name
        /// </summary>
        [J("name")]
        [Column("name")]
        public string Name { get; set; }
    }
}
