using Crm.Domains.Contracts;
using Crm.Domains.Response;
using MediatR;

namespace Crm.Domains.Request
{
    public class GetCustomerRequest : IRequest<GetCustomerResponse>, ICustomerIdentifier, ICustomer
    {
        public int? Id { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
