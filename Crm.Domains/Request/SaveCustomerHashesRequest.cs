using Crm.Domains.Dto;
using Crm.Domains.Response;
using MediatR;
using System.Collections.Generic;

namespace Crm.Domains.Request
{
    public class SaveCustomerHashesRequest :IRequest<SaveCustomerHashesResponse>
    {
        public IEnumerable<CharacterIndex> Characters { get; set; }
        public int CustomerId { get; set; }
    }
}
