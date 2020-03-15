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
        Task<IEnumerable<CustomerPreference>> GetCustomerPreferences(int customerId, DateTimeOffset toDate, 
            CancellationToken cancellationToken, bool getAll = false);
        Task<IEnumerable<CustomerPreference>> GetElapsedCustomerPreferences(int customerId, DateTimeOffset toDate,
            CancellationToken cancellationToken);
        CustomerPreference GetCustomerPreference(IEnumerable<CustomerPreference> customerPreferences, int preferenceId);
        Task<CustomerPreference> Save(CustomerPreference customerPreference, bool saveChanges,
            bool detach, CancellationToken cancellationToken);
        Task CommitChanges(CancellationToken cancellationToken);
    }
}
