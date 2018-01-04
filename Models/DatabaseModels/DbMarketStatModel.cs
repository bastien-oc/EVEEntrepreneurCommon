using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurEsiApi.Models.ThirdPartyResponseModels;
using Newtonsoft.Json;

namespace EntrepreneurEsiApi.Models.DatabaseModels
{
    [Table("market_marketerstat")]
    public class DbMarketStatModel
    {
        // Index
        /// <summary>
        /// TypeId of the item
        /// </summary>
        [Column("type_id", Order = 1)] [Key, DatabaseGenerated(DatabaseGeneratedOption.None)] public int TypeId { get; set; }

        /// <summary>
        /// SystemId or RegionId
        /// </summary>
        //[Column("location_id")] public int? Location { get; set; }
        /// <summary>
        /// Generated at (UNIX Timestamp msec)
        /// </summary>
        [Column("generated_at")] public long Generated { get; set; }

        [Column("buy_min")] public double BuyMin { get; set; }
        [Column("buy_avg")] public double BuyAvg { get; set; }
        [Column("buy_max")] public double BuyMax { get; set; }
        [Column("buy_median")] public double BuyMedian { get; set; }
        [Column("buy_variance")] public double BuyVariance { get; set; }
        [Column("buy_stdev")] public double BuyStDev { get; set; }
        [Column("buy_fivepercent")] public double BuyFivePercent { get; set; }
        [Column("buy_weightedaverage")] public double BuyWeightedAverage { get; set; }
        [Column("buy_volume")] public long BuyVolume { get; set; }

        [Column("sell_min")] public double SellMin { get; set; }
        [Column("sell_avg")] public double SellAvg { get; set; }
        [Column("sell_max")] public double SellMax { get; set; }
        [Column("sell_median")] public double SellMedian { get; set; }
        [Column("sell_variance")] public double SellVariance { get; set; }
        [Column("sell_stdev")] public double SellStDev { get; set; }
        [Column("sell_fivepercent")] public double SellFivePercent { get; set; }
        [Column("sell_weightedaverage")] public double SellWeightedAverage { get; set; }
        [Column("sell_volume")] public long SellVolume { get; set; }

        /// <summary>
        /// Assign values from existing MarketStatMarketerResponse if available.
        /// </summary>
        /// <param name="model"></param>
        public void AssignFromMarketStat( MarketStatMarketerResponse model )
        {
            this.TypeId = model.Sell.ForQuery.Types[0];
            this.Generated = model.Sell.Generated;

            this.BuyMin = model.Buy.Min;
            this.BuyAvg = model.Buy.Avg;
            this.BuyMax = model.Buy.Max;
            this.BuyStDev = model.Buy.StdDev;
            this.BuyMedian = model.Buy.Median;
            this.BuyVariance = model.Buy.Variance;
            this.BuyFivePercent = model.Buy.FivePercent;
            this.BuyWeightedAverage = model.Buy.Wavg;

            this.SellMin = model.Sell.Min;
            this.SellAvg = model.Sell.Avg;
            this.SellMax = model.Sell.Max;
            this.SellStDev = model.Sell.StdDev;
            this.SellMedian = model.Sell.Median;
            this.SellVariance = model.Sell.Variance;
            this.SellFivePercent = model.Sell.FivePercent;
            this.SellWeightedAverage = model.Sell.Wavg;
        }

    }
}
