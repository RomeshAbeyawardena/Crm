﻿using Crm.Domains.Notifications;
using Crm.Domains.Request;
using Crm.Domains.ViewModels;
using DNI.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Web.Controllers.Api
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
            EnsureModalStateIsValid();

            var request = Mapper.Map<SaveCustomerViewModel, SaveCustomerRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            if (!IsResponseValid(response))
                return BadRequest(response.Errors);
            
            await Mediator.Publish(new SaveCustomerNotification { SavedCustomer = response.Result }, cancellationToken);

            return Ok(response.Result);
        }

        [HttpGet]
        public async Task<ActionResult> SearchCustomers(SearchCustomerViewModel model, CancellationToken cancellationToken)
        {
            EnsureModalStateIsValid();

            var request = Mapper.Map<SearchCustomerViewModel, SearchCustomersRequest>(model);
            
            var response = await Mediator.Send(request, cancellationToken);

            if(IsResponseValid(response))
                return Ok(response.Result);

            return BadRequest(response.Errors);
        }

        [HttpGet]
        public async Task<ActionResult> SearchCustomersByKeyword(SearchCustomersByKeywordViewModel model)
        {
            EnsureModalStateIsValid();

            var request = Mapper.Map<SearchCustomersByKeywordViewModel, SearchCustomersByKeywordRequest>(model);

            var response = await Mediator.Send(request);

            if(IsResponseValid(response))
                return Ok(response.Result);

            return BadRequest(response.Errors);
        }

        [HttpGet]
        public async Task<ActionResult> GetCustomer(GetCustomerViewModel model, CancellationToken cancellationToken)
        {
            EnsureModalStateIsValid();

            var request = Mapper.Map<GetCustomerViewModel, GetCustomerRequest>(model);
            
            var response = await Mediator.Send(request, cancellationToken);

            if(IsResponseValid(response))
                return Ok(response.Result);

            return BadRequest(response.Errors);
        }

        [HttpPost]
        public async Task<ActionResult> VerifyCustomerCredentials([FromForm] VerifyCustomerCredentialsViewModel model, CancellationToken cancellationToken)
        {
            EnsureModalStateIsValid();

            var request = Mapper
                .Map<VerifyCustomerCredentialsViewModel, VerifyCustomerCredentialsRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            if(IsResponseValid(response))
                return Ok(response.Result);
            
            return BadRequest(response.Errors);
        }

        [HttpPost]
        public async Task<ActionResult> SaveCustomerAttribute([FromForm] SaveCustomerAttributeViewModel model, CancellationToken cancellationToken)
        {
            EnsureModalStateIsValid();

            var request = Mapper.Map<SaveCustomerAttributeViewModel, SaveCustomerAttributeRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            if(!IsResponseValid(response))
                return BadRequest(response.Errors);

            await Mediator.Publish(new AttributeSavedNotification { 
                IsNewAttribute = response.IsNewAttribute, 
                AttributeId = response.AttributeId }, cancellationToken);

            return Ok(response.Result);
        }

        [HttpGet]
        public async Task<ActionResult> GetCustomerAttributes(GetCustomerAttributeViewModel model, CancellationToken cancellationToken)
        {
            EnsureModalStateIsValid();

            var request = Mapper.Map<GetCustomerAttributeViewModel, GetCustomerAttributeRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            if(IsResponseValid(response))
                return Ok(response.Result);

            return BadRequest(response.Errors);
        }

        [HttpGet]
        public async Task<ActionResult> GetCustomerPreferences(GetCustomerPreferencesViewModel model)
        {
            EnsureModalStateIsValid();

            var request = Mapper.Map<GetCustomerPreferencesViewModel, GetCustomerPreferencesRequest>(model);

            var response = await Mediator.Send(request);

            if(IsResponseValid(response))
                return Ok(response.Result);

            return BadRequest(response.Errors);
        }

        [HttpPost]
        public async Task<ActionResult> SaveCustomerPreferences([FromForm] SaveCustomerPreferencesViewModel model)
        {
            EnsureModalStateIsValid();

            var request = Mapper.Map<SaveCustomerPreferencesViewModel, SaveCustomerPreferencesRequest>(model);

            var response = await Mediator.Send(request);

            if(IsResponseValid(response))
                return Ok(response.Result);

            return BadRequest(response.Errors);
        }
    }
}