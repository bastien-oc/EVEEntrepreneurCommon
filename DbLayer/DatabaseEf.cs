using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using EntrepreneurCommon.Api;
using EntrepreneurCommon.Api.SystemModels;
using EntrepreneurCommon.Authentication;
using EntrepreneurCommon.Client;
using EntrepreneurCommon.Helpers;
using EntrepreneurCommon.Logging;
using EntrepreneurCommon.Models;
using EntrepreneurCommon.Models.DatabaseModels;
using EntrepreneurCommon.Models.EsiResponseModels;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using RestSharp;

namespace EntrepreneurCommon.DbLayer
{
    // Entity Framework based database handler
    /// <summary>
    /// The Database Provider class is responsible for handling the data between API and the persistence database.
    /// It is specifically responsible for pulling data from API only when needed by utilizing cache timers,
    ///  and ensures that all the data that qualifies for persistence is passed through the database and is not lost
    ///  in the ether.
    /// </summary>
    /// <remarks>
    /// All methods responsible for pulling data from database must request that the database attempts to update itself
    ///  from the API first.
    /// </remarks>
    /// TODO: Current implementation of Get data returns IEnumerable, resulting in the data NOT being persisted unless module calling the method iterates through. Change this to ICollection instead, moving persistence updates to separate methods.
    public class DatabaseEf : IDatabaseProvider
    {
        private readonly EsiAuthClient         _auth;
        private readonly IEsiRestClient        _client;
        private readonly MyDbConfiguration     _configuration;
        private readonly ILogger                  _logger;
        private          ITokenStorageProvider _tokenStorageProvider;

        /// <summary>
        /// States whether the Database Provider relies on separate token provider
        /// </summary>
        public bool HasCustomTokenProvider => _tokenStorageProvider != null;

        private bool MirrorTokenWrites { get; set; }

        /// <summary>
        /// Returns either in-built or external token set based on whether there is custom ITokenStorageProvider defined
        /// </summary>
        public IEnumerable<DbTokenWrapper> Tokens {
            get {
                if (HasCustomTokenProvider) {
                    return _tokenStorageProvider.Tokens;
                }

                return GetContext()?.Tokens;
            }
        }

        public DatabaseEf(MyDbConfiguration configuration, EsiConfiguration esiConfiguration, ILogger logger = null)
        {
            _logger?.Info($"Creating new database access class for provider {configuration.ConnectionProvider} with connection string \"{configuration.ConnectionString}\"");
            _configuration = configuration;
            _auth = new EsiAuthClient(esiConfiguration.ClientId, esiConfiguration.SecretKey,
                                      esiConfiguration.CallbackUrl);
            _client = new EsiRestClient(esiConfiguration, logger: logger);

            _logger = logger;

            ApiCharacter = new CharacterApi(_client);
            ApiCorporation = new CorporationApi(_client);
            ApiIndustry = new IndustryApi(_client);
            ApiMarket = new MarketApi(_client);
            ApiUniverse = new UniverseApi(_client);
            ApiWallet = new WalletApi(_client);
        }

        /// <summary>
        /// Instructs the Database Provider to use independent storage provider for ESI tokens.
        /// </summary>
        /// <remarks>
        /// That way the tokens may be stored in other formats, such as JSON file, which could be accessible for
        ///  smaller applications.
        /// </remarks>
        /// <param name="tokenStorage"></param>
        public void SetSeparateTokenStorageProvider(ITokenStorageProvider tokenStorage)
        {
            _logger.Info($"Setting {tokenStorage.GetType().Name} as token storage provider.");
            _tokenStorageProvider = tokenStorage;
        }

        /// <summary>
        /// Instructs the database provider to mirror tokens when updating them, as opposed to only updating
        ///  ITokenStorageProvider when using one.
        /// </summary>
        /// <param name="value"></param>
        public void SetMirrorTokenWrites(bool value)
        {
            _logger.Info($"Mirror token updates in database: {value.ToString()}");
            MirrorTokenWrites = value;
        }

    #region Entity Framework

        /// <summary>
        ///     Gets a configured context
        /// </summary>
        /// <returns></returns>
        private PersistentDataDbContext GetContext()
        {
            switch (_configuration.ConnectionProvider) {
                case DbConnectionType.SqlServer:
                    // _logger?.Debug($"Creating new connection for SQL Server with connection string {_configuration.ConnectionString}");
                    return new PersistentDataDbContext(new SqlConnection(_configuration.ConnectionString), true);
                case DbConnectionType.SQLite:
                    // _logger?.Debug($"Creating new connection for SQLite with connection string {_configuration.ConnectionString}");
                    return new PersistentDataDbContext(new SQLiteConnection(_configuration.ConnectionString), true);
                case DbConnectionType.MySQL:
                    // _logger?.Debug($"Creating new connection for MySQL with connection string {_configuration.ConnectionString}");
                    return new PersistentDataDbContext(new MySqlConnection(_configuration.ConnectionString), true);
                default:
                    return default;
            }
        }

        private void AddToDb<TEntity>(params TEntity[] entities) where TEntity : class
        {
            _logger?.Debug($"Adding {typeof(TEntity).Name} {{{JsonConvert.SerializeObject(entities)}}}");
            try {
                using (var context = GetContext()) {
                    var dbSet = context.Set<TEntity>();
                    dbSet.AddOrUpdate(entities);
                    context.SaveChangesAsync().Wait();
                    context.Database.Connection.Close();
                }
            }
            catch (Exception e) {
                _logger?.Error($"Exception occured when attempting to add entity '{typeof(TEntity).Name}': {e.Message}",
                               e);
                throw;
            }
        }

        // Abstracts the AddToDb() with params parameter to accept a collection of items without needing to explicitly remember to convert them.

        private void AddToDb<TEntity>(ICollection<TEntity> entities) where TEntity : class
        {
            AddToDb(entities.ToArray());
        }

        // private IQueryable<TEntity> GetFromDb<TEntity>() where TEntity : class

        private IEnumerable<TEntity> GetFromDb<TEntity>() where TEntity : class
        {
            _logger?.Debug($"Retrieving {typeof(TEntity).Name} from database");
            using (var context = GetContext()) {
                var dbSet = context.Set<TEntity>().ToArray();
                context.Database.Connection.Close();
                return dbSet;
            }
        }

        // private IQueryable<TEntity> GetFromDb<TEntity>(Expression<Func<TEntity, bool>> predicate)

        private IEnumerable<TEntity> GetFromDb<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            _logger?.Debug($"Retrieving {typeof(TEntity).Name} from database with predicate \"{predicate.ToString()}\"");
            try {
                using (var context = GetContext()) {
                    var dbSet  = context.Set<TEntity>();
                    var result = dbSet.Where(predicate).ToArray();
                    context.Database.Connection.Close();
                    return result;
                }
            }
            catch (Exception e) {
                _logger?.Error($"Error while retrieving {typeof(TEntity).Name} with predicate {predicate}");
                throw;
            }
        }

    #endregion

    #region Operations on Tokens

        /// <summary>
        /// Saves the token in automatically determined location.
        /// </summary>
        /// <param name="token"></param>
        public void SaveToken(DbTokenWrapper token)
        {
            if (HasCustomTokenProvider) {
                _tokenStorageProvider.SaveToken(token);
            }
            else {
                AddToDb(token);
            }

            if (HasCustomTokenProvider && MirrorTokenWrites) {
                AddToDb(token);
            }
        }

        /// <summary>
        ///     Get all tokens with a selected scope
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public IEnumerable<DbTokenWrapper> GetTokens(string scope = null)
        {
            // var tokens = GetContext()?.Tokens;
            // IEnumerable<DbTokenWrapper> tokens;
            // if (HasCustomTokenProvider) {
            //     tokens = TokenStorageProvider.Tokens;
            // }
            // else {
            //     tokens = GetContext()?.Tokens;
            // }

            foreach (var t in Tokens) {
                if (_auth.CheckScope(t, scope)) {
                    yield return t;
                } // If scope matches, yield return.
            }     // Iterate through all tokens and yield the ones with matching scopes.
        }

        /// <summary>
        ///     Get token for specific character with optional scope restriction.
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public DbTokenWrapper GetToken(int characterId, string scope = "")
        {
            // var context = GetContext();
            // IEnumerable<DbTokenWrapper> tokens;
            //
            // if (HasCustomTokenProvider) {
            //     tokens = TokenStorageProvider.Tokens.Where(t => t.CharacterId == characterId);
            // }
            // else {
            //     tokens = GetFromDb<DbTokenWrapper>().Where(t => t.CharacterId == characterId);
            // }

            foreach (var t in Tokens.Where(t => t.CharacterId == characterId)) {
                if (_auth.CheckScope(t, scope)) {
                    return t;
                } // If scope matches, return the token.
            }     // Return first token with matching scope.

            return null; // No valid tokens were found.
        }

        /// <summary>
        ///     Returns the first token whose character has required scopes and corporate roles.
        /// </summary>
        /// <param name="corporationId">ID of the Corporation</param>
        /// <param name="scope">Scopes required</param>
        /// <param name="corpRoles">Roles required</param>
        /// <returns></returns>
        public DbTokenWrapper GetTokenCorporation(int         corporationId,
                                                  string      scope     = "",
                                                  RolesEnum[] corpRoles = null)
        {
            /*
             * Steps:
             * 1. Get all tokens.
             * 2. Filter for scopes. Only process tokens for characters with valid scopes.
             * 3. (Api GET) Fetch Character Public Information - to see whether character is in the required corporation.
             * 4. (Api GET) Get Character Roles and discard tokens for characters without required roles
             */
            // var context = GetContext();
            // var tokens = GetFromDb<DbTokenWrapper>();
            // var tokens = context.Tokens.Where(t => t.CharacterId == characterId);
            // Process for scope validity
            ICollection<DbTokenWrapper> validScopeTokens = new List<DbTokenWrapper>();
            foreach (var t in Tokens) {
                if (_auth.CheckScope(t, scope)) {
                    validScopeTokens.Add(t);
                }
            } // Get all tokens with matching scopes into validScopeTokens.

            // Check if there is any token with valid scope.
            if (validScopeTokens.Any() == false) {
                return null;
            } // No valid token was found. Return.

            // Check character for required roles.
            foreach (var t in validScopeTokens) {
                var characterInfo = GetCharacterInformation(t.CharacterId);
                if (characterInfo.CorporationId != corporationId) {
                    continue;
                } // Character is not in the required corporation. Continue iterating.
                // This section is reached only when corporationId is matched.

                // Check if any roles are actually needed.
                if (corpRoles == null) {
                    return t;
                } // If not, return the first token with valid corporation.

                var characterRoles = GetCharacterRoles(t.CharacterId);
                var valid          = false;
                // Iterate through required roles and check if the character has any of them.
                foreach (var r in corpRoles) {
                    if (characterRoles.Roles.Contains(r)) {
                        valid = true;
                        break;
                    } // Role is found, break from loop so you can return immediately.
                }     // Sets valid to true if role requirement is met.

                if (valid) {
                    return t;
                } // Return token if valid.
            }

            return null; // No token with valid corporate roles was found. Return null.
        }

        /// <summary>
        ///     Provides up-to-date authentication token, updating the token container within database if necessary.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string GetAccessToken(DbTokenWrapper token)
        {
            // var context    = GetContext();
            // var dbTokenRef = GetFromDb<DbTokenWrapper>().FirstOrDefault(t => t.Uuid == token.Uuid);
            var dbTokenRef = Tokens.FirstOrDefault(t => t.Uuid == token.Uuid);
            var result     = _auth.GetAccessToken(dbTokenRef);
            SaveToken(dbTokenRef);
            // context.Tokens.AddOrUpdate(dbTokenRef);
            // context.SaveChanges();
            return result;
        }

        /// <summary>
        ///     Updates the token if necessary.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public DbTokenWrapper RefreshToken(DbTokenWrapper token)
        {
            // var context    = GetContext();
            // var dbTokenRef = GetFromDb<DbTokenWrapper>().FirstOrDefault(t => t.Uuid == token.Uuid);
            var dbTokenRef = Tokens.FirstOrDefault(t => t.Uuid == token.Uuid);
            var result     = _auth.RefreshToken(dbTokenRef) as DbTokenWrapper;
            SaveToken(dbTokenRef);
            // context.Tokens.AddOrUpdate(dbTokenRef);
            // context.SaveChanges();
            return result;
        }

    #endregion

    #region Cache Timers

        /// <summary>
        ///     Check if the entry has expired cache. True if entry is due to be refreshed.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="key"></param>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public bool IsCacheExpired(string resource, string key = "", string dataSource = "tranquility")
        {
            // Default is "Cache is not expired or not existing"
            bool       result = false;
            CacheTimer timer  = null;

            try {
                timer = GetCacheTimer(resource, key, dataSource);
            }
            catch (Exception e) {
                _logger?.Error($"Exception occured when checking cache timer for for resource '{resource}' with key '{key}' from data source '{dataSource}: {e.Message}'.",
                               e);
                throw;
            }
            finally {
                if (timer == null) {
                    result = true;
                    // return true;
                } // Entry is not cached, treat as expired.

                if (timer?.Expires < DateTime.UtcNow) {
                    result = true;
                } // Expiry date is in the past, thus expired.}
            }

            _logger?.Debug($"Checking cache expiry for resource '{resource}' with key '{key}' from data source '{dataSource}':" +
                           $"\n\t\t{timer?.Expires.ToUniversalTime()} (timer) vs {DateTime.UtcNow} (now): {((result == false) ? "not expired" : "expired or non existent")}.");
            return result; // Expiry is still in the future. Return false.
        }

        /// <summary>
        ///     Retrieves a cache timer from the database or returns null if there is no cache for the entry.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="key"></param>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public CacheTimer GetCacheTimer(string resource, string key = "", string dataSource = "tranquility")
        {
            // var context = GetContext();
            return GetFromDb<CacheTimer>().FirstOrDefault(t => (t.Resource   == resource) && (t.Key == key) &&
                                                               (t.DataSource == dataSource));
        }

        /// <summary>
        ///     Add or Update cache timer in the database.
        /// </summary>
        /// <param name="response"></param>
        public void UpdateCacheTimer(IRestResponse response)
        {
            var cacheTimer = new CacheTimer(response, _client);
            AddToDb(cacheTimer);
            // var context    = GetContext();
            // context.CacheTimers.AddOrUpdate(cacheTimer);
            // context.SaveChanges();
        }

    #endregion

    #region Esi Calls

    #region SECTION General calls

        /// <summary>
        ///     Resolve a set of IDs to universe names. All IDs need to be valid, otherwise whole query will be invalid.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<DbUniverseName> ResolveIdToName(params int[] id)
        {
            // var context  = GetContext();
            var toLookUp = new List<int>();
            foreach (var i in id) {
                if (GetFromDb<DbUniverseName>().Any(t => t.Id == i)) {
                    yield return GetFromDb<DbUniverseName>().FirstOrDefault(t => t.Id == i);
                } // If exists in database, return it. 
                else {
                    toLookUp.Add(i);
                } // Otherwise add to lookup.
            }     // Fetch entries from database, query missing.

            var responses = ApiUniverse.GetUniverseNames(toLookUp.ToArray());
            foreach (var r in responses) {
                var entry = new DbUniverseName {
                    Id = r.ID,
                    Name = r.Name,
                    Category = r.Category
                };
                AddToDb(entry);
                // context.UniverseNames.AddOrUpdate(entry);
                // context.SaveChanges();
                yield return entry;
            } // GET all the missing names and yield return them.
        }

        /// <summary>
        ///     Wrapped for ResolveIdToName to work with single ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DbUniverseName ResolveIdToName(int id)
        {
            return ResolveIdToName(new[] {id}).FirstOrDefault();
        }

        /// <summary>
        ///     Retrieve a public character information from api.
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public CharacterPublicInformation GetCharacterInformation(int characterId)
        {
            // var context = GetContext();
            var endpoint = RequestHelper.GetEndpointUrl<CharacterPublicInformation>()
                                        .Replace("{character_id}", $"{characterId}");

            if (IsCacheExpired(endpoint) == false) {
                if (GetFromDb<CharacterPublicInformation>().Any(t => t.CharacterId == characterId)) {
                    return GetFromDb<CharacterPublicInformation>().First(t => t.CharacterId == characterId);
                }
            } // If cache for the entry is not expired, retrieve it from the database. Otherwise, update.

            var apiResponse = ApiCharacter.GetCharacterPublicInformation(characterId);
            var apiData     = apiResponse.Data;
            AddToDb(apiData);
            // context.CharacterInformation.AddOrUpdate(apiData);
            // context.SaveChanges();

            UpdateCacheTimer(apiResponse);
            return apiData;
        }

        /// <summary>
        ///     Retrieve a public corporation information from api.
        /// </summary>
        public CorporationPublicInformation GetCorporationInformation(int corporationId)
        {
            // var context = GetContext();
            var endpoint = RequestHelper.GetEndpointUrl<CorporationPublicInformation>()
                                        .Replace("{corporation_id}", $"{corporationId}");

            // If cache is not expired, retrieve from database.
            if (IsCacheExpired(endpoint) == false) {
                var dbResponse =
                    GetFromDb<CorporationPublicInformation>().FirstOrDefault(t => t.CorporationId == corporationId);
                if (dbResponse != null) {
                    return dbResponse;
                } // Return if not null.
            }

            var apiResponse = ApiCorporation.GetPublicInformation(corporationId);
            var apiData     = apiResponse.Data;
            AddToDb(apiData);
            // context.CorporationInformation.AddOrUpdate(apiData);
            // context.SaveChanges();

            UpdateCacheTimer(apiResponse);

            return apiData;
        }

        /// <summary>
        ///     Retrieve character's corporation roles.
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public CharacterRolesDb GetCharacterRoles(int characterId)
        {
            // var context = GetContext();
            var token = GetToken(characterId, EsiCharacterScopes.CorporationRolesRead);
            if (token == null) {
                return null;
            } // If no valid token, return null.

            var endpoint = RequestHelper.GetEndpointUrl<CharacterRolesModel>()
                                        .Replace("{character_id}", $"{characterId}");
            if (IsCacheExpired(endpoint) == false) {
                return GetFromDb<CharacterRolesDb>().FirstOrDefault(t => t.CharacterId == characterId);
            } // If caches is not expired, use database entry.

            var apiResponse = ApiCharacter.GetCharacterRoles(characterId, _auth.GetAccessToken(token));
            var apiData     = new CharacterRolesDb(apiResponse.Data);
            AddToDb(apiData);
            // context.CharacterRoles.AddOrUpdate(apiData);
            // context.SaveChanges();

            UpdateCacheTimer(apiResponse);

            return apiData;
        }

    #endregion

    #region SECTION Character persistence calls

        public void UpdateCharacterWalletJournal(int characterId)
        {
            var endpoint = RequestHelper.GetEndpointUrl<CharacterWalletJournalModel>()
                                        .Replace("{character_id}", $"{characterId}");
            if (IsCacheExpired(endpoint) == false) {
                return;
            }

            var token = GetToken(characterId, EsiCharacterScopes.WalletRead);
            if (token == null) {
                return;
            } // If token is null, return empty.

            var apiResponse =
                ApiWallet.GetCharacterWalletJournalCompleteWithInfo(characterId, GetAccessToken(token));
            var apiData = apiResponse.Items;
            AddToDb(apiData.ToArray());
            UpdateCacheTimer(apiResponse.Responses.First());
        }

        public void UpdateCharacterWalletTransactions(int characterId)
        {
            var endpoint = RequestHelper.GetEndpointUrl<CharacterWalletTransactionModel>()
                                        .Replace("{character_id}", $"{characterId}");
            if (IsCacheExpired(endpoint) == false) {
                return;
            }

            var token = GetToken(characterId, EsiCharacterScopes.WalletRead);
            if (token == null) {
                return;
            } // If token is null, return empty.

            var apiResponse = ApiWallet.GetCharacterWalletTransactionsWithInfo(characterId, GetAccessToken(token));
            var apiData     = apiResponse.Data;
            AddToDb(apiData.ToArray());
            UpdateCacheTimer(apiResponse);
        }

        public void UpdateCharacterMarketOrdersHistory(int characterId)
        {
            var endpoint = RequestHelper.GetEndpointUrl<CharacterMarketOrdersHistoryModel>()
                                        .Replace("{character_id}", $"{characterId}");
            if (IsCacheExpired(endpoint) == false) {
                return;
            }

            var token = GetToken(characterId, EsiCharacterScopes.MarketsOrdersRead);
            if (token == null) {
                return;
            } // Invalid token, return null.

            var apiResponse = ApiMarket.GetCharacterMarketOrdersHistoryA(characterId, GetAccessToken(token));
            var apiData     = apiResponse.Items;
            AddToDb(apiData.ToArray());
            UpdateCacheTimer(apiResponse.Responses.First());
        }

        public void UpdateCharacterMiningLedger(int characterId)
        {
            var endpoint = RequestHelper.GetEndpointUrl<CharacterMiningLedgerModel>()
                                        .Replace("{character_id}", $"{characterId}");
            if (IsCacheExpired(endpoint) == false) {
                return;
            }

            var token = GetToken(characterId, EsiCharacterScopes.IndustryMiningRead);
            if (token == null) {
                return;
            } // Invalid token, return null.

            var apiResponse = ApiIndustry.GetCharacterMiningLedgerA(characterId, GetAccessToken(token));
            var apiData     = apiResponse.Items;
            AddToDb(apiData.ToArray());
            UpdateCacheTimer(apiResponse.Responses.First());
        }

        /// <summary>
        ///     Get character wallet journal, updating it from api if needed.
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public IEnumerable<CharacterWalletJournalModel> GetCharacterWalletJournal(int characterId)
        {
            UpdateCharacterWalletJournal(characterId);
            return GetFromDb<CharacterWalletJournalModel>(t => t.CharacterId == characterId);
            // // var context = GetContext();
            // var endpoint = RequestHelper.GetEndpointUrl<CharacterWalletJournalModel>()
            //                             .Replace("{character_id}", $"{characterId}");
            // var token = GetToken(characterId, EsiCharacterScopes.WalletRead);
            // if (token == null) {
            //     return default;
            // } // If token is null, return empty.
            //
            // if (IsCacheExpired(endpoint) == false) {
            //     return GetFromDb<CharacterWalletJournalModel>(t => t.CharacterId == characterId);
            //     // return context.CharacterWalletJournal.Where(t => t.CharacterId == characterId);
            // } // If cache is not expired, fetch from database. Otherwise, fetch from api.
            //
            // var apiResponse =
            //     ApiWallet.GetCharacterWalletJournalCompleteWithInfo(characterId, GetAccessToken(token));
            // var apiData = apiResponse.Items;
            //
            // AddToDb(apiData.ToArray());
            // // context.CharacterWalletJournal.AddOrUpdate(apiData.ToArray());
            // // context.SaveChanges();
            //
            // UpdateCacheTimer(apiResponse.Responses.First());
            //
            // return apiData;
        }

        /// <summary>
        ///     Get character wallet transactions, updating entries from api if needed.
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public IEnumerable<CharacterWalletTransactionModel> GetCharacterWalletTransactions(int characterId)
        {
            UpdateCharacterWalletTransactions(characterId);
            return GetFromDb<CharacterWalletTransactionModel>(t => t.CharacterId == characterId);
            //
            // // var context = GetContext();
            // var endpoint = RequestHelper.GetEndpointUrl<CharacterWalletTransactionModel>()
            //                             .Replace("{character_id}", $"{characterId}");
            // var token = GetToken(characterId, EsiCharacterScopes.WalletRead);
            // if (token == null) {
            //     return default;
            // } // If token is null, return empty.
            //
            // if (IsCacheExpired(endpoint) == false) {
            //     return GetFromDb<CharacterWalletTransactionModel>(t => t.CharacterId == characterId);
            //     // return context.CharacterWalletTransactions.Where(t => t.CharacterId == characterId);
            // } // If cache is not expired, fetch from database. Otherwise, fetch from api.
            //
            // var apiResponse = ApiWallet.GetCharacterWalletTransactionsWithInfo(characterId, GetAccessToken(token));
            // var apiData     = apiResponse.Data;
            // AddToDb(apiData.ToArray());
            // // context.CharacterWalletTransactions.AddOrUpdate(apiData.ToArray());
            // // context.SaveChanges();
            //
            // UpdateCacheTimer(apiResponse);
            //
            // return apiData;
        }

        /// <summary>
        ///     Get character market orders history, updating entries from api if needed.
        /// </summary>
        public IEnumerable<CharacterMarketOrdersHistoryModel> GetCharacterMarketOrdersHistory(int characterId)
        {
            UpdateCharacterMarketOrdersHistory(characterId);
            return GetFromDb<CharacterMarketOrdersHistoryModel>(t => t.CharacterId == characterId);
            // // var context = GetContext();

            // var endpoint = RequestHelper.GetEndpointUrl<CharacterMarketOrdersHistoryModel>()
            //                             .Replace("{character_id}", $"{characterId}");
            // var token = GetToken(characterId, EsiCharacterScopes.MarketsOrdersRead);
            // if (token == null) {
            //     return default;
            // } // Invalid token, return null.
            //
            // if (IsCacheExpired(endpoint) == false) {
            //     return GetFromDb<CharacterMarketOrdersHistoryModel>(t => t.CharacterId == characterId);
            //     // return context.CharacterMarketOrdersHistory.Where(t => t.CharacterId == characterId);
            // } // If cache is not expired, fetch from database. Otherwise, fetch from api.
            //
            // var apiResponse = ApiMarket.GetCharacterMarketOrdersHistoryA(characterId, GetAccessToken(token));
            // var apiData     = apiResponse.Items;
            // AddToDb(apiData.ToArray());
            // // context.CharacterMarketOrdersHistory.AddOrUpdate(apiData.ToArray());
            // // context.SaveChanges();
            //
            // UpdateCacheTimer(apiResponse.Responses.First());
            //
            // return apiData;
        }

        /// <summary>
        ///     Get character industry mining ledger, updating it from api if needed.
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public IEnumerable<CharacterMiningLedgerModel> GetCharacterMiningLedger(int characterId)
        {
            UpdateCharacterMiningLedger(characterId);
            return GetFromDb<CharacterMiningLedgerModel>(t => t.CharacterId == characterId);
            // // var context = GetContext();
            // // TODO: Move to separate Update method.
            // var endpoint = RequestHelper.GetEndpointUrl<CharacterMiningLedgerModel>()
            //                             .Replace("{character_id}", $"{characterId}");
            // var token = GetToken(characterId, EsiCharacterScopes.IndustryMiningRead);
            // if (token == null) {
            //     return default;
            // } // Invalid token, return null.
            //
            // if (IsCacheExpired(endpoint) == false) {
            //     return GetFromDb<CharacterMiningLedgerModel>(t => t.CharacterId == characterId);
            //     // return context.CharacterMiningLedger.Where(t => t.CharacterId == characterId);
            // } // If cache is not expired, fetch from database. Otherwise, fetch from api.
            //
            // var apiResponse = ApiIndustry.GetCharacterMiningLedgerA(characterId, GetAccessToken(token));
            // var apiData     = apiResponse.Items;
            // AddToDb(apiData.ToArray());
            // // context.CharacterMiningLedger.AddOrUpdate(apiData.ToArray());
            // // context.SaveChanges();
            //
            // UpdateCacheTimer(apiResponse.Responses.First());
            //
            // return apiData;
        }

    #endregion

    #region SECTION Corporation persistence calls

        public void UpdateCorporationWalletJournal(int corporationId)
        {
            var token = GetTokenCorporation(corporationId, EsiCorporationScopes.WalletsRead,
                                            new[] {RolesEnum.Accountant, RolesEnum.JuniorAccountant});
            if (token == null) {
                return;
            } // No valid tokens returned, return default.

            // Get wallet journal for each of seven divisions 
            for (var division = 1; division <= 7; division++) {
                var endpoint = RequestHelper.GetEndpointUrl<CorporationWalletJournalModel>()
                                            .Replace("{corporation_id}", $"{corporationId}")
                                            .Replace("{division}",       $"{division}");
                if (IsCacheExpired(endpoint) == false) {
                    continue;
                }

                var apiResponse =
                    ApiWallet.GetCorporationWalletJournalCompleteWithInfo(corporationId, division,
                                                                          GetAccessToken(token));
                var apiData = apiResponse.Items;
                AddToDb(apiData.ToArray());
                UpdateCacheTimer(apiResponse.Responses.First());
            }
        }

        public void UpdateCorporationWalletTransactions(int corporationId)
        {
            // TODO: Move to separate Update method.
            var token = GetTokenCorporation(corporationId, EsiCorporationScopes.WalletsRead,
                                            new[] {RolesEnum.Accountant, RolesEnum.JuniorAccountant});
            if (token == null) {
                return;
            } // No valid tokens returned, return default.

            // Get wallet journal for each of seven divisions 
            for (var division = 1; division <= 7; division++) {
                var endpoint = RequestHelper.GetEndpointUrl<CorporationWalletTransactionModel>()
                                            .Replace("{corporation_id}", $"{corporationId}")
                                            .Replace("{division}",       $"{division}");
                if (IsCacheExpired(endpoint) == false) {
                    continue;
                }

                var apiResponse =
                    ApiWallet.GetCorporationWalletTransactionsWithInfo(corporationId, division,
                                                                       GetAccessToken(token));
                var apiData = apiResponse.Items;
                AddToDb(apiData.ToArray());
                UpdateCacheTimer(apiResponse.Responses.First());
            }
        }

        public void UpdateCorporationMarketOrdersHistory(int corporationId)
        {
            var endpoint = RequestHelper.GetEndpointUrl<CorporationMarketOrdersHistory>()
                                        .Replace("{corporation_id}", $"{corporationId}");
            if (IsCacheExpired(endpoint) == false) {
                return;
            }

            var token = GetTokenCorporation(corporationId, EsiCorporationScopes.MarketsOrdersRead,
                                            new[] {RolesEnum.Accountant, RolesEnum.Trader});
            if (token == null) {
                return;
            }

            // Otherwise update from api.
            var apiResponse =
                ApiMarket.GetCorporationMarketOrdersHistoryWithInfo(corporationId, GetAccessToken(token));
            var apiData = apiResponse.Items;
            AddToDb(apiData.ToArray());
            UpdateCacheTimer(apiResponse.Responses.First());
        }

        public void UpdateCorporationMiningLedger(int corporationId)
        {
            var token = GetTokenCorporation(corporationId, EsiCorporationScopes.IndustryMiningRead,
                                            new[] {RolesEnum.Accountant});
            if (token == null) {
                return;
            }

            var observers = ApiIndustry.GetMiningObservers(corporationId, GetAccessToken(token));
            foreach (var observer in observers.Items) {
                var endpoint = RequestHelper.GetEndpointUrl<CorporationMiningObserverLedgerModel>()
                                            .Replace("{corporation_id}", $"{corporationId}")
                                            .Replace("{observer_id}",    $"{observer.ObserverId}");
                if (IsCacheExpired(endpoint) == false) {
                    continue;
                }

                var apiResponse =
                    ApiIndustry.GetMiningLedgerCorp(corporationId, observer.ObserverId, GetAccessToken(token));
                var apiData = apiResponse.Items;
                AddToDb(apiData.ToArray());
                UpdateCacheTimer(apiResponse.Responses.First());
            }
        }

        public IEnumerable<CorporationWalletJournalModel> GetCorporationWalletJournal(int corporationId)
        {
            UpdateCorporationWalletJournal(corporationId);
            for (var division = 1; division <= 7; division++) {
                var divisionJournal =
                    GetFromDb<CorporationWalletJournalModel>(c => (c.CorporationId == corporationId) &&
                                                                  (c.Division      == division));
                foreach (var entry in divisionJournal) {
                    yield return entry;
                }
            }

            // // TODO: Move to separate Update method.
            // var token = GetTokenCorporation(corporationId, EsiCorporationScopes.WalletsRead,
            //                                 new[] {RolesEnum.Accountant, RolesEnum.JuniorAccountant});
            // if (token == null) {
            //     yield break;
            // } // No valid tokens returned, return default.
            //
            // // Get wallet journal for each of seven divisions 
            // for (var division = 1; division <= 7; division++) {
            //     var endpoint = RequestHelper.GetEndpointUrl<CorporationWalletJournalModel>()
            //                                 .Replace("{corporation_id}", $"{corporationId}")
            //                                 .Replace("{division}",       $"{division}");
            //
            //     // If cache is not expired, retrieve data from database instead of api.
            //     if (IsCacheExpired(endpoint) == false) {
            //         var divisionJournal =
            //             GetFromDb<CorporationWalletJournalModel>(c => (c.CorporationId == corporationId) &&
            //                                                           (c.Division      == division));
            //         // Yield return retrieved entries.
            //         foreach (var entry in divisionJournal) {
            //             yield return entry;
            //         }
            //
            //         continue;
            //     }
            //
            //     // Otherwise update from api.
            //     var apiResponse =
            //         ApiWallet.GetCorporationWalletJournalCompleteWithInfo(corporationId, division,
            //                                                               GetAccessToken(token));
            //     var apiData = apiResponse.Items;
            //
            //     AddToDb(apiData.ToArray());
            //
            //
            //     UpdateCacheTimer(apiResponse.Responses.First());
            //
            //     foreach (var entry in apiData) {
            //         yield return entry;
            //     } // Yield return every entry.
        }

        public IEnumerable<CorporationWalletTransactionModel> GetCorporationWalletTransactions(int corporationId)
        {
            UpdateCorporationWalletTransactions(corporationId);
            for (var division = 1; division <= 7; division++) {
                var divisionJournal =
                    GetFromDb<CorporationWalletTransactionModel>(c => (c.CorporationId == corporationId) &&
                                                                      (c.Division      == division));
                foreach (var entry in divisionJournal) {
                    yield return entry;
                }
            }

            // // TODO: Move to separate Update method.
            // var token = GetTokenCorporation(corporationId, EsiCorporationScopes.WalletsRead,
            //                                 new[] {RolesEnum.Accountant, RolesEnum.JuniorAccountant});
            // if (token == null) {
            //     yield break;
            // } // No valid tokens returned, return default.
            //
            // // Get wallet journal for each of seven divisions 
            // for (var division = 1; division <= 7; division++) {
            //     var endpoint = RequestHelper.GetEndpointUrl<CorporationWalletTransactionModel>()
            //                                 .Replace("{corporation_id}", $"{corporationId}")
            //                                 .Replace("{division}",       $"{division}");
            //
            //     // If cache is not expired, retrieve data from database instead of api.
            //     if (IsCacheExpired(endpoint) == false) {
            //         var divisionJournal =
            //             GetFromDb<CorporationWalletTransactionModel>(c => (c.CorporationId == corporationId) &&
            //                                                               (c.Division      == division));
            //         // Yield return retrieved entries.
            //         foreach (var entry in divisionJournal) {
            //             yield return entry;
            //         }
            //
            //         continue;
            //     }
            //
            //     // Otherwise update from api.
            //     var apiResponse =
            //         ApiWallet.GetCorporationWalletTransactionsWithInfo(corporationId, division,
            //                                                            GetAccessToken(token));
            //     var apiData = apiResponse.Items;
            //     AddToDb(apiData.ToArray());
            //
            //     UpdateCacheTimer(apiResponse.Responses.First());
            //
            //     foreach (var entry in apiData) {
            //         yield return entry;
            //     } // Yield return every entry.
            // }
        }

        public IEnumerable<CorporationMarketOrdersHistory> GetCorporationMarketOrdersHistory(int corporationId)
        {
            UpdateCorporationMarketOrdersHistory(corporationId);
            var orderHistory = GetFromDb<CorporationMarketOrdersHistory>(c => c.CorporationId == corporationId);
            foreach (var entry in orderHistory) {
                yield return entry;
            }

            //
            // // TODO: Move to separate Update method.
            // var token = GetTokenCorporation(corporationId, EsiCorporationScopes.MarketsOrdersRead,
            //                                 new[] {RolesEnum.Accountant, RolesEnum.Trader});
            // if (token == null) {
            //     yield break;
            // }
            //
            //
            // var endpoint = RequestHelper.GetEndpointUrl<CorporationMarketOrdersHistory>()
            //                             .Replace("{corporation_id}", $"{corporationId}");
            //
            // // If cache is not expired, retrieve data from database instead of api.
            // if (IsCacheExpired(endpoint) == false) {
            //     var divisionOrdersHistory =
            //         GetFromDb<CorporationMarketOrdersHistory>(c => c.CorporationId == corporationId);
            //     // Yield return retrieved entries.
            //     foreach (var entry in divisionOrdersHistory) {
            //         yield return entry;
            //     }
            //
            //     yield break;
            // }
            //
            // // Otherwise update from api.
            // var apiResponse =
            //     ApiMarket.GetCorporationMarketOrdersHistoryWithInfo(corporationId, GetAccessToken(token));
            // var apiData = apiResponse.Items;
            // AddToDb(apiData.ToArray());
            // UpdateCacheTimer(apiResponse.Responses.First());
            // foreach (var entry in apiData) {
            //     yield return entry;
            // } // Yield return every entry.
        }

        public IEnumerable<CorporationMiningObserverLedgerModel> GetCorporationMiningLedger(int corporationId)
        {
            UpdateCorporationMiningLedger(corporationId);

            // TODO: Move observers into database
            var token = GetTokenCorporation(corporationId, EsiCorporationScopes.IndustryMiningRead,
                                            new[] {RolesEnum.Accountant});
            if (token == null) {
                yield break;
            }

            var observers = ApiIndustry.GetMiningObservers(corporationId, GetAccessToken(token));
            foreach (var observer in observers.Items) {
                var data = GetFromDb<CorporationMiningObserverLedgerModel>(c => (c.CorporationId == corporationId) &&
                                                                                (c.ObserverId ==
                                                                                 observer.ObserverId));
                foreach (var entry in data) {
                    yield return entry;
                }
            }

            //
            // // TODO: Move to separate Update method.
            // var token = GetTokenCorporation(corporationId, EsiCorporationScopes.IndustryMiningRead,
            //                                 new[] {RolesEnum.Accountant});
            // if (token == null) {
            //     yield break;
            // }
            //
            // var observers = ApiIndustry.GetMiningObservers(corporationId, GetAccessToken(token));
            // foreach (var observer in observers.Items) {
            //     var endpoint = RequestHelper.GetEndpointUrl<CorporationMiningObserverLedgerModel>()
            //                                 .Replace("{corporation_id}", $"{corporationId}")
            //                                 .Replace("{observer_id}",    $"{observer.ObserverId}");
            //     // If cache is not expired, retrieve data from database instead of api.
            //     if (IsCacheExpired(endpoint) == false) {
            //         var dbData =
            //             GetFromDb<CorporationMiningObserverLedgerModel>(c => (c.CorporationId == corporationId) &&
            //                                                                  (c.ObserverId == observer.ObserverId
            //                                                                  ));
            //         // Yield return retrieved entries.
            //         foreach (var entry in dbData) {
            //             yield return entry;
            //         }
            //
            //         continue;
            //     }
            //
            //     // Otherwise update from api.
            //     var apiResponse =
            //         ApiIndustry.GetMiningLedgerCorp(corporationId, observer.ObserverId, GetAccessToken(token));
            //     var apiData = apiResponse.Items;
            //     AddToDb(apiData.ToArray());
            //     UpdateCacheTimer(apiResponse.Responses.First());
            //     foreach (var entry in apiData) {
            //         yield return entry;
            //     }
            // }
        }

    #endregion

    #region SECTION Market

        public void UpdateMarketsRegionHistory(int regionId, int typeId)
        {
            var endpoint = "/v1/markets/{region_id}/history/".Replace("{region_id}", $"{regionId}");
            var expired  = IsCacheExpired(endpoint, $"{typeId}");
            if (expired == false) {
                return;
            }

            IRestResponse<List<MarketsRegionHistoryResponse>> apiResponse = null;
            // As of November 2018, there is an error in the API on the server side, which throws error upon
            //  requesting an otherwise valid TypeId entry. This try-catch is a baked-in workaround that allows
            //  the call to still be executed in case the error gets fixed in the future, but fail gracefully in
            //  the meantime.
            try {
                apiResponse = ApiMarket.GetMarketsRegionHistoryData(typeId: typeId, regionId: regionId);
            }
            catch (EsiException ex) {
                _logger.Error($"{ex.Message}");
                return;
            }

            var apiData = apiResponse.Data;
            UpdateCacheTimer(apiResponse);
            AddToDb(apiData.ToArray());
        }

        /// <summary>
        /// Retrieves regional market history for a given typeId.
        /// </summary>
        /// <param name="regionId">Id of the region to perform lookup on</param>
        /// <param name="typeId">Id of the type to perform lookup on</param>
        /// <returns>Iterable MarketRegionHistoryResponse model</returns>
        public IEnumerable<MarketsRegionHistoryResponse> GetMarketsRegionHistory(int regionId, int typeId)
        {
            UpdateMarketsRegionHistory(regionId, typeId);
            var dbData =
                GetFromDb<MarketsRegionHistoryResponse>(c => (c.RegionId == regionId) && (c.TypeId == typeId));
            foreach (var entry in dbData) {
                yield return entry;
            }
            // Previous code, proven to work. Remove only after the new one was proven to work without problems.
            //
            // var endpoint = "/v1/markets/{region_id}/history/".Replace("{region_id}", $"{regionId}");
            // // If cache is not expired, pull data from database.
            // var expired = IsCacheExpired(endpoint, $"{typeId}");
            // if (expired == false) {
            // var dbData =
            //     GetFromDb<MarketsRegionHistoryResponse>(c => (c.RegionId == regionId) && (c.TypeId == typeId));
            // foreach (var entry in dbData) {
            //     yield return entry;
            // }
            //
            //     yield break;
            // }

            //
            // IRestResponse<List<MarketsRegionHistoryResponse>> apiResponse = null;
            // try {
            //     apiResponse = ApiMarket.GetMarketsRegionHistoryData(typeId: typeId, regionId: regionId);
            // }
            // catch (EsiException ex) {
            //     _logger.Error($"{ex.Message}");
            //     yield break;
            // }
            //
            // var apiData = apiResponse.Data;
            // UpdateCacheTimer(apiResponse);
            // AddToDb(apiData.ToArray());
            // foreach (var entry in apiData) {
            //     yield return entry;
            // }
        }

    #endregion

    #endregion

    #region Esi Clients

        public readonly CommonApi      ApiCommon;
        public readonly CharacterApi   ApiCharacter;
        public readonly CorporationApi ApiCorporation;
        public readonly MarketApi      ApiMarket;
        public readonly IndustryApi    ApiIndustry;
        public readonly UniverseApi    ApiUniverse;
        public readonly WalletApi      ApiWallet;

    #endregion
    }
}
