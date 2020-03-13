﻿using Crm.Contracts.Services;
using Crm.Domains.Notifications;
using Crm.Domains.Request;
using Crm.Domains.Response;
using Crm.Services.RequestHandlers;
using DNI.Core.Contracts;
using DNI.Core.Shared.Extensions;
using Hangfire;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services.NotificationHandlers
{
    public class SaveCustomer : INotificationHandler<SaveCustomerNotification>
    {
        private readonly ICharacterHashService _characterHashService;

        public SaveCustomer(ICharacterHashService characterHashService)
        {
            _characterHashService = characterHashService;
        }

        public Task Handle(SaveCustomerNotification notification, CancellationToken cancellationToken)
        {
            var characters = _characterHashService.GetCharacters(notification.SavedCustomer.FirstName);
            characters =  characters.Append(_characterHashService.GetCharacters(notification.SavedCustomer.MiddleName));
            characters = characters.Append(_characterHashService.GetCharacters(notification.SavedCustomer.LastName));
            
            BackgroundJob.Enqueue<IMediatorService>((mediator) => mediator.Send(new SaveCustomerHashesRequest { Characters = characters.ToArray(), CustomerId = notification.SavedCustomer.Id }, cancellationToken));

            return Task.CompletedTask;
        }
    }
}
