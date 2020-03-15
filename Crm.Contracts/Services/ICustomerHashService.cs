using Crm.Domains.Data;
using Crm.Domains.Dto;
using DNI.Core.Contracts.Options;
using DNI.Core.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Contracts.Services
{
    public interface ICustomerHashService : IDataService<Domains.Data.CustomerHash>
    {
        Task<IEnumerable<CustomerHash>> GetCustomerHashes(int customerId, CancellationToken cancellation);
        
        Task<CustomerHash> SaveCustomerHash(CustomerHash customerHash, bool saveChanges, CancellationToken cancellationToken);

        CustomerHash GetCustomerHash(IEnumerable<CustomerHash> customerHashes, IEnumerable<byte> hash, int atIndex);

        Task<IEnumerable<Domains.Data.Customer>> GetCustomersByHash(IEnumerable<CharacterIndex> hashes, 
            CancellationToken cancellationToken,
            Action<IPagerResultOptions> pagerResultOptions = default);
        Task<int> CommitChanges(CancellationToken cancellationToken);
    }
}
