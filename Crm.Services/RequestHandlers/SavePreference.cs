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
using FluentValidation.Results;

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

        public override async Task<SavePreferenceResponse> Handle(SavePreferenceRequest request, 
            CancellationToken cancellationToken)
        {
            var isNewCategory = false;

            var preferences = await _cacheProvider.GetPreferences(cancellationToken);
            var preference = _preferenceService.GetPreference(preferences, request.Name);

            if(preference != null)
                return Response.Failed<SavePreferenceResponse>(new ValidationFailure(nameof(request.Name),
                    "Preference already exists"));

            var categories = await _cacheProvider.GetCategories(cancellationToken);

            var category = string.IsNullOrWhiteSpace(request.CategoryName) 
                ? _categoryService.GetCategory(categories, request.CategoryId)
                : _categoryService.GetCategory(categories, request.CategoryName);

            preference = new Preference
            {
                Name = request.Name,
            };

            if (category == null)
            { 
                if(string.IsNullOrWhiteSpace(request.CategoryName) )
                    return Response.Failed<SavePreferenceResponse>(
                        new ValidationFailure(nameof(request.CategoryId), 
                        "Unable to find category"));

                category = await _categoryService.Save(new Category { Name = request.CategoryName }, 
                    false, false, cancellationToken);

                preference.Category = category;

                isNewCategory = true;
            }
            else
                preference.CategoryId = category.Id;

            preference = await _preferenceService.Save(preference, true, true, cancellationToken);

            return Response.Success<SavePreferenceResponse>(preference, configure => { 
                configure.IsNewCategory = isNewCategory;
                configure.CategoryId = category.Id; });
        }
    }
}
