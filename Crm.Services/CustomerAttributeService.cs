using Crm.Contracts.Services;
using Crm.Domains.Data;
using DNI.Core.Contracts;
using DNI.Core.Services.Abstraction;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services
{
    public class CustomerAttributeService : DataServiceBase<CustomerAttribute>, ICustomerAttributeService
    {
        
        private IQueryable<CustomerAttribute> DefaultCustomerAttributeFilteredQuery(int customerId) 
            => from customerAttribute in DefaultQuery
                            where customerAttribute.CustomerId == customerId
                            select customerAttribute;

        public async Task<CustomerAttribute> GetCustomerAttribute(int attributeId, int customerId, CancellationToken cancellationToken)
        {
            var query = from customerAttribute in Repository
                        .For(DefaultCustomerAttributeFilteredQuery(customerId))
                        .Include(customerAttribute => customerAttribute.Attribute)
                        where customerAttribute.AttributeId == attributeId
                        select customerAttribute;

            return await Repository.For(query).ToSingleOrDefaultAsync(cancellationToken);
        }

        public async Task<CustomerAttribute> SaveCustomerAttribute(CustomerAttribute encryptedCustomerAttribute, CancellationToken cancellationToken)
        {
            return await Repository.SaveChanges(encryptedCustomerAttribute, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<CustomerAttribute>> GetCustomerAttributes(int customerId, CancellationToken cancellationToken)
        {
            var query = DefaultCustomerAttributeFilteredQuery(customerId);

            return await Repository
                .For(query)
                .ToArrayAsync(cancellationToken);
        }

        public CustomerAttributeService(IRepository<CustomerAttribute> customerAttributeRepository)
            : base(customerAttributeRepository, false)
        {

        }
    }
}
