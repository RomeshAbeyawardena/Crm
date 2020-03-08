using Crm.Domains.Request;
using Crm.Domains.ViewModels;
using DNI.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ResponseHelper = DNI.Core.Domains.Response;
namespace Crm.Web.Controllers
{
    public class CustomerController : ControllerBase
    {
        public CustomerController(IMediatorService mediatorService, IMapperProvider mapperProvider) 
            : base(mediatorService, mapperProvider)
        {
        }

        public async Task<ActionResult> SearchCustomers(GetCustomerViewModel model, CancellationToken cancellationToken)
        {
            var request = MapperProvider.Map<GetCustomerViewModel, SearchCustomersRequest>(model);
            
            var response = await MediatorService.Send(request, cancellationToken);

            if(ResponseHelper.IsSuccessful(response))
                return Ok(response.Result);

            return BadRequest(response.Errors);
        }

        public async Task<ActionResult> GetCustomer(GetCustomerViewModel model, CancellationToken cancellationToken)
        {
            var request = MapperProvider.Map<GetCustomerViewModel, GetCustomerRequest>(model);
            
            var response = await MediatorService.Send(request, cancellationToken);

            if(ResponseHelper.IsSuccessful(response))
                return Ok(response.Result);

            return BadRequest(response.Errors);
        }
    }
}
