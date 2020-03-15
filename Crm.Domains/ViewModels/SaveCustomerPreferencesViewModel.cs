using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace Crm.Domains.ViewModels
{
    public class SaveCustomerPreferencesViewModel
    {
        public Dictionary<string, bool> Preferences { get; set; } 
    }
}
