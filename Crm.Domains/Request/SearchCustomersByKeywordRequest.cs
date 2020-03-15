using Crm.Domains.Contracts;
using Crm.Domains.Response;
using MediatR;

namespace Crm.Domains.Request
{
    public class SearchCustomersByKeywordRequest : IRequest<SearchCustomersByKeywordResponse>, IPagedRequest
    {
        public string Keyword { get; set; }
        public int PageNumber { get; set; }
        public int MaximumRowsPerPage { get; set; }
    }
}
