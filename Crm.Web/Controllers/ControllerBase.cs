using DNI.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly IMediatorService MediatorService;
        protected readonly IMapperProvider MapperProvider;

        public ControllerBase(IMediatorService mediatorService, IMapperProvider mapperProvider)
        {
            MediatorService = mediatorService;
            MapperProvider = mapperProvider;
        }
    }
}
