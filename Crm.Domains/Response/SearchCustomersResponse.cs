using Crm.Domains.Contracts;
using Crm.Domains.Dto;
using DNI.Core.Domains;
using System.Collections.Generic;

namespace Crm.Domains.Response
{
    public class SearchCustomersResponse : ResponseBase<IEnumerable<Customer>>, IPagedResult
    {
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
    }
}
