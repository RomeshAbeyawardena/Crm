using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Contracts
{
    public interface IEndPoint
    {
        IEnumerable<IMethodInfo> Method { get; set; }
        string RelativeUrl { get; set; }

    }
}
