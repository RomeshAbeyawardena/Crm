using AutoMapper;
using Crm.Domains.Response;
using Crm.Domains.ViewModels;
using MediatR;

namespace Crm.Domains.Request
{
    [AutoMap(typeof(SaveCustomerAttributeViewModel))]
    public class SaveCustomerAttributeRequest : IRequest<SaveCustomerAttributeResponse>
    {
        public int CustomerId { get; set; }
        public string Property { get; set; }
        public string Value { get; set; }
    }
}
