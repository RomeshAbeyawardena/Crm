using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace Crm.Domains.ViewModels
{
    public class SaveCustomerPreferencesViewModel
    {
        public Dictionary<string, bool> Preferences { get; set; } 
        
        public int CustomerId { get; set; }
        public IDictionary<string, bool> CustomerPreferences { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset NextCheckInDate { get; set; }
    }
}
