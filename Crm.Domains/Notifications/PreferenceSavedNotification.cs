using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Services.Notifications
{
    public class PreferenceSavedNotification : INotification
    {
        public int CategoryId { get; set; }
        public bool IsNewCategory { get; set; }
        public int PreferenceId { get; set; }
    }
}
