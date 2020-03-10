using Crm.Contracts.Services;
using Crm.Domains.Data;
using Crm.Domains.Request;
using Crm.Domains.Response;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Providers;
using DNI.Core.Domains;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CustomerDto = Crm.Domains.Dto.Customer;
using FluentValidation.Results;

namespace Crm.Services.RequestHandlers
{
    public class SaveCustomer : RequestHandlerBase<SaveCustomerRequest, SaveCustomerResponse>
    {
        private readonly ICustomerService _customerService;

        public SaveCustomer(ICustomerService customerService, IMapperProvider mapperProvider, IEncryptionProvider encryptionProvider) 
            : base(mapperProvider, encryptionProvider)
        {
            _customerService = customerService;
        }

        public override async Task<SaveCustomerResponse> Handle(SaveCustomerRequest request, CancellationToken cancellationToken)
        {
            var newCustomer = MapperProvider.Map<SaveCustomerRequest, CustomerDto>(request);

            var encryptedCustomer = await EncryptionProvider.Encrypt<CustomerDto, Customer>(newCustomer);

            var foundCustomer = await _customerService.GetCustomerByEmailAddress(encryptedCustomer.EmailAddress, cancellationToken);

            if(foundCustomer != null && encryptedCustomer.Id != foundCustomer.Id)
                Response.Failed<SaveCustomerResponse>(new ValidationFailure(nameof(request.EmailAddress), "Email address already taken"));

            encryptedCustomer = await _customerService.SaveCustomer(encryptedCustomer, cancellationToken);

            newCustomer = await EncryptionProvider.Decrypt<Customer, CustomerDto>(encryptedCustomer);

            return Response.Success<SaveCustomerResponse>(newCustomer);
        }
    }
}
