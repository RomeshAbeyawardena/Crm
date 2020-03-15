using Crm.Domains.Response;
using MediatR;

namespace Crm.Domains.Request
{
    public class GetCustomerAttributeRequest : IRequest<GetCustomerAttributesResponse>
    {
        public int CustomerId { get; set; }
    }
}
