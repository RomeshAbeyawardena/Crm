using DNI.Core.Contracts;
using DNI.Core.Domains;
using DNI.Core.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Services.ExceptionHandlers
{
    public class ModelStateExceptionHandler : IExceptionHandler<ModelStateException>
    {
        
        public bool HandleException(ExceptionContext exception)
        {
            exception.Result = new BadRequestObjectResult(exception.ModelState);
            return true;
        }
    }
}
