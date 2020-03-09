using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.ViewModels
{
    public class SaveCustomerAttributeViewModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string Property { get; set; }
        public object Value { get; set; }
    }
}
