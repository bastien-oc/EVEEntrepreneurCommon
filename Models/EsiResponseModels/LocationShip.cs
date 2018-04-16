using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    public class LocationShip
    {
        public static readonly String Endpoint = "/v1/characters/{character_id}/ship/";
        public static readonly String Scope = "esi-location.read_ship_type.v1";

        [J("ship_type_id")] public int ShipTypeId { get; set; }
        [J("ship_item_id")] public long ShipItemId { get; set; }
        [J("ship_name")] public string ShipName { get; set; }
    }
}
