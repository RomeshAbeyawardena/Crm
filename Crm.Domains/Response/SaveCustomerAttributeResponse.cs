using Crm.Domains.Dto;
using DNI.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Response
{
    public class SaveCustomerAttributeResponse : ResponseBase<CustomerAttribute>
    {
        public bool IsNewAttribute { get; set; }
        public int AttributeId { get; set; }
    }
}
