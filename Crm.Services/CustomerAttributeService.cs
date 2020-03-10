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
    public class CustomerAttributeService : ICustomerAttributeService
    {
        private readonly IRepository<CustomerAttribute> _customerAttributeRepository;
        private IQueryable<CustomerAttribute> DefaultCustomerAttributeQuery => _customerAttributeRepository.Query();
        private IQueryable<CustomerAttribute> DefaultCustomerAttributeFilteredQuery(int customerId) 
            => from customerAttribute in DefaultCustomerAttributeQuery
                            where customerAttribute.CustomerId == customerId
                            select customerAttribute;

        public async Task<CustomerAttribute> GetCustomerAttribute(int attributeId, int customerId, CancellationToken cancellationToken)
        {
            var query = from customerAttribute in _customerAttributeRepository
                        .For(DefaultCustomerAttributeFilteredQuery(customerId))
                        .Include(customerAttribute => customerAttribute.Attribute)
                        where customerAttribute.AttributeId == attributeId
                        select customerAttribute;

            return await _customerAttributeRepository.For(query).ToSingleOrDefaultAsync(cancellationToken);
        }

        public async Task<CustomerAttribute> SaveCustomerAttribute(CustomerAttribute encryptedCustomerAttribute, CancellationToken cancellationToken)
        {
            return await _customerAttributeRepository.SaveChanges(encryptedCustomerAttribute, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<CustomerAttribute>> GetCustomerAttributes(int customerId, CancellationToken cancellationToken)
        {
            var query = DefaultCustomerAttributeFilteredQuery(customerId);

            return await _customerAttributeRepository
                .For(query)
                .ToArrayAsync(cancellationToken);
        }

        public CustomerAttributeService(IRepository<CustomerAttribute> customerAttributeRepository)
        {
            _customerAttributeRepository = customerAttributeRepository;
        }
    }
}
