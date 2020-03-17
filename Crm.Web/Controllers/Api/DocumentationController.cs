using Crm.Domains.Request;
using DNI.Core.Contracts;
using DNI.Core.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

        [HttpGet]
        public async Task<ActionResult> Output(string groupName, CancellationToken cancellationToken)
        {
            var request = new GetDocumentationRequest { GroupName = groupName };
            var response = await Mediator.Send(request, cancellationToken);

            using var stringWriter = new StringWriter();
            using var jsonDocument = new JsonTextWriter(stringWriter);
            jsonDocument.WriteStartObject();
            jsonDocument.WritePropertyName("swagger");
            jsonDocument.WriteValue("3.0");

            foreach(var item in response.Result.Items)
            {
                
                foreach(var subItem in item.Items)
                {
                    
                }
            }
            jsonDocument.WriteEndObject();

            return Ok();
        }
    }
}
