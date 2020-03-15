using Crm.Contracts.Providers;
using Crm.Contracts.Services;
using Crm.Domains.Constants;
using Crm.Domains.Data;
using DNI.Core.Contracts.Enumerations;
using DNI.Core.Contracts.Providers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services.Providers
{
    public class CrmCacheProvider : ICrmCacheProvider
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly IAttributeService _attributeService;
        private readonly ICategoryService _categoryService;
        private readonly IPreferenceService _preferenceService;

        public CrmCacheProvider(ICacheProvider cacheProvider, 
            IAttributeService attributeService,
            ICategoryService categoryService,
            IPreferenceService preferenceService)
        {
            _cacheProvider = cacheProvider;
            _attributeService = attributeService;
            _categoryService = categoryService;
            _preferenceService = preferenceService;
        }

        public async Task<IEnumerable<Attribute>> GetAttributes(CancellationToken cancellationToken)
        {
            return await _cacheProvider.GetOrSet(CacheType.DistributedMemoryCache, 
                CacheConstants.AttributeCache, async(cT) => await _attributeService
                    .GetAttributes(cT), cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Category>> GetCategories(CancellationToken cancellationToken)
        {
            return await _cacheProvider.GetOrSet(CacheType.DistributedMemoryCache, 
                CacheConstants.CategoryCache, async(cT) => await _categoryService
                    .GetCategories(cT), cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Preference>> GetPreferences(CancellationToken cancellationToken)
        {
            return await _cacheProvider.GetOrSet(CacheType.DistributedMemoryCache,
                CacheConstants.PreferenceCache, async(cT) => await _preferenceService
                    .GetPreferences(cT), cancellationToken: cancellationToken);
        }
    }
}
