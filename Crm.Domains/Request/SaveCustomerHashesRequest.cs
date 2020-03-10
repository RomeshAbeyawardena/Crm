using Crm.Domains.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Request
{
    public class SaveCustomerHashesRequest :IRequest<SaveCustomerHashesResponse>
    {
        public IEnumerable<char> Characters { get; set; }
        public int CustomerId { get; set; }
    }
}
