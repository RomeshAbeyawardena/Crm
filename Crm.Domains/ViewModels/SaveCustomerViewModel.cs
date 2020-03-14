using Crm.Domains.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.ViewModels
{
    public class SaveCustomerViewModel : ICustomerIdentifier, ICustomer, ICustomerCredential
    {
        public int? Id { get; set; }

        [EmailAddress, Required]
        public string EmailAddress { get; set; }

        [Required, MinLength(3), MaxLength(32)]
        public string FirstName { get; set; }

        [MaxLength(32)]
        public string MiddleName { get; set; }

        [Required, MinLength(3), MaxLength(32)]
        public string LastName { get; set; }

        [MinLength(3), MaxLength(16)]
        public string Password { get; set; }
    }
}
