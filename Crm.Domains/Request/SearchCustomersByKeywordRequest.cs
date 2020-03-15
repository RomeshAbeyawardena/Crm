using AutoMapper;
using Crm.Domains.Contracts;
using Crm.Domains.Response;
using Crm.Domains.ViewModels;
using MediatR;

namespace Crm.Domains.Request
{
    [AutoMap(typeof(SearchCustomersByKeywordViewModel))]
    public class SearchCustomersByKeywordRequest : IRequest<SearchCustomersByKeywordResponse>, IPagedRequest
    {
        public string Keyword { get; set; }
        public int PageNumber { get; set; }
        public int MaximumRowsPerPage { get; set; }
    }
}
