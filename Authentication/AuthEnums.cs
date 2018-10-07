using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        RefreshToken
    }
}