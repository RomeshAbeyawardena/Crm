﻿using Crm.Contracts.Services;
using Crm.Domains.Notifications;
using Crm.Domains.Request;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Providers;
using DNI.Core.Shared.Extensions;
using Hangfire;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services.NotificationHandlers
{
    public class CustomerSaved : INotificationHandler<CustomerSavedNotification>
    {
        private readonly IClockProvider _clockProvider;
        private readonly ICustomerService _customerService;
        private readonly ICharacterHashService _characterHashService;

        public CustomerSaved(IClockProvider clockProvider, ICustomerService customerService, ICharacterHashService characterHashService)
        {
            _clockProvider = clockProvider;
            _customerService = customerService;
            _characterHashService = characterHashService;
        }

        public async Task Handle(CustomerSavedNotification notification, CancellationToken cancellationToken)
        {
            var characters = _characterHashService.GetCharacters(notification.SavedCustomer.FirstName);
            characters =  characters.Append(_characterHashService.GetCharacters(notification.SavedCustomer.MiddleName));
            characters = characters.Append(_characterHashService.GetCharacters(notification.SavedCustomer.LastName));
            
            var customer = await _customerService.GetCustomerById(notification.SavedCustomer.Id, cancellationToken);

            if(customer == null || customer.LastIndexed.HasValue)
                return;

            BackgroundJob.Enqueue<IMediatorService>((mediator) => mediator.Send(new 
                SaveCustomerHashesRequest { Characters = characters.ToArray(), CustomerId = notification.SavedCustomer.Id }, cancellationToken));

            customer.LastIndexed = _clockProvider.DateTimeOffset;

            await _customerService.SaveCustomer(customer, true, true, cancellationToken);

        }
    }
}
