using AutoMapper;
using Crm.Domains.Response;
using Crm.Domains.ViewModels;
using MediatR;

namespace Crm.Domains.Request
{
    [AutoMap(typeof(GetCustomerAttributeViewModel))]
    public class GetCustomerAttributeRequest : IRequest<GetCustomerAttributesResponse>
    {
        public int CustomerId { get; set; }
    }
}
