using Crm.Contracts.Providers;
using Crm.Contracts.Services;
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
    public class GetPreferences : RequestHandlerBase<GetPreferencesRequest, GetPreferencesResponse>
    {
        private readonly ICrmCacheProvider _crmCacheProvider;
        private readonly ICategoryService _categoryService;
        private readonly IPreferenceService _preferenceService;

        public GetPreferences(IMapperProvider mapperProvider, IEncryptionProvider encryptionProvider,
            ICrmCacheProvider crmCacheProvider, ICategoryService categoryService,
            IPreferenceService preferenceService) 
            : base(mapperProvider, encryptionProvider)
        {
            _crmCacheProvider = crmCacheProvider;
            _categoryService = categoryService;
            _preferenceService = preferenceService;
        }

        public override async Task<GetPreferencesResponse> Handle(GetPreferencesRequest request, CancellationToken cancellationToken)
        {
            var preferences = await _crmCacheProvider.GetPreferences(cancellationToken);

            if(string.IsNullOrWhiteSpace(request.CategoryName))
                return Response.Success<GetPreferencesResponse>(preferences);

            var categories = await _crmCacheProvider.GetCategories(cancellationToken);
            
            Category foundCategory;

            if((foundCategory = _categoryService.GetCategory(categories, request.CategoryName)) == null)
                return Response.Failed<GetPreferencesResponse>(new ValidationFailure(nameof(request.CategoryName), 
                    $"Unable to find a category named { request.CategoryName }"));

            return Response.Success<GetPreferencesResponse>(_preferenceService.GetPreferencesByCategory(preferences, foundCategory.Id));
        }
    }
}
