using Crm.Contracts.Services;
using Crm.Domains;
using Crm.Domains.Contracts;
using Crm.Domains.Data;
using Crm.Domains.Request;
using Crm.Domains.Response;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Providers;
using DNI.Core.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services.RequestHandlers
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
            async Task<IEnumerable<CustomerHash>> GetCustomerHashes() => 
                await _customerHashService.GetCustomerHashes(request.CustomerId, cancellationToken);

            var customerHashes = await GetCustomerHashes();

            var savedHashes = new List<CustomerHash>();
            
            var characterHashes = _characterHashService.GetHashes(_hashes, request.Characters);

            foreach (var characterHash in characterHashes)
            {
                var customerHash = _customerHashService.GetCustomerHash(customerHashes, characterHash.Hash.Value, characterHash.Index);

                if (customerHash != null)
                    continue;

                customerHash = await _customerHashService.SaveCustomerHash(new CustomerHash
                {
                    CustomerId = request.CustomerId,
                    Index = characterHash.Index,
                    Hash = characterHash.Hash.Value.ToArray()
                }, false, cancellationToken);
                savedHashes.Add(customerHash);

                customerHashes = customerHashes.Append(customerHash);
            }

            if (savedHashes.Any())
                await _customerHashService.CommitChanges(cancellationToken);

            return Response.Success<SaveCustomerHashesResponse>(savedHashes.ToArray());
        }
    }
}
