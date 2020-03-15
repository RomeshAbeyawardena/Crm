using Crm.Domains.Response;
using MediatR;

namespace Crm.Domains.Request
{
    public class SaveCustomerAttributeRequest : IRequest<SaveCustomerAttributeResponse>
    {
        public int CustomerId { get; set; }
        public string Property { get; set; }
        public string Value { get; set; }
    }
}
