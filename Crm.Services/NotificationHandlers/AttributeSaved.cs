using DNI.Core.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Crm.Domains.Notifications;
using System.Threading;
using Crm.Domains.Constants;
using DNI.Core.Contracts.Enumerations;

namespace Crm.Services.NotificationHandlers
{
    public class AttributeSaved : INotificationHandler<AttributeSavedNotification>
    {
        private readonly ICacheEntryTracker _cacheEntryTracker;

        public AttributeSaved(ICacheEntryTracker cacheEntryTracker)
        {
            _cacheEntryTracker = cacheEntryTracker;
        }

        public async Task Handle(AttributeSavedNotification notification, CancellationToken cancellationToken)
        {
            if(notification.IsNewAttribute)
                await _cacheEntryTracker.SetState(CacheConstants.AttributeCache, CacheEntryState.Invalid, cancellationToken);
        }
    }
}
