using Crm.Domains.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Request
{
    public class SearchCustomersByKeywordRequest : IRequest<SearchCustomersByKeywordResponse>
    {
        public string Keyword { get; set; }
    }
}
