using DNI.Core.Contracts;
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
    }
}
