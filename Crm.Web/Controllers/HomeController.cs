using DNI.Core.Contracts;
using DNI.Core.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Crm.Web.Controllers
{
    public class HomeController : DefaultControllerBase
    {
        public HomeController(IMediatorService mediatorService, IMapperProvider mapperProvider) : base(mediatorService, mapperProvider)
        {
        }

        [HttpGet, Route("/")]
        public Task<ViewResult> Index()
        {
            return Task.FromResult(View());
        }
    }
}
