using AutoMapper;
using Crm.Domains.Response;
using Crm.Domains.ViewModels;
using MediatR;

namespace Crm.Domains.Request
{
    [AutoMap(typeof(VerifyCustomerCredentialsViewModel))]
    public class VerifyCustomerCredentialsRequest : IRequest<VerifyCustomerCredentialsResponse>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
