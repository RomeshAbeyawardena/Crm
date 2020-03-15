using Crm.Domains.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Crm.Domains.ViewModels
{
    public class GetCustomerViewModel : ICustomerIdentifier, ICustomer
    {
        public int? Id { get; set; }
        
        [EmailAddress]
        public string EmailAddress { get; set; }
        
        [MinLength(3), MaxLength(32)]
        public string FirstName { get; set; }

        [MinLength(3), MaxLength(32)]
        public string MiddleName { get; set; }

        [MinLength(3), MaxLength(32)]
        public string LastName { get; set; }
    }
}
