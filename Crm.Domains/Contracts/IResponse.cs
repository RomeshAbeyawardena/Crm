using Crm.Domains.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Contracts
{
    public interface IResponse
    {
        IDictionary<StatusCode, string> StatusCodeResults { get; set; }
    }
}
