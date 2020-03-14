using Crm.Contracts.Services;
using Crm.Domains.Data;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services
{
    public class CustomerHashService : ICustomerHashService
    {
        private readonly IRepository<CustomerHash> _customerHashRepository;

        private IQueryable<CustomerHash> DefaultQuery => _customerHashRepository.Query();

        public async Task<IEnumerable<CustomerHash>> GetCustomerHashes(int customerId, CancellationToken cancellationToken)
        {
            var query = from customerHash in DefaultQuery
                        where customerHash.CustomerId == customerId
                        select customerHash;

            return await _customerHashRepository
                    .For(query)
                    .ToArrayAsync(cancellationToken);
        }

        public async Task<CustomerHash> SaveCustomerHash(CustomerHash customerHash, bool saveChanges, CancellationToken cancellationToken)
        {
            return await _customerHashRepository.SaveChanges(customerHash, saveChanges, false, cancellationToken: cancellationToken);
        }

        public CustomerHash GetCustomerHash(IEnumerable<CustomerHash> customerHashes, IEnumerable<byte> hash)
        {
            var hashByteArray = hash.ToArray();
            var hashedString = Convert.ToBase64String(hashByteArray);
            return customerHashes.SingleOrDefault(customerHash => Convert.ToBase64String(customerHash.Hash) == hashedString);
        }

        public async Task<IEnumerable<Customer>> GetCustomersByHash(IEnumerable<IEnumerable<byte>> hashes, 
            CancellationToken cancellationToken,
            Action<IPagerResultOptions> configureOptions = default)
        {
            var hashBytes = hashes.Select(h => h.ToArray());

            var query = from customerHash in DefaultQuery
                        where hashes.Contains(customerHash.Hash)
                        select customerHash.Customer;

            if(configureOptions == null)
                return await _customerHashRepository
                        .For(query)
                        .ToArrayAsync(cancellationToken);

            var pager =_customerHashRepository
                .For(query)
                .AsPager();

            return await pager.GetPagedItems(configureOptions, cancellationToken);
        }

        public async Task<int> CommitChanges(CancellationToken cancellationToken)
        {
            return await _customerHashRepository.Commit(true, cancellationToken);
        }

        public CustomerHashService(IRepository<CustomerHash> customerHashRepository)
        {
            _customerHashRepository = customerHashRepository;
        }
    }
}
