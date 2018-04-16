using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurCommon.Models.DatabaseModels;
using EntrepreneurCommon.Models.EsiResponseModels;
using EntrepreneurEsiApi.Models.DatabaseModels;
using EntrepreneurEsiApi.Models.Esi;

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

        public DbSet<DbMarketPriceCustom> MarketCustomPriceNew { get; set; }
        public DbSet<DbMarketRefining> MarketRefining { get; set; }

        // Names Cache
        public DbSet<DbUniverseName> UniverseNames { get; set; }

        // Industry
        public DbSet<DbIndustryMiningExtraction> MiningExtractions { get; set; }
        public DbSet<IndustryMiningObserverMiningDone> MiningLedgerCorp { get; set; }
        public DbSet<IndustryMiningObserverResponse> MiningObservers { get; set; }

        // Wallet
        public DbSet<WalletJournalModelChar> WalletJournalCharacter { get; set; }
        public DbSet<WalletJournalModelCorp> WalletJournalCorporate { get; set; }

        // Entity Market Orders
        public DbSet<MarketTransactionChar> MarketTransactionsCharacter { get; set; }
        public DbSet<MarketTransactionCorp> MarketTransactionsCorporate { get; set; }
    }
}
