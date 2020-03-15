using Crm.Domains.Data;
using DNI.Core.Domains;

namespace Crm.Domains.Response
{
    public class SaveCustomerPreferencesResponse : ResponseBase<CustomerPreference>
    {
        public int TotalOptedIn { get; set; }
        public int TotalOptedOut { get; set; }
        public string[] InalidPreferences { get; set; }
    }
}
