using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Contracts
{
    public interface IPagedRequest
    {
        int PageNumber { get; set; }
        int MaximumRowsPerPage { get; set; }
    }
}
