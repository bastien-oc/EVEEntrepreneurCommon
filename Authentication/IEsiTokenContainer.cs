using System.Threading.Tasks;
using EntrepreneurEsiApi.Authentication;

namespace EntrepreneurCommon.Authentication {
    /// <summary>
    /// Interface that ensures the object contains all the necessary fields for valid API calls and nothing more.
    /// </summary>
    public interface IEsiTokenContainer: IEsiTokenVerification, IEsiTokenResponse
    {
        EsiAuthClient AuthClient { get; set; }
        Task<string> GetAccessToken(EsiAuthClient client = null);
        EnumNeedsRefreshing NeedsRefreshing();
        bool CheckScope(string scope);
    }
}