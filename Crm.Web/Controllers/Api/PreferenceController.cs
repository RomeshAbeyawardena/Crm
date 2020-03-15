﻿using Crm.Domains.Request;
using Crm.Domains.ViewModels;
using Crm.Services.NotificationHandlers;
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

            if(!IsResponseValid(response))
                return BadRequest(response.Errors);

            return Ok(response.Result);
        }

        [HttpPost]
        public async Task<ActionResult> SavePreference(SavePreferenceViewModel model, 
            CancellationToken cancellationToken)
        {
            EnsureModalStateIsValid();

            var request = Mapper
                .Map<SavePreferenceViewModel, SavePreferenceRequest>(model);

            var response = await Mediator.Send(request, cancellationToken);

            if(!IsResponseValid(response))
                return BadRequest(response.Errors);

            await Mediator.Publish(new PreferenceSaved { 
                CategoryId = response.CategoryId,
                IsNewCategory = response.IsNewCategory,
                PreferenceId = response.Result.Id 
            }, cancellationToken);
            return Ok(response.Result);
        }
    }
}
