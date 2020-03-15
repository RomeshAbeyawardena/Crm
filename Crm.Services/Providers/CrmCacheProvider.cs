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
                CacheConstants.AttributeCache, async(cT) => await _attributeService.GetAttributes(cT));
        }

        public async Task<IEnumerable<Category>> GetCategories(CancellationToken cancellationToken)
        {
            return await _categoryService.GetCategories(cancellationToken);
        }

        public async Task<IEnumerable<Preference>> GetPreferences(CancellationToken cancellationToken)
        {
            return await _preferenceService.GetPreferences(cancellationToken);
        }
    }
}
