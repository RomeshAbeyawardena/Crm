using DNI.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Web.Controllers
{
    public class HomeController : ControllerBase
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
