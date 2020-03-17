using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Contracts
{
    public interface IStatusCodeResult
    {
        string Description { get; set; }
        string Content { get; set; }
    }
}
