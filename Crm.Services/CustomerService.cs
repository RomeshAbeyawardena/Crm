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
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private IQueryable<Customer> DefaultCustomerQuery => _customerRepository.Query(customer => customer.Active);
        public async Task<Customer> GetCustomerById(int value, CancellationToken cancellationToken)
        {
            return await _customerRepository.Find(false, cancellationToken, value);
        }

        public async Task<IEnumerable<Customer>> SearchCustomers(Customer encryptedSearchCustomer, CancellationToken cancellationToken)
        {
            var query = from customer in DefaultCustomerQuery
                        where customer.FirstName == encryptedSearchCustomer.FirstName
                        || customer.MiddleName == encryptedSearchCustomer.MiddleName
                        || customer.LastName == encryptedSearchCustomer.LastName
                        || customer.EmailAddress == encryptedSearchCustomer.EmailAddress
                        select customer;

            return await _customerRepository
                .For(query)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<Customer> GetCustomerByEmailAddress(IEnumerable<byte> emailAddress, CancellationToken cancellationToken)
        {
            var emailAddressArray = emailAddress.ToArray();

            var query = from customer in DefaultCustomerQuery
                        where customer.EmailAddress == emailAddress
                        select customer;

            return await _customerRepository
                .For(query)
                .ToSingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Customer> SaveCustomer(Customer encryptedCustomer, CancellationToken cancellationToken)
        {
            return await _customerRepository.SaveChanges(encryptedCustomer, cancellationToken: cancellationToken);
        }

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }
    }
}
