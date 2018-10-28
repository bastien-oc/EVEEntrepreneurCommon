using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntrepreneurCommon.Common
{
    public class EsiRecordAnnotationAttribute : Attribute
    {
        public string Name;

        public EsiRecordAnnotationAttribute(string name)
        {
            this.Name = name;
        }
    }
}
