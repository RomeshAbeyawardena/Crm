using AutoMapper;
using Crm.Domains.Response;
using Crm.Domains.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;

namespace Crm.Domains.Request
{
    [AutoMap(typeof(SaveCustomerPreferencesViewModel))]
    public class SaveCustomerPreferencesRequest : IRequest<SaveCustomerPreferencesResponse>
    {
        public int CustomerId { get; set; }
        public IDictionary<string, bool> CustomerPreferences { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset NextCheckInDate { get; set; }
    }
}
