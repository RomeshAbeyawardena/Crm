using Crm.Domains.Dto;
using DNI.Core.Domains;

namespace Crm.Domains.Response
{
    public class SaveCustomerAttributeResponse : ResponseBase<CustomerAttribute>
    {
        public bool IsNewAttribute { get; set; }
        public int AttributeId { get; set; }
    }
}
