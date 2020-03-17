using Crm.Domains.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains
{
    public class Endpoint : IEndPoint
    {
        public IEnumerable<IMethodInfo> Method { get; set; }
        public string RelativeUrl { get; set; }
    }
}
