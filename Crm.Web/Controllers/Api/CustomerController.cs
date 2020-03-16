using Crm.Domains.Notifications;
using Crm.Domains.Request;
using Crm.Domains.ViewModels;
using DNI.Core.Contracts;
using DNI.Core.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Web.Controllers.Api
{
    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(CustomerController))]
    public class CustomerController : DefaultApiControllerBase
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

            return await HandleResponse(response, cancellationToken, async (response, ct) => await Mediator
                .Publish(new CustomerSavedNotification { SavedCustomer = response.Result }, ct));
        }

        [HttpGet]
        public async Task<ActionResult> SearchCustomers(SearchCustomerViewModel model, CancellationToken cancellationToken)
        {
            EnsureModalStateIsValid();

            var request = Mapper.Map<SearchCustomerViewModel, SearchCustomersRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            return await HandleResponse(response, cancellationToken);
        }

        [HttpGet]
        public async Task<ActionResult> SearchCustomersByKeyword(SearchCustomersByKeywordViewModel model, CancellationToken cancellationToken)
        {
            EnsureModalStateIsValid();

            var request = Mapper.Map<SearchCustomersByKeywordViewModel, SearchCustomersByKeywordRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            return await HandleResponse(response, cancellationToken);
        }

        [HttpGet]
        public async Task<ActionResult> GetCustomer(GetCustomerViewModel model, CancellationToken cancellationToken)
        {
            EnsureModalStateIsValid();

            var request = Mapper.Map<GetCustomerViewModel, GetCustomerRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            return await HandleResponse(response, cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult> VerifyCustomerCredentials([FromForm] VerifyCustomerCredentialsViewModel model, CancellationToken cancellationToken)
        {
            EnsureModalStateIsValid();

            var request = Mapper
                .Map<VerifyCustomerCredentialsViewModel, VerifyCustomerCredentialsRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            return await HandleResponse(response, cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult> SaveCustomerAttribute([FromForm] SaveCustomerAttributeViewModel model, CancellationToken cancellationToken)
        {
            EnsureModalStateIsValid();

            var request = Mapper.Map<SaveCustomerAttributeViewModel, SaveCustomerAttributeRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            return await HandleResponse(response, cancellationToken, async (response, cT) => await Mediator
                .Publish(new AttributeSavedNotification { IsNewAttribute = response.IsNewAttribute, AttributeId = response.AttributeId })
            );

        }

        [HttpGet]
        public async Task<ActionResult> GetCustomerAttributes(GetCustomerAttributeViewModel model, CancellationToken cancellationToken)
        {
            EnsureModalStateIsValid();

            var request = Mapper.Map<GetCustomerAttributeViewModel, GetCustomerAttributeRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            return await HandleResponse(response, cancellationToken);
        }

        [HttpGet]
        public async Task<ActionResult> GetCustomerPreferences(GetCustomerPreferencesViewModel model, CancellationToken cancellationToken)
        {
            EnsureModalStateIsValid();

            var request = Mapper.Map<GetCustomerPreferencesViewModel, GetCustomerPreferencesRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            return await HandleResponse(response, cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult> SaveCustomerPreferences([FromForm] SaveCustomerPreferencesViewModel model, CancellationToken cancellationToken)
        {
            EnsureModalStateIsValid();

            var request = Mapper.Map<SaveCustomerPreferencesViewModel, SaveCustomerPreferencesRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            return await HandleResponse(response, cancellationToken);
        }
    }
}
