using DNI.Core.Contracts;
using DNI.Core.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly IMediatorService Mediator;
        protected readonly IMapperProvider Mapper;
        
        public ControllerBase(IMediatorService mediatorService, IMapperProvider mapperProvider)
        {
            Mediator = mediatorService;
            Mapper = mapperProvider;
        }

        bool EnsureModalStateIsValid()
        {
            if (ModelState.IsValid)
                return true;

            throw new ModelStateException(ModelState);
        }
    }
}
