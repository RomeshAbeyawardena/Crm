using Crm.Domains.Response;
using DNI.Core.Contracts.Options;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Request
{
    public class SearchCustomersRequest : IRequest<SearchCustomersResponse>
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public int PageNumber { get; set; }
        public int MaximumRowsPerPage { get; set; }
    }
}
