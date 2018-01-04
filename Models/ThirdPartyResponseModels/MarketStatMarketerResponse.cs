using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntrepreneurEsiApi.Models.ThirdPartyResponseModels
{
    using J = Newtonsoft.Json.JsonPropertyAttribute;

    public partial class MarketStatMarketerResponse
    {
        [J("buy")] public Buy Buy { get; set; }
        [J("sell")] public Buy Sell { get; set; }
    }

    public partial class Buy
    {
        [J("avg")] public double Avg { get; set; }
        [J("fivePercent")] public double FivePercent { get; set; }
        [J("forQuery")] public ForQuery ForQuery { get; set; }
        [J("generated")] public long Generated { get; set; }
        [J("highToLow")] public bool HighToLow { get; set; }
        [J("max")] public double Max { get; set; }
        [J("median")] public double Median { get; set; }
        [J("min")] public double Min { get; set; }
        [J("stdDev")] public double StdDev { get; set; }
        [J("variance")] public double Variance { get; set; }
        [J("volume")] public long Volume { get; set; }
        [J("wavg")] public double Wavg { get; set; }
    }

    public partial class ForQuery
    {
        [J("bid")] public bool Bid { get; set; }
        [J("hours")] public int Hours { get; set; }
        [J("minq")] public int Minq { get; set; }
        [J("regions")] public int[] Regions { get; set; }
        [J("systems")] public int[] Systems { get; set; }
        [J("types")] public int[] Types { get; set; }
    }
}
