using Crm.Domains.Data;
using DNI.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Response
{
    public class SaveCustomerPreferencesResponse : ResponseBase<CustomerPreference>
    {
        public int TotalOptedIn { get; set; }
    }
}
