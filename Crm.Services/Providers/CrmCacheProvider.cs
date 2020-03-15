using Crm.Contracts.Providers;
using Crm.Contracts.Services;
using Crm.Domains.Constants;
using Crm.Domains.Data;
using DNI.Core.Contracts.Enumerations;
using DNI.Core.Contracts.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services.Providers
{
    public class CrmCacheProvider : ICrmCacheProvider
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly IAttributeService _attributeService;

        public CrmCacheProvider(ICacheProvider cacheProvider, IAttributeService attributeService)
        {
            _cacheProvider = cacheProvider;
            _attributeService = attributeService;
        }

        public async Task<IEnumerable<Domains.Data.Attribute>> GetAttributes(CancellationToken cancellationToken)
        {
            return await _cacheProvider.GetOrSet(CacheType.DistributedMemoryCache, 
                CacheConstants.AttributeCache, async(cT) => await _attributeService.GetAttributes(cT));
        }

        public Task<IEnumerable<Category>> GetCategories(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Preference>> GetPreferences(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
