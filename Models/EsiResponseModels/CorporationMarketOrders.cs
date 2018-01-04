namespace EntrepreneurEsiApi.Models.Esi
{
    public class CorporationMarketOrders:EntityMarketOrders
    {
        public static string Endpoint => "/v1/corporations/{corporation_id}/orders/";
    }
}
