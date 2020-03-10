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

namespace Crm.Services.RequestHandlers
{
    public class GetCustomer : RequestHandlerBase<GetCustomerRequest, GetCustomerResponse>
    {
        private readonly ICustomerService _customerService;

        public GetCustomer(ICustomerService customerService, IMapperProvider mapperProvider, IEncryptionProvider encryptionProvider)
            : base(mapperProvider, encryptionProvider)
        {
            _customerService = customerService;
        }

        public override async Task<GetCustomerResponse> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            Customer result;
            CustomerDto decryptedResult;
            if (request.Id.HasValue)
            {
                result = await _customerService.GetCustomerById(request.Id.Value, cancellationToken);

                if (result == null)
                    return Response.Failed<GetCustomerResponse>(
                        new ValidationFailure(nameof(request.Id), "Unable to find customer with specified Id"));

                decryptedResult = await Encryption.Decrypt<Customer, CustomerDto>(result);

                return Response.Success<GetCustomerResponse>(decryptedResult);
            }

            if (string.IsNullOrWhiteSpace(request.EmailAddress)
                || string.IsNullOrWhiteSpace(request.FirstName)
                || string.IsNullOrWhiteSpace(request.MiddleName)
                || string.IsNullOrWhiteSpace(request.LastName))
                return Response.Failed<GetCustomerResponse>(new ValidationFailure(string.Empty, "Must specify a search parameter"));

            var mappedCustomer = Mapper.Map<GetCustomerRequest, CustomerDto>(request);

            var encryptedSearchCustomer = await Encryption.Encrypt<CustomerDto, Customer>(mappedCustomer);

            var results = await _customerService
                .SearchCustomers(encryptedSearchCustomer, cancellationToken);

            result = results.FirstOrDefault();
            if (result == null)
                return Response.Failed<GetCustomerResponse>(
                    new ValidationFailure(string.Empty, "Unable to find customer with specified search parameters"));

            decryptedResult = await Encryption.Decrypt<Customer, CustomerDto>(result);

            return Response.Success<GetCustomerResponse>(result);
        }
    }
}
