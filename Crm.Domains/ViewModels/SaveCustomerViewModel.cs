using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.ViewModels
{
    public class SaveCustomerViewModel : ICustomerViewModel
    {
        public int? Id { get; set; }

        [EmailAddress, Required]
        public string EmailAddress { get; set; }

        [Required]
        public string FirstName { get; set; }


        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
