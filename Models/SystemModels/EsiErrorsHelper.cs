using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using RestSharp;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurCommon.Api.SystemModels
{
    public class EsiException : Exception
    {
        public int ErrorCode { get; set; }
        public IRestResponse Response { get; internal set; }

        public EsiException() { }

        public EsiException(int errorCode, IRestResponse response) : base(GetErrorFromModel(response))
        {
            ErrorCode = errorCode;
            Response = response;
        }

        public static string GetErrorFromModel(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<EsiInternalServerError>(response.Content).Error;
        }

        // public EsiException(int errorCode, string message, Exception innerException, IRestResponse response) : base(message)
        // {
        //     this.ErrorCode = errorCode;
        //     if (String.IsNullOrEmpty(message)) {
        //         var _message = JsonConvert.DeserializeObject<EsiInternalServerError>(response.Content).Error;
        //         return new EsiException(errorCode, _message, innerException, response);
        //     }
        //     this.Data.Add("Response", JsonConvert.SerializeObject(response));
        // }
    }

    public static class EsiErrorsHelper
    {
        public static EsiErrorForbidden GetForbidden(string Json)
        {
            return JsonConvert.DeserializeObject<EsiErrorForbidden>(Json);
        }

        public static EsiInternalServerError GetInternalServerError(string Json)
        {
            return JsonConvert.DeserializeObject<EsiInternalServerError>(Json);
        }
    }

    class EsiInternalServerErrorException : Exception
    {
        public EsiInternalServerErrorException() { }

        public EsiInternalServerErrorException(string message) : base(message) { }

        public EsiInternalServerErrorException(string message, Exception innerException) :
            base(message, innerException) { }

        protected EsiInternalServerErrorException(SerializationInfo info, StreamingContext context) :
            base(info, context) { }
    }

    class EsiForbiddenException : Exception
    {
        public EsiForbiddenException() { }

        public EsiForbiddenException(string message) : base(message) { }
    }

    public class EsiInternalServerError
    {
        [J("error")] public string Error { get; set; }
    }

    public class EsiErrorForbidden
    {
        [J("error")]      public string Error     { get; set; }
        [J("sso_status")] public int    SsoStatus { get; set; }
    }
}
