﻿using Crm.Contracts.Services;
using Crm.Domains;
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

namespace Crm.Services.RequestHandler
{
    public class SaveCustomerHashes : RequestHandlerBase<SaveCustomerHashesRequest, SaveCustomerHashesResponse>
    {
        private readonly ICharacterHashService _characterHashService;
        private readonly ICustomerHashService _customerHashService;
        private readonly IHashes _hashes;

        public SaveCustomerHashes(IMapperProvider mapperProvider, 
            IEncryptionProvider encryptionProvider, 
            ICharacterHashService characterHashService,
            ICustomerHashService customerHashService,
            ApplicationSettings applicationSettings) : base(mapperProvider, encryptionProvider)
        {
            _characterHashService = characterHashService;
            _customerHashService = customerHashService;
            _hashes = applicationSettings.GetHashes();
        }

        public override async Task<SaveCustomerHashesResponse> Handle(SaveCustomerHashesRequest request, CancellationToken cancellationToken)
        {
            var savedHashes = new List<CustomerHash>();
            var customerHashes = await _customerHashService.GetCustomerHashes(request.CustomerId, cancellationToken);
            foreach (var characterHash in _characterHashService.GetHashes(_hashes, request.Characters))
            {
                var customerHash = _customerHashService.GetCustomerHash(customerHashes, characterHash.HashValue);

                if(customerHash != null)
                    continue;

                customerHash = await _customerHashService.SaveCustomerHash(new Domains.Data.CustomerHash { 
                        CustomerId = request.CustomerId,
                        Hash = characterHash.HashValue
                    }, false, cancellationToken);
                savedHashes.Add(customerHash);
            }

            if(savedHashes.Any())
                await _customerHashService.CommitChanges(cancellationToken);

            return Response.Success<SaveCustomerHashesResponse>(savedHashes.ToArray());
        }
    }
}
