using Crm.Contracts.Services;
using Crm.Domains.Data;
using DNI.Core.Contracts;
using DNI.Core.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services
{
    public class CustomerPreferenceService : DataServiceBase<CustomerPreference>, ICustomerPreferenceService
    {
        public CustomerPreferenceService(IRepository<CustomerPreference> repository) 
            : base(repository, true)
        {
        }

        public async Task CommitChanges(CancellationToken cancellationToken)
        {
            await Repository.Commit(true, cancellationToken);
        }

        public CustomerPreference GetCustomerPreference(IEnumerable<CustomerPreference> customerPreferences, int preferenceId)
        {
            return customerPreferences.SingleOrDefault(customerPreference => customerPreference.PreferenceId == preferenceId);
        }

        public async Task<IEnumerable<CustomerPreference>> GetCustomerPreferences(int id, DateTimeOffset toDate, 
            CancellationToken cancellationToken, bool getAll = false)
        {
            return await Repository.For(from customerPreference in DefaultQuery
                   where (getAll || !customerPreference.OptInDate.HasValue 
                    || customerPreference.OptInDate < toDate) 
                   && getAll || customerPreference.NextCheckInDate > toDate
                   select customerPreference).ToArrayAsync(cancellationToken);
        }

        public async Task<CustomerPreference> Save(CustomerPreference customerPreference, bool saveChanges, 
            bool detach, CancellationToken cancellationToken)
        {
            return await Repository.SaveChanges(customerPreference, saveChanges, detach, cancellationToken);
        }
    }
}
