using Crm.Domains.Response;
using MediatR;

namespace Crm.Domains.Request
{
    public class VerifyCustomerCredentialsRequest : IRequest<VerifyCustomerCredentialsResponse>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
