using Crm.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Contracts.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerById(int value, CancellationToken cancellationToken);
        Task<IEnumerable<Customer>> SearchCustomers(Customer encryptedSearchCustomer, CancellationToken cancellationToken);
        Task<Customer> GetCustomerByEmailAddress(IEnumerable<byte> emailAddress, CancellationToken cancellationToken);
        Task<Customer> SaveCustomer(Customer encryptedCustomer, CancellationToken cancellationToken);
        bool PasswordIsValid(Customer foundCustomer, IEnumerable<byte> password);
    }
}
