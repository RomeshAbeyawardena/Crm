using MediatR;

namespace Crm.Domains.Notifications
{
    public class AttributeSavedNotification : INotification
    {
        public bool IsNewAttribute { get; set; }
        public int AttributeId { get; set; }
    }
}
