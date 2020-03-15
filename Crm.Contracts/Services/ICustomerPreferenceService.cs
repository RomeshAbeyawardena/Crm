using Crm.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Contracts.Services
{
    public interface ICustomerPreferenceService
    {
        Task<IEnumerable<CustomerPreference>> GetCustomerPreferences(int id, DateTimeOffset toDate, 
            CancellationToken cancellationToken, bool getAll = false);
        CustomerPreference GetCustomerPreference(IEnumerable<CustomerPreference> customerPreferences, int preferenceId);
        Task<CustomerPreference> Save(CustomerPreference customerPreference, bool v, CancellationToken cancellationToken);
    }
}
