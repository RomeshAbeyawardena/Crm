using AutoMapper;
using Crm.Domains.Response;
using Crm.Domains.ViewModels;
using MediatR;
using System;

namespace Crm.Domains.Request
{
    [AutoMap(typeof(GetCustomerPreferencesViewModel))]
    public class GetCustomerPreferencesRequest : IRequest<GetCustomerPreferenceResponse>
    {
        public string EmailAddress { get; set; }
        public DateTimeOffset ToDate { get; set; }
    }
}
