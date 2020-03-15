using Crm.Domains.Dto;
using MediatR;

namespace Crm.Domains.Notifications
{
    public class SaveCustomerNotification : INotification
    {
        public Customer SavedCustomer { get; set; }
    }
}
