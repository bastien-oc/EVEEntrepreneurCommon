using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EntrepreneurCommon.DbLayer {
    public class MyDbConfiguration
    {
        public string           ConnectionString   { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DbConnectionType ConnectionProvider { get; set; }
    }
}