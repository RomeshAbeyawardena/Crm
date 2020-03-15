using Crm.Domains.Response;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Request
{
    public class SaveCustomerPreferencesRequest : IRequest<SaveCustomerPreferencesResponse>
    {
        public int CustomerId { get; set; }
        public IDictionary<string, bool> CustomerPreferences { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset NextCheckInDate { get; set; }
    }
}
