using Crm.Contracts.Services;
using Crm.Domains;
using Crm.Domains.Contracts;
using Crm.Domains.Data;
using Crm.Domains.Request;
using Crm.Domains.Response;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Providers;
using DNI.Core.Domains;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services.RequestHandlers
{
    public class SaveCustomerHashes : RequestHandlerBase<SaveCustomerHashesRequest, SaveCustomerHashesResponse>
    {
        private readonly ICharacterHashService _characterHashService;
        private readonly ICustomerHashService _customerHashService;
        private readonly IHashes _hashes;
        private readonly ICustomerService _customerService;

        public SaveCustomerHashes(IMapperProvider mapperProvider,
            IEncryptionProvider encryptionProvider,
            ICharacterHashService characterHashService,
            ICustomerHashService customerHashService,
            ICustomerService customerService,
            ApplicationSettings applicationSettings) : base(mapperProvider, encryptionProvider)
        {
            _characterHashService = characterHashService;
            _customerHashService = customerHashService;
            _hashes = applicationSettings.GetHashes();
            _customerService = customerService;
        }

        public override async Task<SaveCustomerHashesResponse> Handle(SaveCustomerHashesRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerService.GetCustomerById(request.CustomerId, cancellationToken);

            if(customer.LastIndexed.HasValue && customer.LastIndexed.Value > customer.Modified)
                return Response.Failed<SaveCustomerHashesResponse>(new ValidationFailure(nameof(request.CustomerId), "Indexing has already taken place"));

            async Task<IEnumerable<CustomerHash>> GetCustomerHashes() => await _customerHashService.GetCustomerHashes(request.CustomerId, cancellationToken);

            var customerHashes = await GetCustomerHashes();

            var savedHashes = new List<CustomerHash>();
            
            var characterHashes = _characterHashService.GetHashes(_hashes, request.Characters);

            foreach (var characterHash in characterHashes)
            {
                var customerHash = _customerHashService.GetCustomerHash(customerHashes, characterHash.Value);

                if (customerHash != null)
                    continue;

                customerHash = await _customerHashService.SaveCustomerHash(new CustomerHash
                {
                    CustomerId = request.CustomerId,
                    Hash = characterHash.Value.ToArray()
                }, false, cancellationToken);
                savedHashes.Add(customerHash);

                customerHashes = customerHashes.Append(customerHash);
            }

            customer.LastIndexed = DateTimeOffset.UtcNow;
            await _customerService.SaveCustomer(customer, false, cancellationToken);

            if (savedHashes.Any())
                await _customerHashService.CommitChanges(cancellationToken);

            return Response.Success<SaveCustomerHashesResponse>(savedHashes.ToArray());
        }
    }
}
