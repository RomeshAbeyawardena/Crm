using Crm.Contracts.Services;
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
using CustomerAttributeDto = Crm.Domains.Dto.CustomerAttribute;
namespace Crm.Services.RequestHandlers
{
    public class GetCustomerAttributes : RequestHandlerBase<GetCustomerAttributeRequest, GetCustomerAttributesResponse>
    {
        private readonly ICustomerAttributeService _customerAttributeService;

        public GetCustomerAttributes(IMapperProvider mapperProvider, IEncryptionProvider encryptionProvider, 
            ICustomerAttributeService customerAttributeService) 
            : base(mapperProvider, encryptionProvider)
        {
            _customerAttributeService = customerAttributeService;
        }

        public override async Task<GetCustomerAttributesResponse> Handle(GetCustomerAttributeRequest request, CancellationToken cancellationToken)
        {
            var customerAttributes = await _customerAttributeService.GetCustomerAttributes(request.CustomerId, cancellationToken);

            var decryptedCustomAttributes = await EncryptionProvider.Decrypt<CustomerAttribute, CustomerAttributeDto>(customerAttributes);

            return Response.Success<GetCustomerAttributesResponse>(decryptedCustomAttributes);
        }
    }
}
