using DNI.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RootControllerBase = Crm.Web.Controllers.ControllerBase;

namespace Crm.Web.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    public abstract class ControllerBase : RootControllerBase
    {
        public ControllerBase(IMediatorService mediatorService, IMapperProvider mapperProvider) : base(mediatorService, mapperProvider)
        {
        }
    }
}
