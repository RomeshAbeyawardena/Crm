using DNI.Core.Contracts;
using DNI.Core.Services.Attributes;
using DNI.Core.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

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

        protected void EnsureModalStateIsValid()
        {
            if (ModelState.IsValid)
                return;

            throw new ModelStateException(ModelState);
        }
    }
}
