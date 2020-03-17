using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Contracts
{
    public interface IEndPointParameter
    {
        string Name { get; set; }
        string In { get; set; }
        bool Required { get; set; }
        string Description { get; set; }
        IParameterSchema Schema { get; set; }
    }
}
