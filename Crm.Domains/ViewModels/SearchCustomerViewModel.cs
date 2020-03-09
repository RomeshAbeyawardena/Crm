using Crm.Domains.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.ViewModels
{
    public class SearchCustomerViewModel : ICustomerIdentifier, ICustomer, IPagedRequest
    {
        public string EmailAddress { get; set; }
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int PageNumber { get; set; }
        public int MaximumRowsPerPage { get; set; }
    }
}
