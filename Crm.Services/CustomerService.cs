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
        private IQueryable<Customer> DefaultCustomerSearchQuery(Customer encryptedSearchCustomer) => from customer in DefaultCustomerQuery
                                                                                                     where customer.FirstName == encryptedSearchCustomer.FirstName
                                                                                                     || customer.MiddleName == encryptedSearchCustomer.MiddleName
                                                                                                     || customer.LastName == encryptedSearchCustomer.LastName
                                                                                                     || customer.EmailAddress == encryptedSearchCustomer.EmailAddress
                                                                                                     select customer;


        public async Task<Customer> GetCustomerById(int value, CancellationToken cancellationToken)
        {
            return await _customerRepository.Find(false, cancellationToken, value);
        }

        public IPagerResult<Customer> SearchCustomers(Customer encryptedSearchCustomer)
        {
            var query = DefaultCustomerSearchQuery(encryptedSearchCustomer);

            return _customerRepository
                .For(query)
                .AsPager();
        }

        public async Task<Customer> GetCustomerByEmailAddress(IEnumerable<byte> emailAddress, CancellationToken cancellationToken)
        {
            var emailAddressArray = emailAddress.ToArray();

            var query = from customer in DefaultCustomerQuery
                        where customer.EmailAddress == emailAddressArray
                        select customer;

            return await _customerRepository
                .For(query)
                .ToSingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Customer> SaveCustomer(Customer encryptedCustomer, bool saveChange, CancellationToken cancellationToken)
        {
            if (encryptedCustomer == null)
                throw new ArgumentNullException(nameof(encryptedCustomer));

            return await _customerRepository.SaveChanges(encryptedCustomer, cancellationToken: cancellationToken);
        }

        public bool PasswordIsValid(Customer foundCustomer, IEnumerable<byte> password)
        {
            if (foundCustomer == null)
                throw new ArgumentNullException(nameof(foundCustomer));

            var actualPasswordHash = Convert.ToBase64String(foundCustomer.Password);

            var expectedPasswordHash = Convert.ToBase64String(password.ToArray());

            return expectedPasswordHash.Equals(actualPasswordHash);
        }

        public Task<IEnumerable<Customer>> SearchCustomers(Customer encryptedSearchCustomer, CancellationToken cancellationToken)
        {
            var query = DefaultCustomerSearchQuery(encryptedSearchCustomer);

            return _customerRepository
                .For(query)
                .ToArrayAsync(cancellationToken);
        }

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }
    }
}
