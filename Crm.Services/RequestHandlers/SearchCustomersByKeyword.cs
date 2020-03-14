using Crm.Contracts.Services;
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
using CustomerDto = Crm.Domains.Dto.Customer;

namespace Crm.Services.RequestHandlers
{
    public class SearchCustomersByKeyword : RequestHandlerBase<SearchCustomersByKeywordRequest, SearchCustomersByKeywordResponse>
    {
        private readonly ICustomerHashService _customerHashService;
        private readonly ICharacterHashService _characterHashService;
        private readonly IHashes _hashes;

        public SearchCustomersByKeyword(IMapperProvider mapperProvider, 
            IEncryptionProvider encryptionProvider, 
            ICharacterHashService characterHashService,
            ICustomerHashService customerHashService,
            ApplicationSettings applicationSettings) 
            : base(mapperProvider, encryptionProvider)
        {
            _customerHashService = customerHashService;
            _characterHashService = characterHashService;
            _hashes = applicationSettings.GetHashes();
        }

        public override async Task<SearchCustomersByKeywordResponse> Handle(SearchCustomersByKeywordRequest request, CancellationToken cancellationToken)
        {
            var characters = _characterHashService.GetCharacters(request.Keyword).Distinct();

            var characterHashes = _characterHashService.GetHashes(_hashes, characters);

            var customers = await _customerHashService
                .GetCustomersByHash(characterHashes
                    .Select(hash => hash.Value), cancellationToken, 
                    configure => {  configure.PageNumber = request.PageNumber; 
                                    configure.MaximumRowsPerPage = request.MaximumRowsPerPage;
                                    configure.UseAsync = true;
                    });

            var decryptedCustomers = await Encryption.Decrypt<Customer, CustomerDto>(customers);

            return Response.Success<SearchCustomersByKeywordResponse>(decryptedCustomers);
        }
    }
}
