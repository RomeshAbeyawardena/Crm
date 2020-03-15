using Crm.Contracts.Providers;
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
using DNI.Core.Shared.Extensions;

namespace Crm.Services.RequestHandlers
{
    public class GetCustomerAttributes : RequestHandlerBase<GetCustomerAttributeRequest, GetCustomerAttributesResponse>
    {
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly ICrmCacheProvider _cacheProvider;
        private readonly IAttributeService _attributeService;

        public GetCustomerAttributes(IMapperProvider mapperProvider, IEncryptionProvider encryptionProvider, 
            ICustomerAttributeService customerAttributeService, ICrmCacheProvider cacheProvider,
            IAttributeService attributeService) 
            : base(mapperProvider, encryptionProvider)
        {
            _customerAttributeService = customerAttributeService;
            _cacheProvider = cacheProvider;
            _attributeService = attributeService;
        }

        public override async Task<GetCustomerAttributesResponse> Handle(GetCustomerAttributeRequest request, CancellationToken cancellationToken)
        {
            var attributes = await _cacheProvider.GetAttributes(cancellationToken);

            var customerAttributes = await _customerAttributeService.GetCustomerAttributes(request.CustomerId, cancellationToken);

            customerAttributes.ForEach(customerAttribute => customerAttribute.Attribute = _attributeService
                .GetAttribute(attributes, customerAttribute.Id));

            var decryptedCustomAttributes = await Encryption.Decrypt<CustomerAttribute, CustomerAttributeDto>(customerAttributes);
            
            return Response.Success<GetCustomerAttributesResponse>(decryptedCustomAttributes);
        }
    }
}
