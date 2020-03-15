using DNI.Core.Contracts;
using DNI.Core.Services.Attributes;
using DNI.Core.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using using ResponseHelper = DNI.Core.Domains.Response;
using DNI.Core.Domains;

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

        protected void EnsureModalStateIsValid()
        {
            if (ModelState.IsValid)
                return;

            throw new ModelStateException(ModelState);
        }
    }
}
