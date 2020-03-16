using DNI.Core.Contracts;
using DNI.Core.Services.Attributes;
using DNI.Core.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ResponseHelper = DNI.Core.Domains.Response;
using DNI.Core.Domains;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace Crm.Web.Controllers
{
    [HandleException]
    public class ControllerBase : Controller
    {
        protected readonly IMediatorService Mediator;
        protected readonly IMapperProvider Mapper;
        
        public ControllerBase(IMediatorService mediatorService, IMapperProvider mapperProvider)
        {
            Mediator = mediatorService;
            Mapper = mapperProvider;
        }

        protected bool IsResponseValid(ResponseBase response)
        {
            return ResponseHelper.IsSuccessful(response);
        }

        protected async Task<ActionResult> HandleResponse<TResponse>(TResponse response, 
            CancellationToken cancellationToken,
            Func<TResponse, CancellationToken, Task> onSuccess = default,
            Func<TResponse, CancellationToken, Task> onFailure = default)
            where TResponse : ResponseBase
        {
            if(!IsResponseValid(response))
            {
                await onFailure(response, cancellationToken);
                return BadRequest(response.Errors);
            }

            if(onSuccess != null)
                await onSuccess(response, cancellationToken);
                
            return Ok(response.Result);
        }

        protected void EnsureModalStateIsValid()
        {
            if (ModelState.IsValid)
                return;

            throw new ModelStateException(ModelState);
        }
    }
}
