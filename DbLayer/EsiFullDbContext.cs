using System.Data.Common;
using System.Data.Entity;
using EntrepreneurCommon.Models.DatabaseModels;
using EntrepreneurCommon.Models.EsiResponseModels;

namespace EntrepreneurCommon.DbLayer
{
    public class EsiFullDbContext : DbContext
    {
        public EsiFullDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        public EsiFullDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection,
            contextOwnsConnection) { }

        protected EsiFullDbContext() { }

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
        public DbSet<DbIndustryMiningExtraction> CorporationMiningExtractions { get; set; }
        public DbSet<CorporationMiningObserverLedgerModel> CorporationMiningLedger { get; set; }
        public DbSet<CorporationMiningObserversModel> CorporationMiningObservers { get; set; }

        public DbSet<CharacterMiningLedgerModel> CharacterMiningLedger { get; set; }


        // Wallet
        public DbSet<CharacterWalletJournalModel> CharacterWalletJournal { get; set; }
        public DbSet<CorporationWalletJournalModel> CorporationWalletJournal { get; set; }

        // Wallet Transactions
        public DbSet<CharacterWalletTransactionModel> CharacterWalletTransactions { get; set; }
        public DbSet<CorporationWalletTransactionModel> CorporationWalletTransactions { get; set; }

        // Market Order History
        public DbSet<CharacterMarketOrdersHistoryModel> CharacterMarketOrdersHistory { get; set; }
        public DbSet<CorporationMarketOrdersHistory> CorporationmarketOrdersHistory { get; set; }
    }
}