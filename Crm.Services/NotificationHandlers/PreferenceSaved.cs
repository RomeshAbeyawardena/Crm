using Crm.Domains.Constants;
using Crm.Services.Notifications;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Enumerations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services.NotificationHandlers
{
    public class PreferenceSaved : INotificationHandler<PreferenceSavedNotification>
    {
        private readonly ICacheEntryTracker _cacheEntryTracker;

        public PreferenceSaved(ICacheEntryTracker cacheEntryTracker)
        {
            _cacheEntryTracker = cacheEntryTracker;
        }

        public async Task Handle(PreferenceSavedNotification notification, CancellationToken cancellationToken)
        {
            if(notification.IsNewCategory)
                await _cacheEntryTracker.SetState(CacheConstants.CategoryCache,
                    CacheEntryState.New, cancellationToken);

            await _cacheEntryTracker.SetState(CacheConstants.PreferenceCache,
                    CacheEntryState.New, cancellationToken);
        }
    }
}
