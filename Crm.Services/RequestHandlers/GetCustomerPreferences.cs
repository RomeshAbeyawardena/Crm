using Crm.Contracts.Services;
using Crm.Domains.Contracts;
using Crm.Domains.Data;
using Crm.Domains.Request;
using Crm.Domains.Response;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Providers;
using DNI.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CustomerDto = Crm.Domains.Dto.Customer;
using FluentValidation;
using FluentValidation.Results;

namespace Crm.Services.RequestHandlers
{
    public class GetCustomerPreferences : RequestHandlerBase<GetCustomerPreferencesRequest, GetCustomerPreferenceResponse>
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerPreferenceService _customerPreferenceService;

        public GetCustomerPreferences(IMapperProvider mapperProvider, IEncryptionProvider encryptionProvider,
            ICustomerService customerService,
            ICustomerPreferenceService customerPreferenceService) 
            : base(mapperProvider, encryptionProvider)
        {
            _customerService = customerService;
            _customerPreferenceService = customerPreferenceService;
        }

        public override async Task<GetCustomerPreferenceResponse> Handle(GetCustomerPreferencesRequest request, CancellationToken cancellationToken)
        {
            var encryptedCustomer = await Encryption.Encrypt<CustomerDto, Customer>(new CustomerDto { EmailAddress = request.EmailAddress });

            var customer = await _customerService.GetCustomerByEmailAddress(encryptedCustomer.EmailAddress, cancellationToken);

            if(customer == null)
                Response.Failed<GetCustomerPreferenceResponse>(new ValidationFailure(nameof(request.EmailAddress), 
                    $"Customer preferences for '{ request.EmailAddress }' not found"));

            var customerPreferences = await _customerPreferenceService.GetCustomerPreferences(customer.Id, request.ToDate, cancellationToken);

            return Response.Success<GetCustomerPreferenceResponse>(customerPreferences);
        }
    }
}
