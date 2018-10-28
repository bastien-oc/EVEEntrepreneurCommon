using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Api.SystemModels
{
    public class HttpMessageError
    {
        [J("error")] public string Error { get; set; }
        [J("error_description")] public string ErrorDescription { get; set; }
    }
}
