using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurCommon.Authentication;
using EntrepreneurEsiApi.Authentication;
using EntrepreneurEsiApi.Models.Esi;
using RestSharp;

namespace EntrepreneurCommon.Models {
    public class EsiTokenExtended
    {
        public IEsiTokenContainer Token { get; set; }
    }
}
