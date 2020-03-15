﻿using Crm.Domains.Request;
using Crm.Domains.ViewModels;
using DNI.Core.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<ActionResult> GetPreferences(GetPreferencesViewModel model)
        {
            EnsureModalStateIsValid();

            var request = Mapper
                .Map<GetPreferencesViewModel, GetPreferencesRequest>(model);

            var response = Mediator.Send(request);

            return Ok();
        }
    }
}
