using Crm.Domains.Request;
using DNI.Core.Contracts;
using DNI.Core.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Web.Controllers.Api
{
    public class DocumentationController : DefaultControllerBase
    {
        public DocumentationController(IMediatorService mediatorService, IMapperProvider mapperProvider) 
            : base(mediatorService, mapperProvider)
        {
        }

        [HttpGet]
        [Route("/docs/{groupName?}")]
        public async Task<ViewResult> Index([FromRoute]string groupName, CancellationToken cancellationToken)
        {
            EnsureModalStateIsValid();

            var request = new GetDocumentationRequest { GroupName = groupName };
            var response = await Mediator.Send(request, cancellationToken);

            return View(response.Result);
        }
    }
}
