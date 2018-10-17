namespace EntrepreneurCommon.Models.Esi
{
    public class CharacterMarketOrders:EntityMarketOrders
    {
        public static string Endpoint => "/v1/characters/{character_id}/orders/";
    }
}
