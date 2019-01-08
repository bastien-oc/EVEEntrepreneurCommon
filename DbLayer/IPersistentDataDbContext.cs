using System.Data.Entity;
using EntrepreneurCommon.Models.DatabaseModels;
using EntrepreneurCommon.Models.EsiResponseModels;

namespace EntrepreneurCommon.DbLayer
{
    public interface IPersistentDataDbContext
    {
        DbSet<CharacterMarketOrdersHistoryModel> CharacterMarketOrdersHistory { get; set; }
        DbSet<CharacterMiningLedgerModel> CharacterMiningLedger { get; set; }
        DbSet<CharacterWalletJournalModel> CharacterWalletJournal { get; set; }
        DbSet<CharacterWalletTransactionModel> CharacterWalletTransactions { get; set; }
        DbSet<CorporationMarketOrdersHistory> CorporationMarketOrdersHistory { get; set; }
        DbSet<CorporationMiningExtractionModel> CorporationMiningExtractions { get; set; }
        DbSet<CorporationMiningObserverLedgerModel> CorporationMiningLedger { get; set; }
        DbSet<CorporationMiningObserversModel> CorporationMiningObservers { get; set; }
        DbSet<CorporationWalletJournalModel> CorporationWalletJournal { get; set; }
        DbSet<CorporationWalletTransactionModel> CorporationWalletTransactions { get; set; }
        DbSet<MarketsRegionHistoryResponse> MarketsRegionHistory { get; set; }
    }
}