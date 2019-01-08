using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using EntrepreneurCommon.Client;
using RestSharp;

namespace EntrepreneurCommon.Models
{
    [Table("app_cache_timers")]
    public class CacheTimer
    {
        public CacheTimer() { }

        public CacheTimer(IRestResponse response, IEsiRestClient client, string dataSource = "tranquility")
        {
            Resource = client.BuildUri(response.Request).AbsolutePath;
            DataSource = dataSource;
            Key = "";
            foreach (var p in response.Request.Parameters.Where(param => param.Type == ParameterType.QueryString)) {
                if (p.Name != "datasource") {
                    Key += $"{p.Value}";
                }
            }
            Expires = DateTime.Parse((string) response.Headers.FirstOrDefault(t => t.Name == "Expires")?.Value).ToUniversalTime();
            LastUpdated = DateTime.UtcNow;
        }

        [Key]
        [Column(Order = 0)]
        [Index("CacheTimer", IsUnique = true, Order = 0)]
        public string Resource { get; set; }

        [Key]
        [Column(Order = 1)]
        [Index("CacheTimer", IsUnique = true, Order = 1)]
        public string DataSource { get; set; }

        [Key]
        [Column(Order = 2)]
        [Index("CacheTimer", IsUnique = true, Order = 2)]
        public string Key { get; set; }

        public DateTime Expires     { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
