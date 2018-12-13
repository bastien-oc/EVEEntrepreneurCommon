using System.Collections.Generic;
using EntrepreneurCommon.Client;
using EntrepreneurCommon.Common.Attributes;
using EntrepreneurCommon.ExtensionMethods;
using EntrepreneurCommon.Helpers;
using EntrepreneurCommon.Models.EsiResponseModels;
using RestSharp;

namespace EntrepreneurCommon.Api
{
    public class MarketApi : CommonApi
    {
        public MarketApi(IEsiRestClient client) : base(client) { }

        /// <summary>
        ///     List market orders placed by a character
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public IRestResponse<List<CharacterMarketOrdersModel>> GetCharacterOrdersData(int characterId, string token)
        {
            var request = RequestHelper.GetRestRequest<CharacterMarketOrdersModel>(token)
                                       .SetCharacterId(characterId);
            return Client.Execute<List<CharacterMarketOrdersModel>>(request);
        }

        /// <summary>
        ///     List market orders placed by a character
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public IEnumerable<CharacterMarketOrdersModel> GetCharacterOrders(int characterId, string token)
        {
            return GetCharacterOrdersData(characterId, token).Data;
        }

        /// <summary>
        ///     List market orders placed on behalf of a corporation
        /// </summary>
        /// <param name="corporationId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public IRestResponse<List<CorporationMarketOrdersModel>> GetCorporationOrdersData(int    corporationId,
                                                                                          string token)
        {
            var request = RequestHelper.GetRestRequest<CorporationMarketOrdersModel>(token)
                                       .SetCorporationId(corporationId);
            return Client.Execute<List<CorporationMarketOrdersModel>>(request);
        }

        /// <summary>
        ///     List market orders placed on behalf of a corporation
        /// </summary>
        /// <param name="corporationId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public IEnumerable<CorporationMarketOrdersModel> GetCorporationOrders(int corporationId, string token)
        {
            return GetCorporationOrdersData(corporationId, token).Data;
        }


        /// <summary>
        ///     Return a list of historical market statistics for the specified type in a region
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public IRestResponse<List<MarketsRegionHistoryResponse>> GetMarketsRegionHistoryData(int regionId, int typeId)
        {
            var request = RequestHelper.GetRestRequest<MarketsRegionHistoryResponse>()
                                       .AddParameter("region_id", regionId, ParameterType.UrlSegment)
                                       .AddParameter("type_id",   typeId,   ParameterType.QueryString);
            var response = Client.Execute<List<MarketsRegionHistoryResponse>>(request);
            foreach (var e in response.Data) {
                e.AssignAnnotationFields(request);
            }

            return response;
        }

        /// <summary>
        ///     Return a list of historical market statistics for the specified type in a region
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public IEnumerable<MarketsRegionHistoryResponse> GetMarketsRegionHistory(int typeId, int regionId)
        {
            var response = GetMarketsRegionHistoryData(regionId, typeId);
            return response.Data;
        }

        /// <summary>
        ///     Return a list of orders in a region
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="regionId"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public IRestResponse<List<MarketsRegionOrderResponse>> GetMarketsRegionOrdersData(int    typeId,
                                                                                          int    regionId,
                                                                                          string orderType = "all")
        {
            var request = RequestHelper.GetRestRequest<MarketsRegionOrderResponse>()
                                       .AddParameter("region_id",  regionId,  ParameterType.UrlSegment)
                                       .AddParameter("type_id",    typeId,    ParameterType.QueryString)
                                       .AddParameter("order_type", orderType, ParameterType.QueryString);
            return Client.Execute<List<MarketsRegionOrderResponse>>(request);
        }

        /// <summary>
        ///     Return a list of orders in a region
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="regionId"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public IEnumerable<MarketsRegionOrderResponse> GetMarketsRegionOrders(int    typeId,
                                                                              int    regionId,
                                                                              string orderType = "all")
        {
            var response = GetMarketsRegionOrdersData(typeId, regionId, orderType);
            foreach (var entry in response.Data) {
                entry.RegionId = regionId;
                yield return entry;
            }
        }

        /// <summary>
        ///     Return a list of type IDs that have active orders in the region, for efficient market indexing.
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public EsiPaginatedResponse<int> GetMarketTypesData(int regionId)
        {
            var endpoint = "/v1/markets/{region_id}/types/";
            var request  = new RestRequest(endpoint).AddParameter("region_id", regionId, ParameterType.UrlSegment);
            var response = Client.ExecutePaginated<int>(request);
            return response;
        }

        /// <summary>
        ///     Return a list of type IDs that have active orders in the region, for efficient market indexing.
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetMarketTypes(int regionId)
        {
            var response = GetMarketTypesData(regionId);
            return response.Items;
        }


        /// <summary>
        ///     Get a list of item groups
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> GetMarketGroups()
        {
            var request  = new RestRequest("/v1/markets/groups/");
            var response = Client.Execute<List<int>>(request);
            return response.Data;
        }

        /// <summary>
        ///     Get information on an item group
        /// </summary>
        /// <param name="marketGroupId"></param>
        /// <returns></returns>
        public MarketsMarketGroupResponse GetMarketGroupInfo(int marketGroupId)
        {
            var request = RequestHelper.GetRestRequest<MarketsMarketGroupResponse>()
                                       .AddParameter("market_group_id", marketGroupId);
            return Client.Execute<MarketsMarketGroupResponse>(request).Data;
        }


        /// <summary>
        ///     Return all orders in a structure
        /// </summary>
        /// <param name="structureId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public EsiPaginatedResponse<MarketsStructureOrderResponse> GetMarketsStructureOrdersData(long   structureId,
                                                                                                 string token)
        {
            var request = RequestHelper.GetRestRequest<MarketsStructureOrderResponse>(token)
                                       .AddParameter("structure_id", structureId);
            return Client.ExecutePaginated<MarketsStructureOrderResponse>(request);
        }

        /// <summary>
        ///     Return all orders in a structure
        /// </summary>
        /// <param name="structureId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public IEnumerable<MarketsStructureOrderResponse> GetMarketsStructureOrders(long structureId, string token)
        {
            var response = GetMarketsStructureOrdersData(structureId, token);
            return response.Items;
        }

        /// <summary>
        ///     Return a list of prices
        /// </summary>
        /// <returns></returns>
        public IRestResponse<List<MarketsPriceResponse>> GetMarketPricesData()
        {
            var request  = new RestRequest(MarketsPriceResponse.Endpoint);
            var response = Client.Execute<List<MarketsPriceResponse>>(request);
            return response;
        }

        /// <summary>
        ///     Return a list of prices
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MarketsPriceResponse> GetMarketPrices()
        {
            return GetMarketPricesData().Data;
        }

        public EsiPaginatedResponse<CorporationMarketOrdersHistory> GetCorporationMarketOrdersHistoryWithInfo(
            int    corporationId,
            string token)
        {
            var request = RequestHelper.GetRestRequest<CorporationMarketOrdersHistory>(token)
                                       .SetCorporationId(corporationId);
            var response = Client.ExecutePaginated<CorporationMarketOrdersHistory>(request);
            return response;
        }

        public IEnumerable<CorporationMarketOrdersHistory> GetCorporationMarketOrderHistory(int corporationId, string token)
        {
            var response = GetCorporationMarketOrdersHistoryWithInfo(corporationId, token);
            return response.Items;
        }

        public EsiPaginatedResponse<CharacterMarketOrdersHistoryModel> GetCharacterMarketOrdersHistoryA(
            int    characterId,
            string token)
        {
            var request = RequestHelper.GetRestRequest<CharacterMarketOrdersHistoryModel>(token)
                                       .SetCharacterId(characterId);
            var response = Client.ExecutePaginated<CharacterMarketOrdersHistoryModel>(request);
            return response;
        }

        public IEnumerable<CharacterMarketOrdersHistoryModel> GetCharacterMarketOrdersHistory(int characterId, string token)
        {
            return GetCharacterMarketOrdersHistoryA(characterId, token).Items;
        }
    }
}
