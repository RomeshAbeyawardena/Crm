using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Contracts
{
    public interface IDocumentInfo
    {
        string Version { get; set; } 
        string Title { get; set; } 
        string License { get; set; }
    }
}
