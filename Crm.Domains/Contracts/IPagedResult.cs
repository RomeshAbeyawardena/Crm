using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Contracts
{
    public interface IPagedResult
    {
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
    }
}
