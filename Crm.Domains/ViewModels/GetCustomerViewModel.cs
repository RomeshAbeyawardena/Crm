using Crm.Domains.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
