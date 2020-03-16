using Crm.Contracts.Providers;
using Crm.Contracts.Services;
using Crm.Domains.Data;
using Crm.Domains.Request;
using Crm.Domains.Response;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Providers;
using DNI.Core.Domains;
using System.Threading;
using System.Threading.Tasks;
using CustomerAttributeDto = Crm.Domains.Dto.CustomerAttribute;

namespace Crm.Services.RequestHandlers
{
    public class SaveCustomerAttribute : RequestHandlerBase<SaveCustomerAttributeRequest, SaveCustomerAttributeResponse>
    {
        private readonly IAttributeService _attributeService;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly ICrmCacheProvider _cacheProvider;

        public SaveCustomerAttribute(IMapperProvider mapperProvider, IEncryptionProvider encryptionProvider, 
            IAttributeService attributeService, ICrmCacheProvider cacheProvider,
            ICustomerAttributeService customerAttributeService) 
            : base(mapperProvider, encryptionProvider)
        {
            _attributeService = attributeService;
            _customerAttributeService = customerAttributeService;
            _cacheProvider = cacheProvider;
        }

        public override async Task<SaveCustomerAttributeResponse> Handle(SaveCustomerAttributeRequest request, CancellationToken cancellationToken)
        {
            bool isNewAttribute = false;
            var attribute = _attributeService.GetAttribute(
                await _cacheProvider.GetAttributes(cancellationToken), request.Property);

            CustomerAttribute foundCustomerAttribute;
            
            if(attribute == null)
            {
                attribute = await _attributeService
                    .SaveAttribute(new Attribute { Key = request.Property }, false, cancellationToken);    
                isNewAttribute = true;
            }

            CustomerAttributeDto customerAttribute = new CustomerAttributeDto {
                CustomerId = request.CustomerId,
                Value = request.Value
            };

            foundCustomerAttribute = await _customerAttributeService
                .GetCustomerAttribute(attribute.Id, request.CustomerId, cancellationToken);

            if(foundCustomerAttribute != null)
            {
                customerAttribute.Id = foundCustomerAttribute.Id;
                customerAttribute.Created = foundCustomerAttribute.Created;
            }

            var encryptedCustomerAttribute = await Encryption
                .Encrypt<CustomerAttributeDto, CustomerAttribute>(customerAttribute);

            if(isNewAttribute)
                encryptedCustomerAttribute.Attribute = attribute;
            else
                encryptedCustomerAttribute.AttributeId = attribute.Id;

            var result = await _customerAttributeService
                .SaveCustomerAttribute(encryptedCustomerAttribute, cancellationToken);
            
            customerAttribute = await Encryption.Decrypt<CustomerAttribute, CustomerAttributeDto>(result);

            return Response.Success<SaveCustomerAttributeResponse>(customerAttribute, config => { 
                config.IsNewAttribute = isNewAttribute; 
                config.AttributeId = attribute.Id; });
        }
    }
}
