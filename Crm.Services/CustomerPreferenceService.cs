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
            if(getAll)
                return await Repository
                            .For(DefaultQuery)
                            .ToArrayAsync(cancellationToken);

            //A customer qualifies as having a preference
            //when the opt-in date is earlier than the next check in date.
            //and when the next check in date is in the future.

            return await Repository.For(from customerPreference in DefaultQuery
                   where customerPreference.OptInDate.HasValue 
                   && customerPreference.OptInDate < customerPreference.NextCheckInDate 
                   && customerPreference.NextCheckInDate > toDate
                   select customerPreference).ToArrayAsync(cancellationToken);
        }

        public async Task<CustomerPreference> Save(CustomerPreference customerPreference, bool saveChanges, 
            bool detach, CancellationToken cancellationToken)
        {
            return await Repository.SaveChanges(customerPreference, saveChanges, detach, cancellationToken);
        }
    }
}
