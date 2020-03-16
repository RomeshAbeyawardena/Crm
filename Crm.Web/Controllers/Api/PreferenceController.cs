using Crm.Domains.Request;
using Crm.Domains.ViewModels;
using Crm.Services.NotificationHandlers;
using Crm.Services.Notifications;
using DNI.Core.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Web.Controllers.Api
{
    public class PreferenceController : ControllerBase
    {
        public PreferenceController(IMediatorService mediatorService, IMapperProvider mapperProvider) 
            : base(mediatorService, mapperProvider)
        {
        }

        [HttpGet]
        public async Task<ActionResult> GetPreferences(GetPreferencesViewModel model,
            CancellationToken cancellationToken)
        {
            EnsureModalStateIsValid();

            var request = Mapper
                .Map<GetPreferencesViewModel, GetPreferencesRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            return await HandleResponse(response, cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult> SavePreference(SavePreferenceViewModel model, 
            CancellationToken cancellationToken)
        {
            EnsureModalStateIsValid();

            var request = Mapper
                .Map<SavePreferenceViewModel, SavePreferenceRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            return await HandleResponse(response, cancellationToken, async(response, ct) => 
                await Mediator.Publish(new PreferenceSavedNotification { 
                    CategoryId = response.CategoryId,
                    IsNewCategory = response.IsNewCategory,
                    PreferenceId = response.Result.Id 
                }, ct));
        }
    }
}
