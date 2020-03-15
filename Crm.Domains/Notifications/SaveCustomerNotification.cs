using Crm.Domains.Dto;
using MediatR;

namespace Crm.Domains.Notifications
{
    public class CustomerSavedNotification : INotification
    {
        public Customer SavedCustomer { get; set; }
    }
}
