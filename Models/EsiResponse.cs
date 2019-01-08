using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace EntrepreneurCommon.Models
{
    public class EsiResponse<T>
    {
        public IRestResponse<T> Response { get; set; }
        public T Data { get; set; }
    }
}
