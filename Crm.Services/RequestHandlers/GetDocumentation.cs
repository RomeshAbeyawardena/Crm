using Crm.Domains.Request;
using Crm.Domains.Response;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Providers;
using DNI.Core.Domains;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services.RequestHandlers
{
    public class GetDocumentation : RequestHandlerBase<GetDocumentationRequest, GetDocumentationResponse>
    {
        private readonly IApiDescriptionGroupCollectionProvider _apiExplorer;

        public GetDocumentation(IMapperProvider mapperProvider, 
            IEncryptionProvider encryptionProvider,
            IApiDescriptionGroupCollectionProvider apiExplorer) 
            : base(mapperProvider, encryptionProvider)
        {
            _apiExplorer = apiExplorer;
        }

        public override async Task<GetDocumentationResponse> Handle(GetDocumentationRequest request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if(string.IsNullOrWhiteSpace(request.GroupName))
                return Response.Success<GetDocumentationResponse>(_apiExplorer.ApiDescriptionGroups);

            var foundGroup = _apiExplorer.ApiDescriptionGroups.Items
                .Where(descriptionGroup => descriptionGroup.GroupName.Equals(request.GroupName, StringComparison.InvariantCultureIgnoreCase));

            if(foundGroup == null)
                return Response.Failed<GetDocumentationResponse>(new ValidationFailure(nameof(request.GroupName), 
                    "Group name not found"));

            return Response.Success<GetDocumentationResponse>(CreateCollection(foundGroup));
        }

        private ApiDescriptionGroupCollection CreateCollection(IEnumerable<ApiDescriptionGroup> groups)
        {
            return new ApiDescriptionGroupCollection(groups.ToArray(), _apiExplorer.ApiDescriptionGroups.Version);
        }
    }
}
