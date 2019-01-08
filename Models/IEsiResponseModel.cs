using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurCommon.Common.Attributes;

namespace EntrepreneurCommon.Common
{
    /// <summary>
    /// Marks the class as being mapped as a response to a specific endpoint.
    /// </summary>
    public interface IEsiResponseModel : IEsiAnnotatedRecord { }
}