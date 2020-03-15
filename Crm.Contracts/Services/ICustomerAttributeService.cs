using Crm.Domains.Data;
using DNI.Core.Contracts.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Contracts.Services
{
    public interface ICustomerAttributeService : IDataService<CustomerAttribute>
    {
        Task<CustomerAttribute> GetCustomerAttribute(int id, int customerId, CancellationToken cancellationToken);
        Task<CustomerAttribute> SaveCustomerAttribute(CustomerAttribute encryptedCustomerAttribute, CancellationToken cancellationToken);
        Task<IEnumerable<CustomerAttribute>> GetCustomerAttributes(int customerId, CancellationToken cancellationToken);
    }
}
