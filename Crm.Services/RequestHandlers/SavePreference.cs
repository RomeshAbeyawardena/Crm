using Crm.Contracts.Providers;
using Crm.Contracts.Services;
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

namespace Crm.Services.RequestHandlers
{
    public class SavePreference : RequestHandlerBase<SavePreferenceRequest, SavePreferenceResponse>
    {
        private readonly ICrmCacheProvider _cacheProvider;
        private readonly IPreferenceService _preferenceService;
        private readonly ICategoryService _categoryService;

        public SavePreference(IMapperProvider mapperProvider, IEncryptionProvider encryptionProvider,
            ICrmCacheProvider cacheProvider, IPreferenceService preferenceService,
            ICategoryService categoryService) 
            : base(mapperProvider, encryptionProvider)
        {
            _cacheProvider = cacheProvider;
            _preferenceService = preferenceService;
            _categoryService = categoryService;
        }

        public override async Task<SavePreferenceResponse> Handle(SavePreferenceRequest request, CancellationToken cancellationToken)
        {
            var preferences = await _cacheProvider.GetPreferences(cancellationToken);
            var preference = _preferenceService.GetPreference(preferences, request.Name);

            if(preference != null)
                return Response.Failed<SavePreferenceResponse>(new FluentValidation.Results.ValidationFailure(nameof(request.Name),
                    "Preference already exists"));

            var categories = await _cacheProvider.GetCategories(cancellationToken);

            var category = string.IsNullOrWhiteSpace(request.CategoryName) 
                ? _categoryService.GetCategory(categories, request.CategoryName)
                : _categoryService.GetCategory(categories, request.CategoryId);

            if(category == null)
            { 
                if(string.IsNullOrWhiteSpace(request.CategoryName) )
                    return Response.Failed<SavePreferenceResponse>(new FluentValidation.Results.ValidationFailure(nameof(request.CategoryId), "Unable to find category"));

                category = await _categoryService.Save(new Category { Name = request.CategoryName }, 
                    false, false, cancellationToken);
            }

            preference = await _preferenceService.Save(new Preference
            {
                Name = request.Name,
                Category = category
            }, true, true, cancellationToken);

            return Response.Success<SavePreferenceResponse>(preference);
        }
    }
}
