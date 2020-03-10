using Crm.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Contracts.Services
{
    public interface ICustomerAttributeService
    {
        Task<CustomerAttribute> GetCustomerAttribute(int id, int customerId, CancellationToken cancellationToken);
        Task<CustomerAttribute> SaveCustomerAttribute(CustomerAttribute encryptedCustomerAttribute, CancellationToken cancellationToken);
        Task<IEnumerable<CustomerAttribute>> GetCustomerAttributes(int customerId, CancellationToken cancellationToken);
    }
}
