using System.Data.Common;
using System.Data.Entity;
using EntrepreneurCommon.ExtensionMethods;
using EntrepreneurCommon.Models;
using EntrepreneurCommon.Models.DatabaseModels;
using EntrepreneurCommon.Models.EsiResponseModels;

namespace EntrepreneurCommon.DbLayer
{
    public class PersistentDataDbContext : DbContext
    {
        public PersistentDataDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        public PersistentDataDbContext() { }

        public PersistentDataDbContext(DbConnection existingConnection, bool contextOwnsConnection) :
            base(existingConnection,
                 contextOwnsConnection) { }

        // Application entries
        public DbSet<DbTokenWrapper>   Tokens         { get; set; }
        public DbSet<DbUniverseName>   UniverseNames  { get; set; }
        public DbSet<CacheTimer>       CacheTimers    { get; set; }
        public DbSet<CharacterRolesDb> CharacterRoles { get; set; }

        // Entity information entries
        public DbSet<CharacterPublicInformation>   CharacterInformation   { get; set; }
        public DbSet<CorporationPublicInformation> CorporationInformation { get; set; }

        // Entity based persistence
        public DbSet<CharacterWalletJournalModel>          CharacterWalletJournal         { get; set; }
        public DbSet<CharacterWalletTransactionModel>      CharacterWalletTransactions    { get; set; }
        public DbSet<CharacterMarketOrdersHistoryModel>    CharacterMarketOrdersHistory   { get; set; }
        public DbSet<CharacterMiningLedgerModel>           CharacterMiningLedger          { get; set; }
        public DbSet<CorporationWalletJournalModel>        CorporationWalletJournal       { get; set; }
        public DbSet<CorporationWalletTransactionModel>    CorporationWalletTransactions  { get; set; }
        public DbSet<CorporationMarketOrdersHistory>       CorporationMarketOrdersHistory { get; set; }
        public DbSet<CorporationMiningObserverLedgerModel> CorporationMiningLedger        { get; set; }
        public DbSet<CorporationMiningExtractionModel>     CorporationMiningExtractions   { get; set; }
        public DbSet<CorporationMiningObserversModel>      CorporationMiningObservers     { get; set; }

        // Universe wide persistence
        public DbSet<MarketsRegionHistoryResponse> MarketsRegionHistory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Ensure snake_case naming convention for consistency across databases
            modelBuilder.Properties().Configure(c => {
                                                    var name    = c.ClrPropertyInfo.Name;
                                                    var newName = name.ToSnakeCase();
                                                    c.HasColumnName(newName);
                                                });
        }
    }
}
