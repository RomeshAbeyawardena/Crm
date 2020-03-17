using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Contracts
{
    public interface IMethodInfo
    {
        string OperationId { get; }
        IEnumerable<string> Tags { get; set; }
        IEnumerable<IEndPointParameter> Parameters { get; set; }
        IEnumerable<IResponse> Responses { get; set; }
    }
}
