using Crm.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Contracts.Services
{
    public interface ICustomerHashService
    {
        Task<IEnumerable<CustomerHash>> GetCustomerHashes(int customerId, CancellationToken cancellation);
        
        Task<CustomerHash> SaveCustomerHash(CustomerHash customerHash, bool saveChanges, CancellationToken cancellationToken);

        CustomerHash GetCustomerHash(IEnumerable<CustomerHash> customerHashes, string hash);

        Task<IEnumerable<Customer>> GetCustomersByHash(IEnumerable<string> hashes, CancellationToken cancellationToken);
        Task<int> CommitChanges(CancellationToken cancellationToken);
    }
}
