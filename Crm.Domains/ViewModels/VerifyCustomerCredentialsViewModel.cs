using Crm.Domains.Contracts;
using System.ComponentModel.DataAnnotations;

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
