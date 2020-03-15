using Crm.Domains.Response;
using MediatR;
using System;

namespace Crm.Domains.Request
{
    public class GetCustomerPreferencesRequest : IRequest<GetCustomerPreferenceResponse>
    {
        public string EmailAddress { get; set; }
        public DateTimeOffset ToDate { get; set; }
    }
}
