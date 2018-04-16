using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    public class LocationOnline
    {
        public static readonly String Endpoint = "/v2/characters/{character_id}/online/";
        public static readonly String Scope = "esi-location.read_online.v1";

        [J("online")] public bool Online { get; set; }
        [J("last_login")] public DateTime LastLogin { get; set; }
        [J("last_logout")] public DateTime LastLogout { get; set; }
        [J("logins")] public int Logins { get; set; }
    }
}
