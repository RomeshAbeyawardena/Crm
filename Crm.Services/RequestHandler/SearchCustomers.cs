using Crm.Contracts.Services;
using Crm.Domains.Data;
using Crm.Domains.Request;
using Crm.Domains.Response;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Providers;
using DNI.Core.Domains;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CustomerDto = Crm.Domains.Dto.Customer;

namespace Crm.Services.RequestHandler
{
    public class SearchCustomers : IRequestHandler<SearchCustomersRequest, SearchCustomersResponse>
    {
        private readonly ICustomerService _customerService;
        private readonly IMapperProvider _mapperProvider;
        private readonly IEncryptionProvider _encryptionProvider;

        public SearchCustomers(ICustomerService customerService, IMapperProvider mapperProvider, IEncryptionProvider encryptionProvider)
        {
            _customerService = customerService;
            _mapperProvider = mapperProvider;
            _encryptionProvider = encryptionProvider;
        }

        public async Task<SearchCustomersResponse> Handle(SearchCustomersRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.EmailAddress)
                || string.IsNullOrWhiteSpace(request.FirstName)
                || string.IsNullOrWhiteSpace(request.MiddleName)
                || string.IsNullOrWhiteSpace(request.LastName))
                return Response.Failed<SearchCustomersResponse>(new ValidationFailure(string.Empty, "Must specify a search parameter"));

            var mappedCustomer = _mapperProvider.Map<SearchCustomersRequest, CustomerDto>(request);

            var encryptedSearchCustomer = await _encryptionProvider.Encrypt<CustomerDto, Customer>(mappedCustomer);

            var results = await _customerService
                .SearchCustomers(encryptedSearchCustomer, cancellationToken);

            if (results == null || !results.Any())
                return Response.Failed<SearchCustomersResponse>(
                    new ValidationFailure(string.Empty, "Unable to find customer with specified search parameters"));

            var decryptedResult = await _encryptionProvider.Decrypt<Customer, CustomerDto>(results);

            return Response.Success<SearchCustomersResponse>(results);
        }
    }
}
