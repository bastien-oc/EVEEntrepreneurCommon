using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurCommon.Models.DatabaseModels;
using EntrepreneurEsiApi.Models.DatabaseModels;

namespace EntrepreneurCommon.DbLayer
{
    public class DbDataContext:DbContext
    {
        public DbDataContext( string nameOrConnectionString ) : base(nameOrConnectionString)
        {
        }
        public DbDataContext( DbConnection existingConnection, bool contextOwnsConnection ) : base(existingConnection, contextOwnsConnection)
        {

        }
        protected DbDataContext()
        {
        }

        // Market Models
        public DbSet<DbMarketHistoryModel> MarketHistory { get; set; }
        public DbSet<DbMarketOrderModel> MarketOrders { get; set; }
        public DbSet<DbMarketStatModel> MarketStat { get; set; }
        
        public DbSet<DbCustomPrice> MarketCustomPrice { get; set; }

        // Names Cache
        public DbSet<DbUniverseName> UniverseNames { get; set; }
    }
}
