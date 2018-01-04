using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurEsiApi.Models.EsiResponseModels;
using EntrepreneurEsiApi.Api;
using EntrepreneurEsiApi.Models.Esi;
using RestSharp;

namespace EntrepreneurEsiApi.Api
{
    public class MarketApi:CommonApi
    {
        public MarketApi( EsiApiClient apiClient ) : base(apiClient)
        {
        }

        /// <summary>
        /// List market orders placed by a character
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public IRestResponse<List<EntityMarketOrders>> GetCharacterOrdersData(int characterId, string token)
        {
            var request = new RestRequest(CharacterMarketOrders.Endpoint);
            request.AddParameter("character_id", characterId, ParameterType.UrlSegment);
            request.AddParameter("token", token, ParameterType.QueryString);
            var response = ApiClient.Execute<List<EntityMarketOrders>>(request);
            return response;
        }
        /// <summary>
        /// List market orders placed by a character
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public IEnumerable<EntityMarketOrders> GetCharacterOrders(int characterId, string token)
        {
            return GetCharacterOrdersData(characterId, token).Data;
        }

        /// <summary>
        /// List market orders placed on behalf of a corporation
        /// </summary>
        /// <param name="corporationId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public IRestResponse<List<EntityMarketOrders>> GetCorporationOrdersData( int corporationId, string token )
        {
            var request = new RestRequest(CorporationMarketOrders.Endpoint);
            request.AddParameter("corporation_id", corporationId, ParameterType.UrlSegment);
            request.AddParameter("token", token, ParameterType.QueryString);
            var response = ApiClient.Execute<List<EntityMarketOrders>>(request);
            return response;
        }
        /// <summary>
        /// List market orders placed on behalf of a corporation
        /// </summary>
        /// <param name="corporationId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public IEnumerable<EntityMarketOrders> GetCorporationOrders(int corporationId, string token)
        {
            return GetCorporationOrdersData(corporationId, token).Data;
        }



        /// <summary>
        /// Return a list of historical market statistics for the specified type in a region
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public IRestResponse<List<MarketsRegionHistoryResponse>> GetMarketsRegionHistoryData( int typeId, int regionId )
        {
            var request = new RestRequest(MarketsRegionHistoryResponse.Endpoint);
            request.AddParameter("region_id", regionId, ParameterType.UrlSegment);
            request.AddParameter("type_id", typeId, ParameterType.QueryString);
            var response = ApiClient.Execute<List<MarketsRegionHistoryResponse>>(request);
            foreach (var e in response.Data) {
                e.TypeId = typeId;
                e.RegionId = regionId;
            }
            return response;
        }
        /// <summary>
        /// Return a list of historical market statistics for the specified type in a region
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public IEnumerable<MarketsRegionHistoryResponse> GetMarketsRegionHistory( int typeId, int regionId )
        {
            var response = GetMarketsRegionHistoryData(typeId, regionId);
            return response.Data;
        }
        
        /// <summary>
        /// Return a list of orders in a region
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="regionId"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public IRestResponse<List<MarketsRegionOrderResponse>> GetMarketsRegionOrdersData( int typeId, int regionId, string orderType = "all" )
        {
            var request = new RestRequest(MarketsRegionOrderResponse.Endpoint);
            request.AddParameter("region_id", regionId, ParameterType.UrlSegment);
            request.AddParameter("type_id", typeId, ParameterType.QueryString);
            request.AddParameter("order_type", orderType, ParameterType.QueryString);
            var response = ApiClient.Execute<List<MarketsRegionOrderResponse>>(request);
            return response;
        }
        /// <summary>
        /// Return a list of orders in a region
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="regionId"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public IEnumerable<MarketsRegionOrderResponse> GetMarketsRegionOrders( int typeId, int regionId, string orderType = "all" )
        {
            var response = GetMarketsRegionOrdersData(typeId, regionId, orderType);
            foreach (var entry in response.Data) {
                entry.RegionId = regionId;
                yield return entry;
            }
        }

        /// <summary>
        /// Return a list of type IDs that have active orders in the region, for efficient market indexing.
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public EsiPaginatedResponse<int> GetMarketTypesData( int regionId )
        {
            string endpoint = "/v1/markets/{region_id}/types/";
            var request = new RestRequest(endpoint);
            request.AddParameter("region_id", regionId, ParameterType.UrlSegment);
            var response = ApiClient.ExecutePaginated<int>(request);
            return response;
        }
        /// <summary>
        /// Return a list of type IDs that have active orders in the region, for efficient market indexing.
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetMarketTypes( int regionId )
        {
            var response = GetMarketTypesData(regionId);
            return response.Items;
        }


        /// <summary>
        /// Get a list of item groups
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> GetMarketGroups()
        {
            var request = new RestRequest("/v1/markets/groups/");
            var response = ApiClient.Execute<List<int>>(request);
            return response.Data;
        }
        /// <summary>
        /// Get information on an item group
        /// </summary>
        /// <param name="marketGroupId"></param>
        /// <returns></returns>
        public MarketsMarketGroupResponse GetMarketGroupInfo(int marketGroupId)
        {
            var request = new RestRequest(MarketsMarketGroupResponse.Endpoint);
            request.AddParameter("market_group_id",marketGroupId);
            var response = ApiClient.Execute<MarketsMarketGroupResponse>(request);
            return response.Data;
        }


        /// <summary>
        /// Return all orders in a structure
        /// </summary>
        /// <param name="structureId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public EsiPaginatedResponse<MarketsStructureOrderResponse> GetMarketsStructureOrdersData( Int64 structureId, string token )
        {
            var request = new RestRequest(MarketsRegionOrderResponse.Endpoint);
            request.AddParameter("structure_id", structureId, ParameterType.UrlSegment);
            request.AddParameter("token", token, ParameterType.QueryString);
            var response = ApiClient.ExecutePaginated<MarketsStructureOrderResponse>(request);
            return response;
        }
        /// <summary>
        /// Return all orders in a structure
        /// </summary>
        /// <param name="structureId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public IEnumerable<MarketsStructureOrderResponse> GetMarketsStructureOrders(Int64 structureId, string token)
        {
            var response = GetMarketsStructureOrdersData(structureId, token);
            return response.Items;
        }

        /// <summary>
        /// Return a list of prices
        /// </summary>
        /// <returns></returns>
        public IRestResponse<List<MarketsPriceResponse>> GetMarketPricesData()
        {
            var request = new RestRequest(MarketsPriceResponse.Endpoint);
            var response = ApiClient.Execute<List<MarketsPriceResponse>>(request);
            return response;
        }
        /// <summary>
        /// Return a list of prices
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MarketsPriceResponse> GetMarketPrices()
        {
            return GetMarketPricesData().Data;
        }


    }
}
