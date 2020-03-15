using Crm.Domains.Data;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Contracts.Services
{
    public interface ICustomerService : IDataService<Domains.Data.Customer>
    {
        Task<Customer> GetCustomerById(int value, CancellationToken cancellationToken);
        IPagerResult<Customer> SearchCustomers(Customer encryptedSearchCustomer);
        Task<IEnumerable<Customer>> SearchCustomers(Customer encryptedSearchCustomer, CancellationToken cancellationToken);
        Task<Customer> GetCustomerByEmailAddress(IEnumerable<byte> emailAddress, CancellationToken cancellationToken);
        Task<Customer> SaveCustomer(Customer encryptedCustomer, bool saveChanges, bool detachAfterSave, CancellationToken cancellationToken);
        bool PasswordIsValid(Customer foundCustomer, IEnumerable<byte> password);
    }
}
