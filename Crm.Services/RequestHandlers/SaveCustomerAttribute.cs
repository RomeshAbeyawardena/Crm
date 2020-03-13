﻿using Crm.Contracts.Services;
using Crm.Domains.Constants;
using Crm.Domains.Data;
using Crm.Domains.Request;
using Crm.Domains.Response;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Enumerations;
using DNI.Core.Contracts.Providers;
using DNI.Core.Domains;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CustomerAttributeDto = Crm.Domains.Dto.CustomerAttribute;

namespace Crm.Services.RequestHandlers
{
    public class SaveCustomerAttribute : RequestHandlerBase<SaveCustomerAttributeRequest, SaveCustomerAttributeResponse>
    {
        private readonly IAttributeService _attributeService;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly ICacheEntryTracker _cacheEntryTracker;

        public SaveCustomerAttribute(IMapperProvider mapperProvider, IEncryptionProvider encryptionProvider, 
            IAttributeService attributeService,
            ICustomerAttributeService customerAttributeService,
            ICacheEntryTracker cacheEntryTracker) 
            : base(mapperProvider, encryptionProvider)
        {
            _attributeService = attributeService;
            _customerAttributeService = customerAttributeService;
            _cacheEntryTracker = cacheEntryTracker;
        }

        public override async Task<SaveCustomerAttributeResponse> Handle(SaveCustomerAttributeRequest request, CancellationToken cancellationToken)
        {
            var attribute = await _attributeService.GetAttribute(request.Property, cancellationToken);
            CustomerAttribute foundCustomerAttribute;
            
            if(attribute == null)
                attribute = await _attributeService
                    .SaveAttribute(new Domains.Data.Attribute { Key = request.Property }, false, cancellationToken);            
            
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

            encryptedCustomerAttribute.Attribute = attribute;

            var result = await _customerAttributeService.SaveCustomerAttribute(encryptedCustomerAttribute, cancellationToken);
            
            customerAttribute = await Encryption.Decrypt<CustomerAttribute, CustomerAttributeDto>(result);

            await _cacheEntryTracker.SetState(CacheConstants.AttributeCache, CacheEntryState.Invalid, cancellationToken);

            return Response.Success<SaveCustomerAttributeResponse>(customerAttribute);
        }
    }
}
