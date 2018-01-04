using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace EntrepreneurEsiApi.Api.SystemModels
{
    public class EsiException:Exception {
        public int ErrorCode { get; set; }
        public dynamic ErrorContent { get; private set; }

        public EsiException() {}
        public EsiException(int errorCode, string message ) : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public EsiException(int errorCode, string message, dynamic errorContent = null) : base(message)
        {
            this.ErrorCode = errorCode;
            this.ErrorContent = errorContent;
        }
    }

    public static class EsiErrors
    {
        public static EsiErrorForbidden GetForbidden( string Json )
        {
            return JsonConvert.DeserializeObject<EsiErrorForbidden>(Json);
        }

        public static EsiInternalServerError GetInternalServerError( string Json )
        {
             return JsonConvert.DeserializeObject<EsiInternalServerError>(Json);
        }
    }

    class EsiInternalServerErrorException:Exception
    {
        public EsiInternalServerErrorException()
        {
        }

        public EsiInternalServerErrorException( string message ) : base(message)
        {
        }

        public EsiInternalServerErrorException( string message, Exception innerException ) : base(message, innerException)
        {
        }

        protected EsiInternalServerErrorException( SerializationInfo info, StreamingContext context ) : base(info, context)
        {
        }
    }

    class EsiForbiddenException:Exception
    {
        public EsiForbiddenException()
        {
        }

        public EsiForbiddenException( string message ) : base(message)
        {
        }
    }

    public class EsiInternalServerError
    {
        [J("error")] public string Error { get; set; }
    }

    public class EsiErrorForbidden
    {
        [J("error")] public string Error { get; set; }
        [J("sso_status")] public int SsoStatus { get; set; }
    }
}
