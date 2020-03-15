using Crm.Domains.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Crm.Domains.ViewModels
{
    public class SearchCustomerViewModel : ICustomerIdentifier, ICustomer, IPagedRequest
    {
        [EmailAddress]
        public string EmailAddress { get; set; }
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int PageNumber { get; set; }
        public int MaximumRowsPerPage { get; set; }
    }
}
