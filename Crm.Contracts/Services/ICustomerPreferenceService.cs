using Crm.Domains.Data;
using DNI.Core.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Contracts.Services
{
    public interface ICustomerPreferenceService : IDataService<CustomerPreference>
    {
        Task<IEnumerable<CustomerPreference>> GetCustomerPreferences(int id, DateTimeOffset toDate, 
            CancellationToken cancellationToken, bool getAll = false);
        CustomerPreference GetCustomerPreference(IEnumerable<CustomerPreference> customerPreferences, int preferenceId);
        Task<CustomerPreference> Save(CustomerPreference customerPreference, bool v, CancellationToken cancellationToken);
        Task CommitChanges(CancellationToken cancellationToken);
    }
}
