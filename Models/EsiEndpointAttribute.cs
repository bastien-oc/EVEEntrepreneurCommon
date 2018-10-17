using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntrepreneurCommon.Models
{
    class EsiEndpointAttribute : Attribute
    {
        public EsiEndpointAttribute(string path)
        {
            Path = path;
        }
        public String Path { get; set; }
    }
}
