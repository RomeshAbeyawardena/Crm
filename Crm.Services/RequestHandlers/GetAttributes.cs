using Crm.Contracts.Services;
using Crm.Domains.Request;
using Crm.Domains.Response;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Enumerations;
using DNI.Core.Contracts.Providers;
using DNI.Core.Domains;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services.RequestHandlers
{
    public class GetAttributes : RequestHandlerBase<GetAttributesRequest, GetAttributesResponse>
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly IAttributeService _attributeService;

        public GetAttributes(IMapperProvider mapperProvider, IEncryptionProvider encryptionProvider,
            ICacheProvider cacheProvider, IAttributeService attributeService) 
            : base(mapperProvider, encryptionProvider)
        {
            _cacheProvider = cacheProvider;
            _attributeService = attributeService;
        }

        public override async Task<GetAttributesResponse> Handle(GetAttributesRequest request, CancellationToken cancellationToken)
        {
            var attributes = await _cacheProvider.GetOrSet(CacheType.DistributedMemoryCache, "", 
                async(cancellationToken) => await _attributeService.GetAttributes(cancellationToken));

            return Response.Success<GetAttributesResponse>(attributes);
        }
    }
}
