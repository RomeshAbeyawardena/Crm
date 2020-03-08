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
    public class GetCustomer : IRequestHandler<GetCustomerRequest, GetCustomerResponse>
    {
        private readonly ICustomerService _customerService;
        private readonly IMapperProvider _mapperProvider;
        private readonly IEncryptionProvider _encryptionProvider;

        public GetCustomer(ICustomerService customerService, IMapperProvider mapperProvider, IEncryptionProvider encryptionProvider)
        {
            _customerService = customerService;
            _mapperProvider = mapperProvider;
            _encryptionProvider = encryptionProvider;
        }

        public async Task<GetCustomerResponse> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            Customer result;
            CustomerDto decryptedResult;
            if (request.Id.HasValue)
            {
                result = await _customerService.GetCustomerById(request.Id.Value, cancellationToken);

                if (result == null)
                    return Response.Failed<GetCustomerResponse>(
                        new ValidationFailure(nameof(request.Id), "Unable to find customer with specified Id"));

                decryptedResult = await _encryptionProvider.Decrypt<Customer, CustomerDto>(result);

                return Response.Success<GetCustomerResponse>(decryptedResult);
            }

            if (string.IsNullOrWhiteSpace(request.EmailAddress)
                || string.IsNullOrWhiteSpace(request.FirstName)
                || string.IsNullOrWhiteSpace(request.MiddleName)
                || string.IsNullOrWhiteSpace(request.LastName))
                return Response.Failed<GetCustomerResponse>(new ValidationFailure(string.Empty, "Must specify a search parameter"));

            var mappedCustomer = _mapperProvider.Map<GetCustomerRequest, CustomerDto>(request);

            var encryptedSearchCustomer = await _encryptionProvider.Encrypt<CustomerDto, Customer>(mappedCustomer);

            var results = await _customerService
                .SearchCustomers(encryptedSearchCustomer, cancellationToken);

            result = results.FirstOrDefault();
            if (result == null)
                return Response.Failed<GetCustomerResponse>(
                    new ValidationFailure(string.Empty, "Unable to find customer with specified search parameters"));

            decryptedResult = await _encryptionProvider.Decrypt<Customer, CustomerDto>(result);

            return Response.Success<GetCustomerResponse>(result);
        }
    }
}
