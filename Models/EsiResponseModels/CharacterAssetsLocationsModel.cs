using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Models.EsiResponseModels {
    [EsiEndpoint("/v2/characters/{character_id}/assets/locations/", false, new[] {EsiCharacterScopes.AssetsRead})]
    public class CharacterAssetsLocationsModel : IEsiResponseModel {
        /// <summary>
        ///     item_id integer
        /// </summary>
        /// <value>item_id integer</value>
        public long ItemId { get; set; }

        /// <summary>
        ///     Gets or Sets Position
        /// </summary>
        public AssetsLocationsPosition Position { get; set; }
    }
}
