namespace EntrepreneurCommon.Authentication
{
    public enum EnumNeedsRefreshing
    {
        Yes,
        No,
        Invalid
    }

    public enum TokenAuthenticationType
    {
        VerifyAuthCode,
        RefreshToken,
        AccessToken
    }
}