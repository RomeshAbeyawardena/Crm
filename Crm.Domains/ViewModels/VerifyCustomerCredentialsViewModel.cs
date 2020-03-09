using Crm.Domains.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.ViewModels
{
    public class VerifyCustomerCredentialsViewModel : ICustomerIdentifier, ICustomerCredential
    {
        [Required]
        public string Password { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }
    }
}
