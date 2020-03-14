using Crm.Domains.Data;
using DNI.Core.Contracts.Options;
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

        CustomerHash GetCustomerHash(IEnumerable<CustomerHash> customerHashes, IEnumerable<byte> hash);

        Task<IEnumerable<Customer>> GetCustomersByHash(IEnumerable<IEnumerable<byte>> hashes, 
            CancellationToken cancellationToken,
            Action<IPagerResultOptions> pagerResultOptions = default);
        Task<int> CommitChanges(CancellationToken cancellationToken);
    }
}
