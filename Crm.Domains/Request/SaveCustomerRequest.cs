using Crm.Domains.Response;
using MediatR;

namespace Crm.Domains.Request
{
    public class SaveCustomerRequest : IRequest<SaveCustomerResponse>
    {
        public int? Id { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
    }
}
