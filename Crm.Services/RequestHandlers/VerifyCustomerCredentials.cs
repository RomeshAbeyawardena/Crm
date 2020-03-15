using Crm.Contracts.Services;
using Crm.Domains.Data;
using Crm.Domains.Request;
using Crm.Domains.Response;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Providers;
using DNI.Core.Domains;
using FluentValidation.Results;
using System.Threading;
using System.Threading.Tasks;
using CustomerDto = Crm.Domains.Dto.Customer;

namespace Crm.Services.RequestHandlers
{
    public class VerifyCustomerCredentials : RequestHandlerBase<VerifyCustomerCredentialsRequest, VerifyCustomerCredentialsResponse>
    {
        private readonly ICustomerService _customerService;

        public VerifyCustomerCredentials(IMapperProvider mapperProvider, 
            IEncryptionProvider encryptionProvider, ICustomerService customerService) 
            : base(mapperProvider, encryptionProvider)
        {
            _customerService = customerService;
        }

        public override async Task<VerifyCustomerCredentialsResponse> Handle(VerifyCustomerCredentialsRequest request, CancellationToken cancellationToken)
        {
            var customer = Mapper.Map<VerifyCustomerCredentialsRequest, CustomerDto>(request);

            var encryptedCustomer = await Encryption.Encrypt<CustomerDto, Customer>(customer);
            
            var foundCustomer = await _customerService
                .GetCustomerByEmailAddress(encryptedCustomer.EmailAddress, cancellationToken);

            if(foundCustomer == null)
                return Response
                    .Failed<VerifyCustomerCredentialsResponse>(new ValidationFailure(nameof(request.EmailAddress), "Invalid e-mail address"));

            customer = await Encryption.Decrypt<Customer, CustomerDto>(foundCustomer);

            if(_customerService.PasswordIsValid(foundCustomer, encryptedCustomer.Password))
                return Response.Success<VerifyCustomerCredentialsResponse>(customer);

            return Response
                    .Failed<VerifyCustomerCredentialsResponse>(new ValidationFailure(string.Empty, "Invalid e-mail address or password"));
        }
    }
}
