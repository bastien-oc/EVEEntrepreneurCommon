using System;

namespace EntrepreneurCommon.Client {
    public class EsiConfiguration
    {
        public string BaseUrl { get; set; } = "https://esi.tech.ccp.is";
        public string DataSource { get; set; }
        public string ClientId { get; set; }
        public string SecretKey { get; set; }
        public string CallbackUrl { get; set; }
        public string UserAgent { get; set; }
    }
}