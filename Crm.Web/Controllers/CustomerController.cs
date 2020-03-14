using Crm.Domains.Notifications;
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

        [HttpPost]
        public async Task<ActionResult> SaveCustomer([FromForm] SaveCustomerViewModel model, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = Mapper.Map<SaveCustomerViewModel, SaveCustomerRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            if (!ResponseHelper.IsSuccessful(response))
                return BadRequest(response.Errors);
            
            await Mediator.Publish(new SaveCustomerNotification { SavedCustomer = response.Result }, cancellationToken);

            return Ok(response.Result);
        }

        [HttpGet]
        public async Task<ActionResult> SearchCustomers(SearchCustomerViewModel model, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = Mapper.Map<SearchCustomerViewModel, SearchCustomersRequest>(model);
            
            var response = await Mediator.Send(request, cancellationToken);

            if(ResponseHelper.IsSuccessful(response))
                return Ok(response.Result);

            return BadRequest(response.Errors);
        }

        [HttpGet]
        public async Task<ActionResult> GetCustomer(GetCustomerViewModel model, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = Mapper.Map<GetCustomerViewModel, GetCustomerRequest>(model);
            
            var response = await Mediator.Send(request, cancellationToken);

            if(ResponseHelper.IsSuccessful(response))
                return Ok(response.Result);

            return BadRequest(response.Errors);
        }

        [HttpPost]
        public async Task<ActionResult> VerifyCustomerCredentials([FromForm] VerifyCustomerCredentialsViewModel model, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = Mapper
                .Map<VerifyCustomerCredentialsViewModel, VerifyCustomerCredentialsRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            if(ResponseHelper.IsSuccessful(response))
                return Ok(response.Result);
            
            return BadRequest(response.Errors);
        }

        [HttpPost]
        public async Task<ActionResult> SaveCustomerAttribute([FromForm] SaveCustomerAttributeViewModel model, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = Mapper.Map<SaveCustomerAttributeViewModel, SaveCustomerAttributeRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            if(ResponseHelper.IsSuccessful(response))
                return Ok(response.Result);

            return BadRequest(response.Errors);
        }

        [HttpGet]
        public async Task<ActionResult> GetCustomerAttributes(GetCustomerAttributeViewModel model, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = Mapper.Map<GetCustomerAttributeViewModel, GetCustomerAttributeRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            if(ResponseHelper.IsSuccessful(response))
                return Ok(response.Result);

            return BadRequest(response.Errors);
        }
    }
}
