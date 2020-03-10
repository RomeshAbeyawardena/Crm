using Crm.Contracts.Services;
using Crm.Domains.Data;
using DNI.Core.Contracts;
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
            return await _customerHashRepository.SaveChanges(customerHash, saveChanges, cancellationToken: cancellationToken);
        }

        public CustomerHash GetCustomerHash(IEnumerable<CustomerHash> customerHashes, string hash)
        {
            return customerHashes.SingleOrDefault(customerHash => customerHash.Hash == hash);
        }

        public async Task<IEnumerable<Customer>> GetCustomersByHash(IEnumerable<string> hashes, CancellationToken cancellationToken)
        {
            var query = from customerHash in DefaultQuery
                        where hashes.Contains(customerHash.Hash)
                        select customerHash.Customer;

            return await _customerHashRepository
                    .For(query)
                    .ToArrayAsync(cancellationToken);
        }

        public async Task<int> CommitChanges(CancellationToken cancellationToken)
        {
            return await _customerHashRepository.Commit(cancellationToken);
        }

        public CustomerHashService(IRepository<CustomerHash> customerHashRepository)
        {
            _customerHashRepository = customerHashRepository;
        }
    }
}
